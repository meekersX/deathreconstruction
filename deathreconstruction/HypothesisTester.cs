using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deathreconstruction
{
    class HypothesisTester
    {
        private readonly Dictionary<uint, Item> inventory = new Dictionary<uint, Item>();
        private readonly Dictionary<uint, Item> packs = new Dictionary<uint, Item>();
        private readonly Dictionary<uint, Item> wielded = new Dictionary<uint, Item>();
        private readonly int characterLevel = 0;

        public HypothesisTester(in int createdCharacterLevel, in Dictionary<uint, Item> createdInventory, in Dictionary<uint, Item> createdPacks, in Dictionary<uint, Item> createdWielded)
        {
            inventory = createdInventory;
            packs = createdPacks;
            wielded = createdWielded;
            characterLevel = createdCharacterLevel;
        }

        // things to test:
        // item ordering in message
        //  inventory position?
        //  always drop items?
        // pyreal rounding
        // wielded items
        // stack?

        public void Test()
        {
//            PrintInventoryByValue();
            ByClass();
        }

        private void PrintInventoryByValue()
        {
            foreach (var item in SortedValue())
            {
                item.Item2.Print2();
            }
        }

        private IEnumerable<Tuple<uint, Item>> SortedValue(bool includeWielded = true)
        {
            List<Tuple<uint, Item>> valueMapping = new List<Tuple<uint, Item>>(inventory.Count + wielded.Count + packs.Count);

            Console.WriteLine("Total Pyreals: " + TotalPyreals());
            foreach (var item in inventory.Values)
            {
                if (item.BondedStatus == BondedStatusEnum.Normal_BondedStatus)
                {
                    if (item.type != ITEM_TYPE.TYPE_MONEY && item.type != ITEM_TYPE.TYPE_PROMISSORY_NOTE)
                    {
                        valueMapping.Add(new Tuple<uint, Item>(item.value / item.stackSize, item));
                    }
                }
            }
            if (includeWielded)
            {
                foreach (var item in wielded.Values)
                {
                    if (item.BondedStatus == BondedStatusEnum.Normal_BondedStatus)
                    {
                        valueMapping.Add(new Tuple<uint, Item>(item.value / item.stackSize, item));
                    }
                }
            }

            return valueMapping.OrderByDescending(x => x.Item1);
        }

        private uint TotalPyreals()
        {
            uint totalPyreals = 0;

            foreach (var item in inventory.Values)
            {
                if (item.name == "Pyreal")
                {
                    totalPyreals += item.stackSize;
                }
            }

            return totalPyreals;
        }

        private void CurrentGDLE()
        {

        }

        private void ByClass()
        {
            HashSet<ITEM_TYPE> seenTypes = new HashSet<ITEM_TYPE>();
            IEnumerable<Tuple<uint, Item>> valueMapping = SortedValue();
            List<Tuple<uint, Item>> classified = new List<Tuple<uint, Item>>(valueMapping.Count());

            foreach (var item in valueMapping)
            {
                if (seenTypes.Contains(item.Item2.type))
                {
                    classified.Add(new Tuple<uint, Item>(item.Item1 / 2, item.Item2));
                }
                else
                {
                    classified.Add(new Tuple<uint, Item>(item.Item1, item.Item2));
                }
                seenTypes.Add(item.Item2.type);
            }

            int i = 1;
            foreach (var item in classified.OrderByDescending(x => x.Item1))
            {
                Console.WriteLine(i++ + ".\t" + item.Item1 + "\t" + item.Item2.name + " " + item.Item2.type);
            }
        }

        private void ByName()
        {

        }
    }
}
