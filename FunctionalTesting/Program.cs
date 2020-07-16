using System;
using Codemasters.F1_2020;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Codemasters.F1_2020.Analysis;
using TimHanewichToolkit;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream s = System.IO.File.OpenRead("C:\\Users\\TaHan\\Downloads\\Codemasters.F1_2020\\SampleData\\Australia_Practice_AlphaTauri.txt");
            StreamReader sr = new StreamReader(s);
            JsonTextReader jtr = new JsonTextReader(sr);
            JsonSerializer js = new JsonSerializer();
            List<byte[]> data = js.Deserialize<List<byte[]>>(jtr);

            HanewichTimer ht = new HanewichTimer();
            ht.StartTimer();

            Console.WriteLine("Serializing...");
            Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(data);
            PacketFrame[] frames = PacketFrame.CreateAll(packets);

            //Printing
            TelemetryAnalysisEngine tae = TelemetryAnalysisEngine.Create(frames);
            Console.WriteLine("Printing...");
            string content = tae.PrintTelemetryToCsvContent(packets[0].PlayerCarIndex);


            Console.WriteLine("Writing to file...");
            System.IO.File.WriteAllText("C:\\Users\\TaHan\\Downloads\\data.csv", content);
            Console.WriteLine("Done!");

            ht.StopTimer();

            Console.WriteLine("Total time: " + ht.GetElapsedTime().TotalSeconds.ToString());



        }
    }
}
