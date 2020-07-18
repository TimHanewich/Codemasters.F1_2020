using System;

namespace Codemasters.F1_2020.Analysis
{
    public class CornerAnalysis
    {
        public byte CornerNumber {get; set;}
        public TrackLocation CornerData {get; set;}

        public MotionPacket.CarMotionData Motion {get; set;}
        public LapPacket.LapData Lap {get; set;}
        public TelemetryPacket.CarTelemetryData Telemetry {get; set;}
        public CarStatusPacket.CarStatusData CarStatus {get; set;}
    }
}