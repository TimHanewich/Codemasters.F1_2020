using System;

namespace Codemasters.F1_2020.Analysis
{
    public class TrackLocation
    {
        //Basic info
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public byte Sector { get; set; }

        //Optimums
        public float OptimalSpeedMph {get; set;} //Probably a major point of interest
        public sbyte OptimalGear {get; set;} //Probably a major point of interest
        public float OptimalSteer {get; set;}
        public float OptimalThrottle {get; set;}
        public float OptimalBrake {get; set;}
    }
}