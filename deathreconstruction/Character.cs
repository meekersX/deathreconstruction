using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace deathreconstruction
{
    class Character
    {
        public uint ID;
        public int Level;
        public string Name;
        public int DeathAugmentations;

        private Dictionary<uint, Item> inventory;
        private Dictionary<uint, Item> packs;

        private List<uint> unloadedItems;
        bool initialItemsPrinted;

        public Character()
        {
            ID = 0x0;
            Level = 0;
            Name = "";
            DeathAugmentations = 0;
            inventory = new Dictionary<uint, Item>();
            packs = new Dictionary<uint, Item>();
            unloadedItems = new List<uint>();
            initialItemsPrinted = false;
        }

        public Character (Character characterToCopy)
        {
            ID = characterToCopy.ID;
            Level = characterToCopy.Level;
            Name = characterToCopy.Name;
            DeathAugmentations = characterToCopy.DeathAugmentations;
            inventory = characterToCopy.inventory.ToDictionary(x => x.Key, x => x.Value.Copy());
            packs = characterToCopy.packs.ToDictionary(x => x.Key, x => x.Value.Copy());

            Debug.Assert(characterToCopy.unloadedItems.Count == 0);
        }

        public void AddItem(Item item, bool pack = false)
        {
            if (pack)
            {
                packs.Add(item.ID, item);
            }
            else
            {
                inventory.Add(item.ID, item);
            }
        }

        public Item AddItem(CM_Inventory.ContentProfile item)
        {
            Item addedItem = new Item(item.m_iid);
            if (item.m_uContainerProperties == (uint)ContainerProperties.None)
            {
                if (!inventory.ContainsKey(item.m_iid))
                {
                    inventory.Add(item.m_iid, addedItem);
                    unloadedItems.Add(item.m_iid);
                }
            }
            else
            {
                if (inventory.ContainsKey(item.m_iid))
                {
                    packs.Add(item.m_iid, inventory[item.m_iid]);
                    inventory.Remove(item.m_iid);
                }
                else if (!packs.ContainsKey(item.m_iid))
                {
                    packs.Add(item.m_iid, addedItem);
                    unloadedItems.Add(item.m_iid);
                }
            }

            return addedItem;
        }

        public void AddWieldedItem(CM_Login.InventoryPlacement item)
        {
            Item oldItem;
            if (inventory.TryGetValue(item.iid_, out oldItem))
            {
                oldItem.WielderID = ID;
            }
            else
            {
                inventory[item.iid_] = new Item(item.iid_, ID);
                unloadedItems.Add(item.iid_);
            }
        }

        public void UpdateItem(Item item, bool pack = false)
        {
            if (inventory.ContainsKey(item.ID))
            {
                if (pack)
                {
                    packs[item.ID] = inventory[item.ID];
                    inventory.Remove(item.ID);
                }
                else
                {
                    inventory[item.ID] = item;
                }
            }
            else if (packs.ContainsKey(item.ID))
            {
                packs[item.ID] = item;
            }
            else
            {
                Debug.Assert(item.ContainerID == ID || item.WielderID == ID);
                AddItem(item, pack);
            }
            unloadedItems.Remove(item.ID);
            if (!initialItemsPrinted && unloadedItems.Count == 0)
            {
                //PrintInventory();
                initialItemsPrinted = true;
            }
        }

        public bool UpdateStackSize(uint itemID, uint stackSize, uint value)
        {
            if (inventory.ContainsKey(itemID))
            {
                inventory[itemID].StackSize = stackSize;
                inventory[itemID].Value = value;
                return true;
            }
            return false;
        }

        public void WieldItem(uint itemID)
        {
            Debug.Assert(inventory.ContainsKey(itemID));
            Item item = inventory[itemID];
            item.WielderID = ID;
            item.ContainerID = ID;
        }

        public Item FindItem(uint itemID)
        {
            if (inventory.ContainsKey(itemID))
            {
                return inventory[itemID];
            }
            else if (packs.ContainsKey(itemID))
            {
                return packs[itemID];
            }
            return null;
        }

        public bool Contains(uint containerID)
        {
            if (containerID == ID || packs.ContainsKey(containerID))
            {
                return true;
            }
            return false;
        }

        public Item RemoveItem(uint itemID)
        {
            Item item = FindItem(itemID);

            if (item != null)
            {
                if (inventory.Remove(itemID))
                {
                }
                else if (packs.Remove(itemID))
                {
                    Debug.Assert(false);
                    foreach (Item itemInPack in inventory.Values)
                    {
                        if (itemInPack.ContainerID == itemID)
                        {
                            inventory.Remove(ID);
                            // move to otherobjects?
                        }
                    }
                }
                return item;
            }
            return null;
        }

        public void AddItemToContainer(CM_Inventory.ContentProfile item, uint containerID)
        {
            Item addedItem = AddItem(item);
            addedItem.ContainerID = containerID;
            unloadedItems.Remove(item.m_iid);
        }

        public void AddItemToContainer(Item item, uint containerID)
        {
            AddItem(item);
            item.ContainerID = containerID;
        }

        public bool MoveItem(uint itemID, uint containerID, out Item item)
        {
            if (inventory.TryGetValue(itemID, out item))
            {
                // new container
                item.ContainerID = containerID;
                // unwield
                item.WielderID = 0x0;
                if (containerID == ID || packs.ContainsKey(containerID))
                {
                    // still within inventory, done
                    item = null;
                }
                else
                {
                    // removed from inventory
                    inventory.Remove(itemID);
                }
                return true;
            }
            return false;
        }

        public List<Item> GetInventory()
        {
            return new List<Item>(inventory.Values);
        }

        public void PrintInventory()
        {
            Console.WriteLine("Main Pack");
            foreach (Item item in inventory.Values)
            {
                Debug.Assert(item.WielderID == ID || item.WielderID == 0x0);
                if (item.ContainerID == ID && item.WielderID != ID)
                {
                    item.Print();
                }
            }
            foreach (Item pack in packs.Values)
            {
                Console.WriteLine("Pack " + pack.Name);
                foreach (Item item in inventory.Values)
                {
                    if (item.ContainerID == pack.ID)
                    {
                        item.Print();
                    }
                }
            }
            Console.WriteLine("Wielded");
            foreach (Item item in inventory.Values)
            {
                if (item.WielderID == ID)
                {
                    item.Print();
                }
            }
        }

        public void PrintItem(uint itemID)
        {
            if (inventory.ContainsKey(itemID))
            {
                if (inventory[itemID].WielderID != 0x0)
                {
                    Console.Write("W ");
                }
                else
                {
                    Console.Write("I ");
                }
                inventory[itemID].Print();
            }
            else if (packs.ContainsKey(itemID))
            {
                Console.Write("P ");
                packs[itemID].Print();
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
