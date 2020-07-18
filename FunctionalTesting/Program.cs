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

            Console.WriteLine("DesSerializing...");
            Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(data);
            Console.WriteLine("Deserializing complete");


            SessionAnalysis sa = new SessionAnalysis();
            Console.WriteLine("Generating session analysis.");
            sa.Load(packets, packets[0].PlayerCarIndex);
            
            string as_json = JsonConvert.SerializeObject(sa);

            System.IO.File.WriteAllText("C:\\Users\\TaHan\\Downloads\\sa.txt", as_json);




        }
    }
}
