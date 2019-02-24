using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace deathreconstruction
{
    class Program
    {
        static void Main(string[] args)
        {
            //int totalHits = 0;
            //StreamWriter writer = new StreamWriter("output2.txt");

            //foreach (var fileName in Directory.GetFiles("C:\\Build\\deathreconstruction\\deathreconstruction\\bin\\Debug\\Data", "*.pcap"))
            //{
            //    int hits = ProcessFile(fileName, writer);
            //    if (hits > 0)
            //    {
            //        totalHits += hits;
            //    }
            //    writer.Flush();

            //}
            //writer.WriteLine("Total Hits: " + totalHits);
            //writer.Close();


            Util.initReaders();
            string fileName = "..\\Debug\\Data\\pkt_2017-1-15_1484535255_log.pcap";
            InventoryReconstructor reconstructor = new InventoryReconstructor();
            reconstructor.Reconstruct(fileName);
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
