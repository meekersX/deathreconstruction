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


            //string[] directoryFiles = Directory.GetFiles("C:\\Build\\deathreconstruction\\deathreconstruction\\bin\\Debug\\Data", "*.pcap");
            string[] directoryFiles =
            {
                @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-22_1485046869_log.pcap",
                @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-29_1485735010_log.pcap",
                @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-29_1485741839_log.pcap",
            };

            int i = 0;

            //string startFileName = @"C:\Build\deathreconstruction\deathreconstruction\bin\Debug\Data\pkt_2017-1-31_1485843405_log.pcap";
            //for (i = 0; i < directoryFiles.Length; i++)
            //{
            //    if (directoryFiles[i].Equals(startFileName))
            //    {
            //        break;
            //    }
            //}

            using (StreamWriter output = new StreamWriter("output.txt"))
            {
                HypothesisTester tester = new HypothesisTester(output);

                for (; i < directoryFiles.Length; i++)
                {
                    InventoryReconstructor reconstructor = new InventoryReconstructor();
                    List<Tuple<Character, List<uint>, string>> foundDeaths = reconstructor.Reconstruct(directoryFiles[i]);
                    output.WriteLine(directoryFiles[i]);

                    bool success = true;
                    foreach (Tuple<Character, List<uint>, string> deathRecord in foundDeaths)
                    {
                        bool result = tester.Test(deathRecord.Item1, deathRecord.Item2, deathRecord.Item3);
                        success = success && result;
                    }
                    //if (!success)
                    //{
                    //    break;
                    //}
                    output.WriteLine();
                    output.Flush();
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
