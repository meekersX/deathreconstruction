using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace deathreconstruction
{
    class HypothesisTester
    {
        private Character character;
        private List<uint> droppedItems;
        private string deathText;

        // things to test:
        // item ordering in message
        //  inventory position?
        //  always drop items?
        // pyreal rounding
        // wielded items
        // stack?

        public void Test(Character testCharacter, List<uint> testDroppedItems, string testDeathText)
        {
            character = testCharacter;
            droppedItems = testDroppedItems;
            deathText = testDeathText;

            Console.WriteLine(testDeathText);
            AlwaysDrop();
            foreach (uint itemID in droppedItems)
            {
                character.PrintItem(itemID);
            }
            Console.WriteLine(droppedItems.Count);
            // needs:
            // inventory snapshot
            // dropped items
            // dropped item string
            //HypothesisTester tester = new HypothesisTester(characterLevel, inventory, packs, wielded);
            //tester.Test();
            //            PrintInventoryByValue();
            ByClass();
        }

        private IEnumerable<Tuple<uint, Item>> SortedValue(bool includeWielded = true)
        {
            List<Item> inventory = character.GetInventory();

            List<Tuple<uint, Item>> valueMapping = new List<Tuple<uint, Item>>(inventory.Count + 8);

            Console.WriteLine("Total Pyreals: " + TotalPyreals());
            foreach (var item in inventory)
            {
                if (includeWielded || item.WielderID == character.ID)
                {
                    if (item.BondedStatus == BondedStatusEnum.Normal_BondedStatus)
                    {
                        if (item.Type != ITEM_TYPE.TYPE_MONEY && item.Type != ITEM_TYPE.TYPE_PROMISSORY_NOTE)
                        {
                            valueMapping.Add(new Tuple<uint, Item>(item.Value / item.StackSize, item));
                        }
                    }
                }
            }

            return valueMapping.OrderByDescending(x => x.Item1);
        }

        private uint TotalPyreals()
        {
            uint totalPyreals = 0;

            foreach (var item in character.GetInventory())
            {
                if (item.Name.Equals("Pyreal"))
                {
                    totalPyreals += item.StackSize;
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
                if (seenTypes.Contains(item.Item2.Type))
                {
                    classified.Add(new Tuple<uint, Item>(item.Item1 / 2, item.Item2));
                }
                else
                {
                    classified.Add(new Tuple<uint, Item>(item.Item1, item.Item2));
                }
                seenTypes.Add(item.Item2.Type);
            }

            int i = 1;
            foreach (var item in classified.OrderByDescending(x => x.Item1).Take(20))
            {
                Console.WriteLine(i++ + ".\t" + item.Item1 + "\t" + item.Item2.Name + " " + item.Item2.Type);
            }
        }

        private void AlwaysDrop()
        {
            // TODO: investigate named tuples
            // TODO: cache results
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

            // find all created weenies that match to inventory wcids, otherwise null
            IEnumerable<Tuple<Item, string>> createdWeenieMatches =
                from item in character.GetInventory()
                join createdWeenie in createdWeenies
                on item.Wcid.ToString() + " " equals createdWeenie.Substring(0, createdWeenie.IndexOf(" ") + 1)
                into fileNames
                from fileName in fileNames.DefaultIfEmpty()
                select new Tuple<Item, string>(item, fileName == null ? null : createdPath + fileName);

            // find all cached weenies for inventory wcids that do not have created weenies
            IEnumerable<Tuple<Item, string>> cachedWeenieMatches =
                from fileMatch in createdWeenieMatches
                where fileMatch.Item2 == null
                join cachedWeenie in cachedWeenies
                on fileMatch.Item1.Wcid.ToString("00000") equals cachedWeenie.Substring(0, 5)
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
                    //Console.WriteLine(fileNamePair.Item1.Name);
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
