using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace deathreconstruction
{
    class InventoryReconstructor
    {
        private Dictionary<uint, Item> inventory = new Dictionary<uint, Item>();
        private Dictionary<uint, Item> packs = new Dictionary<uint, Item>();
        private Dictionary<uint, Item> wielded = new Dictionary<uint, Item>();
        private Dictionary<uint, Item> otherObjects = new Dictionary<uint, Item>();
        private uint characterID = 0x0;
        private int characterLevel = 0;
        private string characterName = "";
        List<PacketRecord> records;

        private void LoadFile(string fileName)
        {
            bool isPcapng = false;
            bool searchAborted = false;

            Console.WriteLine(fileName);
            records = PCapReader.LoadPcap(fileName, true, ref searchAborted, ref isPcapng);
        }

        public void Reconstruct(string fileName)
        {
            LoadFile(fileName);
            List<int> deathIndices = FindDeaths();

            int deathIndex = deathIndices.First<int>();
            //int deathIndex = deathIndices.Last<int>();

            int i;
            for (i = deathIndex - 1; i >= 0; i--)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref characterID);
                    if (opcode == PacketOpcode.PLAYER_DESCRIPTION_EVENT)
                    {
                        CM_Login.PlayerDescription message = CM_Login.PlayerDescription.read(messageDataReader);
                        Console.WriteLine(message.ilist.Length);
                        // level
                        characterName = message.CACQualities.CBaseQualities._strStatsTable.hashTable[STypeString.NAME_STRING].ToString();
                        characterLevel = message.CACQualities.CBaseQualities._intStatsTable.hashTable[STypeInt.LEVEL_INT];
                        // pack items
                        foreach (CM_Inventory.ContentProfile item in message.clist.list)
                        {
                            if (item.m_uContainerProperties == (uint)ContainerProperties.None)
                            {
                                inventory[item.m_iid] = new Item(item.m_iid);
                            }
                            else
                            {
                                packs[item.m_iid] = new Item(item.m_iid);
                            }

                        }
                        // wielded items
                        foreach (CM_Login.InventoryPlacement item in message.ilist.list)
                        {
                            wielded[item.iid_] = new Item(item.iid_);
                        }
                        break;
                    }
                }
            }

            List<uint> initialItems = new List<uint>(inventory.Count);
            foreach (uint ID in inventory.Keys)
            {
                initialItems.Add(ID);
            }
            bool initialItemsPrinted = false;

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
                                if (initialItems.Count > 0)
                                {
                                    initialItems.Remove(ID);
                                }
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
                // print initial inventory
                if (!initialItemsPrinted && initialItems.Count == 0)
                {
                    PrintInventory();
                    initialItemsPrinted = true;
                }
                if (i == deathIndex)
                {
                    AlwaysDrop();
                    List<uint> droppedItems = GetDroppedItems(deathIndex);
                    foreach (uint ID in droppedItems)
                    {
                        if (inventory.ContainsKey(ID))
                        {
                            Console.Write("I ");
                            inventory[ID].Print();
                        }
                        else if (wielded.ContainsKey(ID))
                        {
                            Console.Write("W ");
                            wielded[ID].Print();
                        }
                        else
                        {
                            Debug.Assert(false);
                        }
                    }
                    Console.WriteLine(droppedItems.Count);
                    // needs:
                    // inventory snapshot
                    // dropped items
                    // dropped item string
                    //HypothesisTester tester = new HypothesisTester(characterLevel, inventory, packs, wielded);
                    //tester.Test();
                    break;
                }
            }
        }

        public void PrintInventory()
        {
            Console.WriteLine("Main Pack");
            foreach (Item item in inventory.Values)
            {
                if (item.containerID == characterID)
                {
                    item.Print();
                }
            }
            foreach (Item pack in packs.Values)
            {
                Console.WriteLine("Pack " + pack.name);
                foreach (Item item in inventory.Values)
                {
                    if (item.containerID == pack.ID)
                    {
                        item.Print();
                    }
                }
            }
            Console.WriteLine("Wielded");
            foreach (Item item in wielded.Values)
            {
                item.Print();
            }
        }

        // find login descriptions followed by deaths of the logged in character
        private List<int> FindDeaths()
        {
            uint characterID = uint.MaxValue;
            uint loginCharacterID = 0x0;
            List<int> deathIndices = new List<int>();

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
                                    deathIndices.Add(i);
                                }
                                break;
                            }
                    }
                }
            }

            return deathIndices;
        }

        private List<uint> GetDroppedItems(int deathIndex)
        {
            uint affectedCharacterID = characterID;
            uint corpseID = 0x0;
            List<uint> droppedItems = new List<uint>();

            int i;
            // find the corpse
            for (i = deathIndex + 1; i < records.Count; i++)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readUnorderedOpcode(messageDataReader);
                    if (opcode == PacketOpcode.Evt_Physics__CreateObject_ID)
                    {
                        CM_Physics.CreateObject message = CM_Physics.CreateObject.read(messageDataReader);

                        if (message.wdesc._name.ToString().Equals("Corpse of " + characterName))
                        {
                            corpseID = message.object_id;
                            break;
                        }
                    }
                }
            }
            for (--i; i > deathIndex; i--)
            {
                using (BinaryReader messageDataReader = new BinaryReader(new MemoryStream(records[i].data)))
                {
                    PacketOpcode opcode = Util.readOrderedOpcode(messageDataReader, ref affectedCharacterID);

                    if (opcode == PacketOpcode.INVENTORY_PUT_OBJ_IN_CONTAINER_EVENT)
                    {
                        CM_Inventory.PutObjectInContainerEvent message = CM_Inventory.PutObjectInContainerEvent.read(messageDataReader);

                        if (message.i_container == corpseID)
                        {
                            Debug.Assert(affectedCharacterID == characterID);
                            droppedItems.Add(message.i_objectId);
                        }
                    }
                }
            }

            return droppedItems;
        }

        private uint CreateObject(BinaryReader messageDataReader)
        {
            CM_Physics.CreateObject message = CM_Physics.CreateObject.read(messageDataReader);

            uint ID = message.object_id;

            Item item = new Item(ID, message.wdesc._name.ToString(), message.wdesc._value, message.wdesc._stackSize, message.wdesc._containerID, message.wdesc._type, message.wdesc._wcid);

            if (inventory.ContainsKey(ID))
            {
                Console.WriteLine("Inventory " + message.wdesc._name.ToString());
                inventory[ID] = item;
            }
            else if (wielded.ContainsKey(ID))
            {
                Debug.Assert(message.wdesc._wielderID == characterID);
                Console.WriteLine("Wielded " + message.wdesc._name.ToString());
                wielded[ID] = item;
            }
            else if (packs.ContainsKey(ID))
            {
                packs[ID] = item;
            }
            else
            {
                if (otherObjects.ContainsKey(ID))
                {
                    otherObjects[ID] = item;
                    // shouldn't create an object again before destroying it?
                    //Console.WriteLine("Encountered " + message.wdesc._name.ToString() + " -- Duplicate!");
                }
                else
                {
                    otherObjects.Add(message.object_id, item);
                }
            }
            return message.object_id;
        }

        private void PutObjectInContainer(BinaryReader messageDataReader)
        {
            CM_Inventory.PutObjectInContainerEvent message = CM_Inventory.PutObjectInContainerEvent.read(messageDataReader);

            uint ID = message.i_objectId;
            uint containerID = message.i_container;

            if (inventory.ContainsKey(ID))
            {
                if (containerID == characterID || packs.ContainsKey(containerID))
                {
                    // moved item into a container within inventory
                    inventory[ID].containerID = containerID;
                }
                else
                {
                    // removed item from inventory
                    otherObjects.Add(ID, inventory[ID]);
                    inventory.Remove(ID);
                    otherObjects[ID].containerID = containerID;
                }
            }
            else if (wielded.ContainsKey(ID))
            {
                // unequip item into particular pack?
                if (containerID == characterID || packs.ContainsKey(containerID))
                {
                    inventory.Add(ID, wielded[ID]);
                    inventory[ID].containerID = containerID;
                    wielded.Remove(ID);
                }
                else
                {
                    // removed item from inventory
                    otherObjects.Add(ID, wielded[ID]);
                    wielded.Remove(ID);
                    otherObjects[ID].containerID = containerID;
                }
            }
            else if (packs.ContainsKey(ID))
            {
                // moving an entire pack
                Debug.Assert(false);
            }
            else if (otherObjects.ContainsKey(ID))
            {
                // put item into our inventory
                inventory.Add(ID, otherObjects[ID]);
                otherObjects.Remove(ID);
                inventory[ID].containerID = containerID;
                Console.WriteLine("Picked up " + inventory[ID].name);
            }
            else
            {
                // handle unknown item
                if (containerID == characterID || packs.ContainsKey(containerID))
                {
                    inventory.Add(ID, new Item(ID));
                    inventory[ID].containerID = containerID;
                }
                else
                {
                    Debug.Assert(false);
                }
            }
        }

        private void WieldItem(BinaryReader messageDataReader)
        {
            CM_Inventory.WieldItem message = CM_Inventory.WieldItem.read(messageDataReader);

            uint ID = message.i_item;
            Debug.Assert(inventory.ContainsKey(ID));
            wielded.Add(ID, inventory[ID]);
            inventory.Remove(ID);
            wielded[ID].containerID = characterID;
        }

        private void RemoveObject(BinaryReader messageDataReader)
        {
            CM_Inventory.RemoveObject message = CM_Inventory.RemoveObject.read(messageDataReader);

            uint ID = message.i_item;

            if (inventory.Remove(ID))
            {
                return;
            }
            else if (packs.Remove(ID))
            {
                Debug.Assert(false);
                foreach (Item item in inventory.Values)
                {
                    if (item.containerID == ID)
                    {
                        inventory.Remove(ID);
                    }
                }
            }
            else if (wielded.Remove(ID))
            {
                return;
            }
            else if (otherObjects.Remove(ID))
            {
                return;
            }
        }

        private void ViewContents(BinaryReader messageDataReader)
        {
            CM_Inventory.ViewContents message = CM_Inventory.ViewContents.read(messageDataReader);

            uint containerID = message.i_container;
            if (packs.ContainsKey(containerID))
            {
                foreach (var item in message.contents_list.list)
                {
                    inventory.Add(item.m_iid, new Item(item.m_iid));
                    inventory[item.m_iid].containerID = containerID;
                }
            }
        }

        private void UpdateStackSize(BinaryReader messageDataReader)
        {
            CM_Inventory.UpdateStackSize message = CM_Inventory.UpdateStackSize.read(messageDataReader);

            uint ID = message.item;

            if (inventory.ContainsKey(ID))
            {
                inventory[ID].stackSize = message.amount;
                inventory[ID].value = message.newValue;
            }
            else if (otherObjects.ContainsKey(ID))
            {
                otherObjects[ID].stackSize = message.amount;
                otherObjects[ID].value = message.newValue;
            }
            else
            {
                otherObjects.Add(ID, new Item(ID));
                otherObjects[ID].stackSize = message.amount;
                otherObjects[ID].value = message.newValue;
            }
        }

        private void PutObjectIn3D(BinaryReader messageDataReader)
        {
            CM_Inventory.InventoryPutObjIn3D message = CM_Inventory.InventoryPutObjIn3D.read(messageDataReader);

            uint ID = message.ObjectID;

            if (inventory.ContainsKey(ID))
            {
                otherObjects.Add(ID, inventory[ID]);
                inventory.Remove(ID);
            }
            else if (packs.ContainsKey(ID))
            {
                Debug.Assert(false);
                otherObjects.Add(ID, packs[ID]);
                packs.Remove(ID);
            }
            else if (wielded.ContainsKey(ID))
            {
                otherObjects.Add(ID, wielded[ID]);
                wielded.Remove(ID);
            }
            else
            {
                Debug.Assert(false);
            }

        }

        private void AlwaysDrop()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string createdPath = "..\\Debug\\Data\\weenies\\";
            string cachedPath = "..\\Debug\\Data\\cached\\";
            IEnumerable<string> createdWeenies = Directory
                .EnumerateFiles(createdPath, "*.json", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName);
            IEnumerable<string> cachedWeenies = Directory
                .EnumerateFiles(cachedPath, "*.json", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName);

            // TODO: investigate named tuples

            // find all created weenies that match to inventory wcids, otherwise null
            IEnumerable<Tuple<Item, string>> createdWeenieMatches =
                from item in inventory.Values // TODO: need to fix, add wielded
                join createdWeenie in createdWeenies
                on item.wcid.ToString() + " " equals createdWeenie.Substring(0, createdWeenie.IndexOf(" ") + 1)
                into fileNames
                from fileName in fileNames.DefaultIfEmpty()
                select new Tuple<Item, string>(item, fileName == null ? null : createdPath + fileName);

            // find all cached weenies for inventory wcids that do not have created weenies
            IEnumerable<Tuple<Item, string>> cachedWeenieMatches =
                from fileMatch in createdWeenieMatches
                where fileMatch.Item2 == null
                join cachedWeenie in cachedWeenies
                on fileMatch.Item1.wcid.ToString("00000") equals cachedWeenie.Substring(0, 5)
                select new Tuple<Item, string>(fileMatch.Item1, cachedPath + cachedWeenie);

            // combine results
            IEnumerable<Tuple<Item, string>> fileNamePairs =
                from createdWeeniePair in createdWeenieMatches
                join cachedWeeniePair in cachedWeenieMatches
                on createdWeeniePair.Item1 equals cachedWeeniePair.Item1
                into createdWeenieNulls
                from createdWeenieNull in createdWeenieNulls.DefaultIfEmpty()
                select new Tuple<Item, string>(createdWeeniePair.Item1, createdWeeniePair.Item2 != null ? createdWeeniePair.Item2 : createdWeenieNull.Item2);

            foreach (Tuple<Item, string> fileNamePair in fileNamePairs)
            {
                fileNamePair.Item1.BondedStatus = GetBondedStatusFromJson(fileNamePair.Item2);
                if (fileNamePair.Item1.BondedStatus != BondedStatusEnum.Normal_BondedStatus)
                {
                    Console.WriteLine(fileNamePair.Item1.name);
                }
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        // grabs BONDED_INT quick and dirty
        private BondedStatusEnum GetBondedStatusFromJson(string filePath)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(File.OpenRead(filePath)));
            reader.Read(); // BeginObject
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    if (reader.Value.Equals("intStats"))
                    {
                        reader.Read(); // Token : StartArray
                        while (reader.Read() && reader.TokenType != JsonToken.EndArray) // Token: StartObject
                        {
                            reader.Read(); // "key"
                            reader.Read(); // 33
                            if ((long)reader.Value == 33)
                            {
                                reader.Read(); // "value"
                                return (BondedStatusEnum)(reader.ReadAsInt32() ?? 0);
                            }
                            reader.Read(); // "value"
                            reader.Read(); // different value
                            reader.Read(); // Token: EndObject
                        }
                        break;
                    }
                    else if (reader.Value.Equals("IntValues"))
                    {
                        reader.Read(); // Token: StartObject
                        while (reader.Read() && reader.TokenType != JsonToken.EndObject) // 
                        {
                            if (reader.TokenType == JsonToken.PropertyName && reader.Value.Equals("33"))
                            {
                                return (BondedStatusEnum)(reader.ReadAsInt32() ?? 0);
                            }
                            reader.Read(); // different property
                        }
                        break; // didn't find
                    }
                }
                else
                {
                    reader.Skip();
                }
            }
            return 0;
        }
    }
}
