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

            foreach (byte[] b in data)
            {
                PacketType pt = CodemastersToolkit.GetPacketType(b);
                if (pt == PacketType.Participants)
                {
                    ParticipantPacket pp = new ParticipantPacket();
                    pp.LoadBytes(b);
                    foreach (ParticipantPacket.ParticipantData pd in pp.FieldParticipantData)
                    {
                        Console.WriteLine(pd.Name);
                    }
                    Console.ReadLine();
                }
            }
        }
    }
}
