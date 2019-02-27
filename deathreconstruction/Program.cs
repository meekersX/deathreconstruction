using System;
using System.IO;
using System.Collections.Generic;

namespace deathreconstruction
{
    class Program
    {
        static void Main(string[] args)
        {
            Util.initReaders();

            //string fileName = @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-15_1484535255_log.pcap";
            //string fileName = @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\OlthoiPlayArwic.pcap";
            //string fileName = @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-10_1484026681_log.pcap";
            //HypothesisTester tester = new HypothesisTester();
            //InventoryReconstructor reconstructor = new InventoryReconstructor();
            //List<Tuple<Character, List<uint>, string>> foundDeaths = reconstructor.Reconstruct(fileName);
            //foreach (Tuple<Character, List<uint>, string> deathRecord in foundDeaths)
            //{
            //    tester.Test(deathRecord.Item1, deathRecord.Item2, deathRecord.Item3);
            //}


            HypothesisTester tester = new HypothesisTester();
            int i = 0;
            foreach (string fileName in Directory.GetFiles("C:\\Build\\deathreconstruction\\deathreconstruction\\bin\\Debug\\Data", "*.pcap"))
            {
                InventoryReconstructor reconstructor = new InventoryReconstructor();
                List<Tuple<Character, List<uint>, string>> foundDeaths = reconstructor.Reconstruct(fileName);

                bool success = true;
                foreach (Tuple<Character, List<uint>, string> deathRecord in foundDeaths)
                {
                    bool result = tester.Test(deathRecord.Item1, deathRecord.Item2, deathRecord.Item3);
                    success = success || result;
                }
                i++;
                if (!success)
                {
                    break;
                }
            }
        }

        //static void processDirectoryRecursively(string path)
        //{
        //    //            Console.WriteLine("Current Directory: " + path);
        //    foreach (var directoryName in Directory.EnumerateDirectories(path))
        //    {
        //        processDirectoryRecursively(directoryName);
        //    }
        //    foreach (var fileName in Directory.GetFiles(path, "*.pcap"))
        //    {
        //        Console.WriteLine("Current File: " + fileName);
        //        if (ProcessFile(fileName) > 0)
        //        {
        //            try
        //            {
        //                Console.WriteLine(fileName);
        //                string name = fileName.Substring(path.Length + 1);
        //                File.Copy(fileName, Path.Combine("C:\\Build\\deathreconstruction\\deathreconstruction\\bin\\Debug\\Data", name));
        //                break;
        //            }
        //            catch (IOException copyError)
        //            {
        //                Console.WriteLine(copyError.Message);
        //                continue;
        //            }

        //        }

        //    }
        //}
    }
}
