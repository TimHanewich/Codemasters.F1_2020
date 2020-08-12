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
            TrackDataContainer tdc = TrackDataContainer.LoadTrack(Track.Silverstone);
            TrackDataContainer tdc2 = TrackDataContainer.LoadFromCsvContent(Track.Silverstone, tdc.AsCsvContent());
            Console.WriteLine(JsonConvert.SerializeObject(tdc2));

        }
    }
}
