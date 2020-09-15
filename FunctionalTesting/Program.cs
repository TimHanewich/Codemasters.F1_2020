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
            string path = "C:\\Users\\TaHan\\Downloads\\Codemasters.F1_2020\\SampleData\\Australia_Practice_AlphaTauri.json";
            string content = System.IO.File.ReadAllText(path);

            List<byte[]> bytes = JsonConvert.DeserializeObject<List<byte[]>>(content);
            Packet[] packets = Packet.BulkLoadAllSessionData(bytes);
            SessionAnalysis sa = new SessionAnalysis();
            Console.WriteLine("Loading...");
            sa.Load(packets, packets[0].PlayerCarIndex);

            foreach (LapAnalysis la in sa.Laps)
            {
                Console.WriteLine(la.EquippedTyreCompound.ToString() + " - " + la.IncrementalTyreWear_FrontLeft.ToString() + " " + la.IncrementalTyreWear_FrontRight.ToString() + " " + la.IncrementalTyreWear_RearLeft.ToString() + " " + la.IncrementalTyreWear_RearRight.ToString());
            }
        }
    }
}
