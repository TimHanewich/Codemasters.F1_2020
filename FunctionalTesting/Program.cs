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
                Packet p = new Packet();
                p.LoadBytes(b);

                Console.WriteLine(p.PacketFormat.ToString());
                Console.WriteLine(p.GameMajorVersion.ToString());
                Console.WriteLine(p.GameMinorVersion.ToString());
                Console.WriteLine(p.PlayerCarIndex.ToString());
                Console.WriteLine(p.SecondaryPlayerCarIndex.ToString());
                Console.WriteLine(p.PacketType.ToString());
                Console.WriteLine(p.UniqueSessionId.ToString());
                Console.ReadLine();
            }
        }
    }
}
