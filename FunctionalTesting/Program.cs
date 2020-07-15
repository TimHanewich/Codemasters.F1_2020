using System;
using Codemasters.F1_2020;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Codemasters.F1_2020.Analysis;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream s = System.IO.File.OpenRead("C:\\Users\\tihanewi\\Downloads\\Codemasters.F1_2020\\SampleData\\Australia_Practice_AlphaTauri.txt");
            StreamReader sr = new StreamReader(s);
            JsonTextReader jtr = new JsonTextReader(sr);
            JsonSerializer js = new JsonSerializer();
            List<byte[]> data = js.Deserialize<List<byte[]>>(jtr);

            Console.WriteLine("Converting...");
            Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(data);
            Console.WriteLine("Done converting");

            Console.WriteLine("Making frames...");
            PacketFrame[] frames = PacketFrame.CreateAll(packets);
            Console.WriteLine("Done. " + frames.Length.ToString());
        }
    }
}
