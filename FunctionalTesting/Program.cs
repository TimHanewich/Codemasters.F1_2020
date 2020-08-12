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
            string path = "C:\\Users\\TaHan\\Downloads\\Silverstone.csv";
            string content = System.IO.File.ReadAllText(path);
            TrackDataContainer tdl = TrackDataContainer.LoadFromCsvContent(Track.Silverstone, content);
            Console.WriteLine(JsonConvert.SerializeObject(tdl));


        }
    }
}
