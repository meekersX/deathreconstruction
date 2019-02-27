using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace deathreconstruction
{
    class InventoryReconstructor
    {
        Character character;
        private Dictionary<uint, Item> otherObjects;
        List<PacketRecord> records;

        private void LoadFile(string fileName)
        {
            bool isPcapng = false;
            bool searchAborted = false;

            Console.WriteLine(fileName);
            records = PCapReader.LoadPcap(fileName, true, ref searchAborted, ref isPcapng);
        }

        public List<Tuple<Character, List<uint>, string>> Reconstruct(string fileName)
        {
            List<Tuple<Character, List<uint>, string>> foundDeaths = new List<Tuple<Character, List<uint>, string>>();

            LoadFile(fileName);

            List<Tuple<int, int>> deathRecords = FindDeaths();
            if (deathRecords.Count == 0)
            {
                // no valid deaths found
                return foundDeaths;
            }
            int deathIndex = 0;
            List<uint> droppedItems = null;
            string deathText = null;
            int lastDeathRecord = int.MaxValue;

            character = new Character();
            otherObjects = new Dictionary<uint, Item>();

            for (int i = deathRecords[0].Item1; i < records.Count; i++)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOpcode(messageDataReader);
                    switch (opcode)
                    {
                        case PacketOpcode.PLAYER_DESCRIPTION_EVENT:
                            {
                                using (BinaryReader comessageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                                {
                                    Util.readOrderedOpcode(comessageDataReader, ref character.ID);
                                }
                                PlayerDescription(messageDataReader);
                                break;
                            }
                        case PacketOpcode.Evt_Physics__CreateObject_ID:
                            {
                                uint ID = CreateObject(messageDataReader);
                                break;
                            }
                        case PacketOpcode.INVENTORY_PUT_OBJ_IN_CONTAINER_EVENT:
                            {
                                PutObjectInContainer(messageDataReader);
                                break;
                            }
                        case PacketOpcode.INVENTORY_WIELD_OBJ_EVENT:
                            {
                                WieldItem(messageDataReader);
                                break;
                            }
                        case PacketOpcode.INVENTORY_REMOVE_OBJ_EVENT:
                            {
                                RemoveObject(messageDataReader);
                                break;
                            }
                        case PacketOpcode.VIEW_CONTENTS_EVENT:
                            {
                                ViewContents(messageDataReader);
                                break;
                            }
                        case PacketOpcode.STACKABLE_SET_STACKSIZE_EVENT:
                            {
                                UpdateStackSize(messageDataReader);
                                break;
                            }
                        case PacketOpcode.INVENTORY_PUT_OBJ_IN_3D_EVENT:
                            {
                                PutObjectIn3D(messageDataReader);
                                break;
                            }
                        case PacketOpcode.Evt_Qualities__PrivateUpdateInt_ID:
                            {
                                UpdatePrivateInt(messageDataReader, opcode);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

                if (i == deathRecords[deathIndex].Item2)
                {
                    // find dropped items
                    (droppedItems, deathText, lastDeathRecord) = GetDroppedInformation(i);
                }

                if (i == lastDeathRecord)
                {
                    // add death record
                    foundDeaths.Add(new Tuple<Character, List<uint>, string>(new Character(character), droppedItems, deathText));

                    deathIndex++;
                    if (deathIndex >= deathRecords.Count)
                    {
                        // finished all deaths
                        break;
                    }

                    if (deathRecords[deathIndex - 1].Item1 != deathRecords[deathIndex].Item1) // different login events
                    {
                        // skip forward
                        character = new Character();
                        otherObjects = new Dictionary<uint, Item>();
                        i = deathRecords[deathIndex].Item1;
                    }
                }
            }
            return foundDeaths;
        }

        // find login descriptions followed by deaths of the logged in character
        private List<Tuple<int, int>> FindDeaths()
        {
            uint characterID = uint.MaxValue;
            uint loginCharacterID = 0x0;
            List<Tuple<int, int>> deathRecords = new List<Tuple<int, int>>();
            int startRecord = -1;

            for (int i = 0; i < records.Count; i++)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref characterID);
                    switch (opcode)
                    {
                        case PacketOpcode.CHARACTER_ENTER_GAME_EVENT:
                            {
                                Proto_UI.EnterWorld message = Proto_UI.EnterWorld.read(messageDataReader);
                                startRecord = i;
                                break;
                            }
                        case PacketOpcode.PLAYER_DESCRIPTION_EVENT:
                            {
                                CM_Login.PlayerDescription message = CM_Login.PlayerDescription.read(messageDataReader);
                                if ((uint)message.CACQualities._weenie_type == 43480 || (uint)message.CACQualities._weenie_type == 43481)
                                {
                                    // exclude Olthoi play
                                    continue;
                                }
                                loginCharacterID = characterID;
                                break;
                            }
                        case PacketOpcode.VICTIM_NOTIFICATION_EVENT:
                            {
                                // we have a player descrption event for this death
                                if (characterID == loginCharacterID)
                                {
                                    deathRecords.Add(new Tuple<int, int>(startRecord, i));
                                }
                                break;
                            }
                    }
                }
            }

            return deathRecords;
        }

        private Tuple<List<uint>, string, int> GetDroppedInformation(int deathRecord)
        {
            uint affectedCharacterID = character.ID;
            List<uint> droppedItems = new List<uint>();
            string deathText = null;
            int lastDeathRecord = deathRecord;

            int i;
            // find the death text, assumes printed after all items are moved
            // need to update for augments and no items dropped
            for (i = deathRecord + 1; i < records.Count; i++)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readUnorderedOpcode(messageDataReader);
                    if (opcode == PacketOpcode.Evt_Communication__TextboxString_ID)
                    {
                        CM_Communication.TextBoxString message = CM_Communication.TextBoxString.read(messageDataReader);

                        if (message.ChatMessageType == 0x0)
                        {
                            deathText = message.MessageText.ToString();

                            if (deathText.StartsWith("You've lost"))
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (deathText == null)
            {
                // couldn't find death text
                return new Tuple<List<uint>, string, int>(droppedItems, "", 0);
            }
            for (--i; i > deathRecord; i--)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref affectedCharacterID);

                    if (opcode == PacketOpcode.INVENTORY_PUT_OBJ_IN_CONTAINER_EVENT)
                    {
                        CM_Inventory.PutObjectInContainerEvent message = CM_Inventory.PutObjectInContainerEvent.read(messageDataReader);

                        Debug.Assert(affectedCharacterID == character.ID);
                        if (character.Contains(message.i_container))
                        {
                            // we can pickup items after we die
                            if (lastDeathRecord == deathRecord)
                            {
                                lastDeathRecord = i;
                            }
                        }
                        else
                        {
                            droppedItems.Add(message.i_objectId);
                        }
                    }
                }
            }

            return new Tuple<List<uint>, string, int>(droppedItems, deathText, lastDeathRecord);
        }

        private void PlayerDescription(BinaryReader messageDataReader)
        {
            CM_Login.PlayerDescription message = CM_Login.PlayerDescription.read(messageDataReader);
            // level
            character.Name = message.CACQualities.CBaseQualities._strStatsTable.hashTable[STypeString.NAME_STRING].ToString();
            character.Level = message.CACQualities.CBaseQualities._intStatsTable.hashTable[STypeInt.LEVEL_INT];
            // pack items
            foreach (CM_Inventory.ContentProfile item in message.clist.list)
            {
                character.AddItem(item);

                Item addedItem;
                uint id = item.m_iid;
                if (otherObjects.TryGetValue(id, out addedItem))
                {
                    // moving pack in world into inventory
                    character.UpdateItem(addedItem);
                    if (item.m_uContainerProperties == (uint)ContainerProperties.Container)
                    {
                        // look for items in the world in this container to add
                        foreach (Item otherItem in new List<Item>(otherObjects.Values))
                        {
                            if (otherItem.ContainerID == id)
                            {
                                character.AddItem(otherItem);
                                otherObjects.Remove(otherItem.ID);
                            }
                        }
                    }
                    otherObjects.Remove(id);
                }
            }
            // wielded items
            foreach (CM_Login.InventoryPlacement item in message.ilist.list)
            {
                character.AddWieldedItem(item);
            }
        }


        private uint CreateObject(BinaryReader messageDataReader)
        {
            CM_Physics.CreateObject message = CM_Physics.CreateObject.read(messageDataReader);

            uint id = message.object_id;
            Item item = new Item(id, message.wdesc);

            if (!character.UpdateItem(item))
            {
                if (otherObjects.ContainsKey(id))
                {
                    otherObjects[id] = item;
                }
                else
                {
                    otherObjects.Add(id, item);
                }
            }
            return id;
        }

        private void PutObjectInContainer(BinaryReader messageDataReader)
        {
            CM_Inventory.PutObjectInContainerEvent message = CM_Inventory.PutObjectInContainerEvent.read(messageDataReader);

            uint id = message.i_objectId;
            uint containerID = message.i_container;

            Item item;
            if (character.MoveItem(id, containerID, out item))
            {
                // existing item needs to be moved into world
                if (item != null)
                {
                    otherObjects.Add(item.ID, item);
                }
            }
            else if (otherObjects.TryGetValue(id, out item))
            {
                if (character.Contains(containerID))
                {
                    // moving item in world into inventory
                    character.AddItemToContainer(item, containerID);
                    otherObjects.Remove(id);
                }
                else
                {
                    // moving item within world
                    item.ContainerID = containerID;
                }
            }
            else // non-existent items
            {
                item = new Item(id);
                if (character.Contains(containerID))
                {
                    // move non-existent item into inventory
                    character.AddItemToContainer(item, containerID);
                }
                else
                {
                    // move non-existent item into world
                    otherObjects.Add(item.ID, item);
                }
            }
        }

        private void WieldItem(BinaryReader messageDataReader)
        {
            CM_Inventory.WieldItem message = CM_Inventory.WieldItem.read(messageDataReader);

            character.WieldItem(message.i_item);
        }

        private void RemoveObject(BinaryReader messageDataReader)
        {
            CM_Inventory.RemoveObject message = CM_Inventory.RemoveObject.read(messageDataReader);

            if (character.RemoveItem(message.i_item) == null)
            {
                otherObjects.Remove(message.i_item);
            }
        }

        private void ViewContents(BinaryReader messageDataReader)
        {
            CM_Inventory.ViewContents message = CM_Inventory.ViewContents.read(messageDataReader);

            uint containerID = message.i_container;
            if (character.FindItem(containerID) != null)
            {
                foreach (var item in message.contents_list.list)
                {
                    character.AddItemToContainer(item, containerID);
                }
            }
            else
            {
                Item containerItem;
                if (otherObjects.TryGetValue(containerID, out containerItem))
                {
                }
                else
                {
                    otherObjects.Add(containerID, new Item(containerID));
                    foreach (var item in message.contents_list.list)
                    {
                        Item addedItem = new Item(item.m_iid);
                        addedItem.ContainerID = containerID;
                        otherObjects.Add(item.m_iid, addedItem);
                    }
                }
            }
        }

        private void UpdateStackSize(BinaryReader messageDataReader)
        {
            CM_Inventory.UpdateStackSize message = CM_Inventory.UpdateStackSize.read(messageDataReader);

            uint id = message.item;

            if (!character.UpdateStackSize(id, message.amount, message.newValue))
            {
                if (!otherObjects.ContainsKey(id))
                {
                    otherObjects.Add(id, new Item(id));
                }
                otherObjects[id].StackSize = message.amount;
                otherObjects[id].Value = message.newValue;
            }
        }

        private void PutObjectIn3D(BinaryReader messageDataReader)
        {
            CM_Inventory.InventoryPutObjIn3D message = CM_Inventory.InventoryPutObjIn3D.read(messageDataReader);

            uint id = message.ObjectID;

            Item item = character.RemoveItem(id);
            if (item != null)
            {
                otherObjects.Add(id, item);
            }
            else
            {
                // didn't find in our inventory
                Debug.Assert(false);
            }
        }

        private void UpdatePrivateInt(BinaryReader messageDataReader, PacketOpcode opcode)
        {
            CM_Qualities.PrivateUpdateQualityEvent<STypeInt, int> message = CM_Qualities.PrivateUpdateQualityEvent<STypeInt, int>.read(opcode, messageDataReader);

            if (message.stype == STypeInt.LEVEL_INT)
            {
                character.Level = message.val;
            }
        }
    }
}
