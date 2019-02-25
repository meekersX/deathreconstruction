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

            List<int> deathRecords = FindDeaths();
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

            int i;
            for (i = deathRecords[deathIndex] - 1; i >= 0; i--)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref character.ID);
                    if (opcode == PacketOpcode.PLAYER_DESCRIPTION_EVENT)
                    {
                        PlayerDescription(messageDataReader);
                        break;
                    }
                }
            }

            for (i = i + 1; i < records.Count; i++)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOpcode(messageDataReader);
                    switch (opcode)
                    {
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
                        default:
                            {
                                break;
                            }
                    }
                }

                if (i == deathRecords[deathIndex])
                {
                    // find dropped items
                    (droppedItems, deathText, lastDeathRecord) = GetDroppedInformation(deathRecords[deathIndex]);
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

                    // search up from next death
                    for (int j = deathRecords[deathIndex]; j > i; j--)
                    {
                        using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                        {
                            PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref character.ID);
                            if (opcode == PacketOpcode.PLAYER_DESCRIPTION_EVENT)
                            {
                                // restart from new login event
                                character = new Character();
                                otherObjects = new Dictionary<uint, Item>();
                                PlayerDescription(messageDataReader);
                                i = j;
                                break;
                            }
                        }
                    }
                }
                // continue from current login event
            }
            return foundDeaths;
        }

        // find login descriptions followed by deaths of the logged in character
        private List<int> FindDeaths()
        {
            uint characterID = uint.MaxValue;
            uint loginCharacterID = 0x0;
            List<int> deathRecords = new List<int>();

            for (int i = 0; i < records.Count; i++)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref characterID);
                    switch (opcode)
                    {
                        case PacketOpcode.PLAYER_DESCRIPTION_EVENT:
                            {
                                loginCharacterID = characterID;
                                Console.WriteLine(loginCharacterID);
                                break;
                            }
                        case PacketOpcode.VICTIM_NOTIFICATION_EVENT:
                            {
                                // we have a player descrption event for this death
                                if (characterID == loginCharacterID)
                                {
                                    deathRecords.Add(i);
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
                        // we can pickup items after we die
                        if (lastDeathRecord == deathRecord && message.i_container == character.ID)
                        {
                            lastDeathRecord = i;
                        }
                        droppedItems.Add(message.i_objectId);
                    }
                }
            }

            return new Tuple<List<uint>, string, int>(droppedItems, deathText, lastDeathRecord);
        }

        private void PlayerDescription(BinaryReader messageDataReader)
        {
            CM_Login.PlayerDescription message = CM_Login.PlayerDescription.read(messageDataReader);
            Console.WriteLine(message.ilist.Length);
            // level
            character.Name = message.CACQualities.CBaseQualities._strStatsTable.hashTable[STypeString.NAME_STRING].ToString();
            character.Level = message.CACQualities.CBaseQualities._intStatsTable.hashTable[STypeInt.LEVEL_INT];
            // pack items
            foreach (CM_Inventory.ContentProfile item in message.clist.list)
            {
                character.AddItem(item);
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

            Item item = character.MoveItem(id, containerID);

            if (item != null)
            {
                // removed from character
                otherObjects.Add(id, item);
            }
            else if (otherObjects.ContainsKey(id))
            {
                // put item into our inventory
                item = otherObjects[id];
                item.ContainerID = containerID;
                character.AddItem(item);
                otherObjects.Remove(id);
                Console.WriteLine("Picked up " + item.Name);
            }
            else
            {
                Debug.Assert(item == null);
                //// handle unknown item
                //if (containerID == character.ID || packs.ContainsKey(containerID))
                //{
                //    inventory.Add(ID, new Item(ID));
                //    inventory[ID].containerID = containerID;
                //}
                //else
                //{
                //    Debug.Assert(false);
                //}
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
    }
}
