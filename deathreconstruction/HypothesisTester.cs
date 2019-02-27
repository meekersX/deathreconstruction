using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace deathreconstruction
{
    class HypothesisTester
    {
        private Character character;
        private List<uint> droppedItemIDs;
        private string deathText;

        // things to test:
        // item ordering in message
        //  inventory position?
        //  always drop items?
        // pyreal rounding
        // wielded items
        // stack?
        // need PK status

        public bool Test(Character testCharacter, List<uint> testdroppedItemIDs, string testDeathText)
        {
            character = testCharacter;
            droppedItemIDs = testdroppedItemIDs;
            deathText = testDeathText;

            //foreach (uint itemID in droppedItemIDs)
            //{
            //    character.FindItem(itemID).Print();
            //}

            Console.WriteLine(testDeathText);
            AlwaysDrop();
            return ByClass();
        }

        private IEnumerable<Tuple<uint, Item>> SortedValue(bool includeWielded = true)
        {
            List<Item> inventory = character.GetInventory();

            List<Tuple<uint, Item>> valueMapping = new List<Tuple<uint, Item>>(inventory.Count);

            foreach (var item in inventory)
            {
                if (includeWielded || item.WielderID != character.ID)
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

        private bool ByClass()
        {
            int i;

            List<Item> droppedItems = new List<Item>(droppedItemIDs.Count);
            foreach (uint droppedItemID in droppedItemIDs)
            {
                droppedItems.Add(character.FindItem(droppedItemID));
            }

            string pattern = @"^You've lost ([1-9][0-9]{0,2}(?:,[0-9]{3})*) Pyreals?,?(?: your ([^,]+),)*(?: and your ([^,]+)){0,1}!$";
            Match match = Regex.Match(deathText, pattern);

            List<string> parsedItems = new List<string>(droppedItemIDs.Count + 1);
            for (i = 1; i < match.Groups.Count; i++)
            {
                foreach (Capture capture in match.Groups[i].Captures)
                {
                    parsedItems.Add(capture.Value);
                }
            }

            uint droppedPyreals = 0;

            if (character.Level > 5)
            {
                droppedPyreals = (TotalPyreals() + 1) / 2;
            }

            // verify pyreal count
            if (!droppedPyreals.ToString("N0").Equals(parsedItems[0]))
            {
                Console.WriteLine("Pyreals predicted: " + droppedPyreals.ToString("N0") + " actual: " + parsedItems[0]);
                return false;
            }
            Console.WriteLine("Pyreals good");
            parsedItems.RemoveAt(0);

            // check we found all death drops
            if (droppedItems.Count != parsedItems.Count)
            {
                Console.WriteLine("Didn't find all the dropped items, found: " + droppedItems.Count + " actual: " + parsedItems.Count);
                return false;
            }
            Console.WriteLine("Found all dropped items");

            // check found in same order and their naming in death string
            // TODO: fix for pluralization
            parsedItems.Reverse();
            bool nameOrdering = true;
            for (i = 0; i < droppedItems.Count; i++)
            {
                if (droppedItems[i].Name != parsedItems[i])
                {
                    Console.WriteLine("Name predicted: " + droppedItems[i].Name + " actual: " + parsedItems[i]);
                    nameOrdering = false;
                }
            }
            if (nameOrdering)
            {
                Console.WriteLine("Names good");
            }
            else
            {
                return false;
            }

            bool wielded = false;
            if (character.Level > 35)
            {
                wielded = true;
            }
            // ensure no wielded if specified
            if (!wielded)
            {
                foreach (Item item in droppedItems)
                {
                    if (item.WielderID == character.ID)
                    {
                        Console.Write("No wielded drops predicted, but dropped: ");
                        item.Print();
                        return false;
                    }
                }
            }
            Console.WriteLine("Wielded good");

            // ensure always dropped or destroyed
            // TODO: determine ordering of always drops
            List<Item> inventory = character.GetInventory();

            // check by more than count?
            int numberAlwaysDropped = 0;
            List<Item> alwaysDroppedItem = new List<Item>();
            foreach (Item item in inventory)
            {
                if (item.BondedStatus == BondedStatusEnum.Slippery_BondedStatus || item.BondedStatus == BondedStatusEnum.Destroy_BondedStatus)
                {
                    alwaysDroppedItem.Add(item);
                    numberAlwaysDropped++;
                }
            }
            if (numberAlwaysDropped > droppedItems.Count)
            {
                Console.WriteLine("More always drop predicted than drops");
                return false;
            }
            for (i = 0; i < numberAlwaysDropped; i++)
            {
                if (droppedItems[i].BondedStatus == BondedStatusEnum.Normal_BondedStatus)
                {
                    Console.WriteLine("Always drop predicted, but normal drop found");
                    foreach (Item item in alwaysDroppedItem)
                    {
                        item.Print();
                    }
                    return false;
                }
            }
            for (; i < droppedItems.Count; i++)
            {
                if (droppedItems[i].BondedStatus != BondedStatusEnum.Normal_BondedStatus)
                {
                    Console.WriteLine("Normal drop predicted, but always drop found");
                    return false;
                }
            }
            Console.WriteLine("Always drops good");

            // work only with normal drops
            droppedItems.RemoveRange(0, numberAlwaysDropped);
            parsedItems.RemoveRange(0, numberAlwaysDropped);

            // ensure number of normal dropped in range
            // TODO: adjust for PVP deaths
            int droppedItemsMin;
            int droppedItemsMax;
            if (character.Level <= 5)
            {
                droppedItemsMin = 0;
                droppedItemsMax = 0;
            }
            else if (character.Level <= 20)
            {
                droppedItemsMin = 1;
                droppedItemsMax = 1;
            }
            else // character.Level > 20
            {
                droppedItemsMin = character.Level / 20;
                droppedItemsMax = droppedItemsMin + 2;
            }

            if (droppedItems.Count < droppedItemsMin || droppedItems.Count > droppedItemsMax)
            {
                Console.WriteLine("Number of normal drops range predicted: (" + droppedItemsMin + ", " + droppedItemsMax + ") actual: " + droppedItems.Count);
                return false;
            }
            Console.WriteLine("Number of normal drops good");

            // ensure items dropped and ordering possible by value

            // get inventory sorted by value
            // TODO: think more about stacking items
            List<Tuple<uint, Item>> valueMapping = new List<Tuple<uint, Item>>(SortedValue(wielded));

            HashSet<ITEM_TYPE> seenTypes = new HashSet<ITEM_TYPE>();
            int ordinal = 1;
            foreach (Item droppedItem in droppedItems)
            {
                double droppedValue = (droppedItem.Value / droppedItem.StackSize) * 1.1;
                if (seenTypes.Contains(droppedItem.Type))
                {
                    droppedValue /= 2;
                }
                seenTypes.Add(droppedItem.Type);

                // assure all items not dropped before this one could have a lesser value, accounting for randomness, and seen types
                foreach (Tuple<uint, Item> currentItem in valueMapping)
                {
                    if (currentItem.Item2 == droppedItem)
                    {
                        valueMapping.Remove(currentItem);
                        ordinal++;
                        break;
                    }

                    double currentValue = currentItem.Item1 * 0.9;
                    if (seenTypes.Contains(currentItem.Item2.Type))
                    {
                        currentValue /= 2;
                    }

                    if (currentValue > droppedValue)
                    {
                        Console.WriteLine(currentItem.Item2.Name + " is worth more than " + droppedItem.Name);
                        return false;
                    }
                }

            }
            Console.WriteLine("Dropped items good");

            return true;
        }

        private void AlwaysDrop()
        {
            // wcids where cache.bin has bad data
            Dictionary<uint, BondedStatusEnum> badWcids = new Dictionary<uint, BondedStatusEnum>
            {
                [30244] = BondedStatusEnum.Slippery_BondedStatus
            };

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
                if (badWcids.ContainsKey(fileNamePair.Item1.Wcid))
                {
                    fileNamePair.Item1.BondedStatus = badWcids[fileNamePair.Item1.Wcid];
                }
                else
                {
                    fileNamePair.Item1.BondedStatus = GetBondedStatusFromJson(fileNamePair.Item2);
                }

                //if (fileNamePair.Item1.BondedStatus != BondedStatusEnum.Normal_BondedStatus)
                //{
                //    Console.WriteLine(fileNamePair.Item1.Name);
                //}
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
