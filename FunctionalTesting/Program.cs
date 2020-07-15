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
            Stream s = System.IO.File.OpenRead("C:\\Users\\TaHan\\Downloads\\Telemetry 7-14-2020 542a12b8-0ccc-4ce2-8975-9d5d8f9381a9.json");
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
