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
            //HypothesisTester tester = new HypothesisTester();
            //InventoryReconstructor reconstructor = new InventoryReconstructor();
            //List<Tuple<Character, List<uint>, string>> foundDeaths = reconstructor.Reconstruct(fileName);
            //foreach (Tuple<Character, List<uint>, string> deathRecord in foundDeaths)
            //{
            //    tester.Test(deathRecord.Item1, deathRecord.Item2, deathRecord.Item3);
            //    Console.WriteLine();
            //}


            HypothesisTester tester = new HypothesisTester();

            string[] directoryFiles = Directory.GetFiles("C:\\Build\\deathreconstruction\\deathreconstruction\\bin\\Debug\\Data", "*.pcap");

            int i = 0;

            string startFileName = @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-22_1485152410_log.pcap";
            for (i = 0; i < directoryFiles.Length; i++)
            {
                if (directoryFiles[i].Equals(startFileName))
                {
                    break;
                }
            }

            for (; i < directoryFiles.Length; i++)
            {
                InventoryReconstructor reconstructor = new InventoryReconstructor();
                List<Tuple<Character, List<uint>, string>> foundDeaths = reconstructor.Reconstruct(directoryFiles[i]);

                bool success = true;
                foreach (Tuple<Character, List<uint>, string> deathRecord in foundDeaths)
                {
                    bool result = tester.Test(deathRecord.Item1, deathRecord.Item2, deathRecord.Item3);
                    success = success && result;
                    Console.WriteLine();
                }
                //if (!success)
                //{
                //    break;
                //}
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
