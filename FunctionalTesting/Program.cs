using System;
using Codemasters.F1_2020;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Codemasters.F1_2020.Analysis;
using TimHanewichToolkit;
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
            Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(data);
            Console.WriteLine("Deserializing complete");


            SessionAnalysis sa = new SessionAnalysis();
            Console.WriteLine("Generating session analysis.");
            sa.Load(packets, packets[0].PlayerCarIndex);
            Console.WriteLine("Analysis complete.");
            

            foreach (LapAnalysis la in sa.Laps)
            {
                Console.WriteLine("Lap " + la.LapNumber.ToString());
                Console.WriteLine("T:" + la.PercentOnThrottle.ToString("#0.0%"));
                Console.WriteLine("B:" + la.PercentOnBrake.ToString("#0.0%"));
                Console.WriteLine("C:" + la.PercentCoasting.ToString("#0.0%"));
                Console.WriteLine("O:" + la.PercentThrottleBrakeOverlap.ToString("#0.0%"));
                Console.ReadLine();
            }


            

            // //Write to file
            // string as_json = JsonConvert.SerializeObject(sa);
            // System.IO.File.WriteAllText("C:\\Users\\TaHan\\Downloads\\sa.txt", as_json);




        }
    }
}
