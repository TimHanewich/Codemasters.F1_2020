using System;
using Codemasters.F1_2020;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Codemasters.F1_2020.Analysis;
using TimHanewich.Toolkit;
using System.Threading.Tasks;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream s = System.IO.File.OpenRead(args[0].Replace("\"", ""));
            StreamReader sr = new StreamReader(s);
            JsonTextReader jtr = new JsonTextReader(sr);
            JsonSerializer js = new JsonSerializer();
            List<byte[]> data = js.Deserialize<List<byte[]>>(jtr);

            Console.WriteLine("DesSerializing...");
            Packet[] packets = Packet.BulkLoadAllSessionData(data);
            Console.WriteLine("Deserializing complete");

            SessionAnalysis sa = new SessionAnalysis();
            Task.Run(() => sa.Load(packets, packets[0].PlayerCarIndex));

            while (sa.LoadComplete == false)
            {
                Console.Write("\r" + sa.PercentLoadComplete.ToString());
                Task.Delay(50).Wait();
            }

            Console.WriteLine("Done!");
            

        }
    }
}
