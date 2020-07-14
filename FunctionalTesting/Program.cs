using System;
using Codemasters.F1_2020;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream s = System.IO.File.OpenRead("C:\\Users\\tihanewi\\Downloads\\Codemasters.F1_2020\\SampleData\\Australia_Race_AlphaTauri.txt");
            StreamReader sr = new StreamReader(s);
            JsonTextReader jtr = new JsonTextReader(sr);
            JsonSerializer js = new JsonSerializer();
            List<byte[]> data = js.Deserialize<List<byte[]>>(jtr);

            foreach (byte[] b in data)
            {
                PacketType pt = CodemastersToolkit.GetPacketType(b);
                if (pt == PacketType.FinalClassification)
                {
                    Console.WriteLine("Got a final class packet");
                    FinalClassificationPacket fcp = new FinalClassificationPacket();
                    fcp.LoadBytes(b);
                    foreach (FinalClassificationPacket.FinalClassificationData fcd in fcp.FieldClassificationData)
                    {
                        Console.WriteLine(fcd.NumberOfLaps.ToString());
                    }
                }
            }
        }
    }
}
