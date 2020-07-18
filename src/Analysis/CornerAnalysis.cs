using System;

namespace Codemasters.F1_2020.Analysis
{
    public class CornerAnalysis
    {
        //Info about the corner
        public byte CornerNumber {get; set;}
        public TrackLocation CornerData {get; set;}

        //The data
        public MotionPacket.CarMotionData Motion {get; set;}
        public LapPacket.LapData Lap {get; set;}
        public TelemetryPacket.CarTelemetryData Telemetry {get; set;}
        public CarStatusPacket.CarStatusData CarStatus {get; set;}


        public float DistanceToApex()
        {
            TrackLocation car_loc = new TrackLocation();
            car_loc.PositionX = Motion.PositionX;
            car_loc.PositionY = Motion.PositionY;
            car_loc.PositionZ = Motion.PositionZ;
            car_loc.Sector = Lap.Sector;

            float distance = CodemastersToolkit.DistanceBetweenTwoPoints(CornerData, car_loc);
            return distance;
        }

    }
}