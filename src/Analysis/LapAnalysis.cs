using System;

namespace Codemasters.F1_2020.Analysis
{
    public class LapAnalysis
    {
        public byte LapNumber {get; set;}
        public CornerAnalysis[] Corners {get; set;} 
        public float Sector1Time {get; set;}
        public float Sector2Time {get; set;}
        public float Sector3Time {get; set;}
        public float LapTime {get; set;}
        public bool LapInvalid {get; set;}

        public float FuelConsumed {get; set;}
    }
}