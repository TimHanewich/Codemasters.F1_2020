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
        public float PercentOnThrottle {get; set;}
        public float PercentOnBrake {get; set;}
        public float PercentCoasting {get; set;}
        public float PercentThrottleBrakeOverlap {get; set;}
        public float PercentOnMaxThrottle {get; set;}
        public float PercentOnMaxBrake {get; set;}
        public float ErsDeployed {get; set;}
        public float ErsHarvested {get; set;}
        public int GearChanges {get; set;}
        public ushort TopSpeedKph {get; set;}
        public ushort TopSpeedMph {get; set;}
        public TyreCompound EquippedTyreCompound {get; set;}

        //Incremental Tyre Wear
        public WheelDataArray IncrementalTyreWear {get; set;}
        public float IncrementalAverageTyreWear {get; set;}

        //Beginning tyre wear (snapshot)
        public WheelDataArray BeginningTyreWear {get; set;}
        public float BeginningAverageTyreWear {get; set;}
    }
}