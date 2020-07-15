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
            Stream s = System.IO.File.OpenRead("C:\\Users\\tihanewi\\Downloads\\Codemasters.F1_2020\\SampleData\\Australia_Practice_AlphaTauri.txt");
            StreamReader sr = new StreamReader(s);
            JsonTextReader jtr = new JsonTextReader(sr);
            JsonSerializer js = new JsonSerializer();
            List<byte[]> data = js.Deserialize<List<byte[]>>(jtr);

            Console.WriteLine("Converting...");
            Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(data);
            List<string> ToWrite = new List<string>();
            foreach (Packet p in packets)
            {
                if (p.PacketType == PacketType.CarTelemetry)
                {
                    MotionPacket mp = (MotionPacket)p.GetRelatedPacket(packets, PacketType.Motion);
                    MotionPacket.CarMotionData cmd = mp.FieldMotionData[mp.PlayerCarIndex];
                    ToWrite.Add(mp.SessionTime.ToString() + "," + cmd.gForceLateral.ToString() + "," + cmd.gForceLongitudinal.ToString() + "," + cmd.gForceVertical.ToString());
                }
            }

            string all = "";
            foreach (string ss in ToWrite)
            {
                all = all + ss + Environment.NewLine;
            }

            System.IO.File.WriteAllText("C:\\Users\\tihanewi\\Downloads\\test.csv", all);
        }
    }
}
