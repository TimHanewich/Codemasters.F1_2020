using System;
using System.Collections.Generic;
using TimHanewich.Toolkit;

namespace Codemasters.F1_2020
{
    public class TelemetryPacket : Packet
    {
        public CarTelemetryData[] FieldTelemetryData { get; set; }
        public int ButtonStatus {get; set;}
        public byte MfdPanelIndex {get; set;} //New to f1 2020. This should be an enum, but will do that later.
        public byte SecondaryPlayerMfdPanelIndex {get; set;}
        public byte SuggestedGear {get; set;}

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);
            base.LoadBytes(BAM.NextBytes(24)); //Load header

            int t = 0;
            List<CarTelemetryData> TelDatas = new List<CarTelemetryData>();
            for (t = 1; t <= 22; t++)
            {
                TelDatas.Add(CarTelemetryData.Create(BAM.NextBytes(58)));
            }
            FieldTelemetryData = TelDatas.ToArray();

            //Button status
            ButtonStatus = BitConverter.ToInt32(BAM.NextBytes(4), 0);

            //MFD Panel Index
            MfdPanelIndex = BAM.NextByte();

            //Secndary player mfd panel index
            SecondaryPlayerMfdPanelIndex = BAM.NextByte();

            //Suggested gear
            SuggestedGear = BAM.NextByte();

        }

        public class CarTelemetryData
        {
            public ushort SpeedKph { get; set; }
            public ushort SpeedMph { get; set; }
            public float Throttle { get; set; }
            public float Steer { get; set; }
            public float Brake { get; set; }
            public float Clutch { get; set; }
            public sbyte Gear { get; set; }
            public ushort EngineRpm { get; set; }
            public bool DrsActive { get; set; }
            public byte RevLightsPercentage { get; set; }
            public WheelDataArray BrakeTemperature { get; set; }
            public WheelDataArray TyreSurfaceTemperature { get; set; }
            public WheelDataArray TyreInnerTemperature { get; set; }
            public ushort EngineTemperature { get; set; }
            public WheelDataArray TyrePressure { get; set; }
            public SurfaceType SurfaceType_RearLeft { get; set; }
            public SurfaceType SurfaceType_RearRight { get; set; }
            public SurfaceType SurfaceType_FrontLeft { get; set; }
            public SurfaceType SurfaceType_FrontRight { get; set; }

            public static CarTelemetryData Create(byte[] bytes)
            {
                CarTelemetryData ReturnInstance = new CarTelemetryData();

                ByteArrayManager BAM = new ByteArrayManager(bytes);

                //Get speed
                ReturnInstance.SpeedKph = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                double MPH = ReturnInstance.SpeedKph * 0.621371;
                ReturnInstance.SpeedMph = (ushort)MPH;

                //Get throttle
                ReturnInstance.Throttle = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get steer
                ReturnInstance.Steer = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get brake
                ReturnInstance.Brake = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get clutch
                ReturnInstance.Clutch = BAM.NextByte();

                //Get gear
                ReturnInstance.Gear = (sbyte)BAM.NextByte();

                //Get engine RMP
                ReturnInstance.EngineRpm = BitConverter.ToUInt16(BAM.NextBytes(2), 0);

                //Drs on or not
                byte nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.DrsActive = false;
                }
                else if (nb == 1)
                {
                    ReturnInstance.DrsActive = true;
                }

                //Get engine rev lights percentage
                ReturnInstance.RevLightsPercentage = BAM.NextByte();

                //get brake temperature
                ReturnInstance.BrakeTemperature = new WheelDataArray();
                ReturnInstance.BrakeTemperature.RearLeft = (float)BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BrakeTemperature.RearRight = (float)BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BrakeTemperature.FrontLeft = (float)BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BrakeTemperature.FrontRight = (float)BitConverter.ToUInt16(BAM.NextBytes(2), 0);

                //get tyre surface temperature
                ReturnInstance.TyreSurfaceTemperature = new WheelDataArray();
                ReturnInstance.TyreSurfaceTemperature.RearLeft = (float)BAM.NextByte();
                ReturnInstance.TyreSurfaceTemperature.RearRight = (float)BAM.NextByte();
                ReturnInstance.TyreSurfaceTemperature.FrontLeft = (float)BAM.NextByte();
                ReturnInstance.TyreSurfaceTemperature.FrontRight = (float)BAM.NextByte();

                //get tyre Inner Temperature
                ReturnInstance.TyreInnerTemperature = new WheelDataArray();
                ReturnInstance.TyreInnerTemperature.RearLeft = (float)BAM.NextByte();
                ReturnInstance.TyreInnerTemperature.RearRight = (float)BAM.NextByte();
                ReturnInstance.TyreInnerTemperature.FrontLeft = (float)BAM.NextByte();
                ReturnInstance.TyreInnerTemperature.FrontRight = (float)BAM.NextByte();

                //Get engine temperature
                ReturnInstance.EngineTemperature = BitConverter.ToUInt16(BAM.NextBytes(2), 0);

                //Get tyre pressure
                ReturnInstance.TyrePressure = new WheelDataArray();
                ReturnInstance.TyrePressure.RearLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.TyrePressure.RearRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.TyrePressure.FrontLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.TyrePressure.FrontRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get surface type
                ReturnInstance.SurfaceType_RearLeft = CodemastersToolkit.GetSurfaceTypeFromSurfaceTypeId(BAM.NextByte());
                ReturnInstance.SurfaceType_RearRight = CodemastersToolkit.GetSurfaceTypeFromSurfaceTypeId(BAM.NextByte());
                ReturnInstance.SurfaceType_FrontLeft = CodemastersToolkit.GetSurfaceTypeFromSurfaceTypeId(BAM.NextByte());
                ReturnInstance.SurfaceType_FrontRight = CodemastersToolkit.GetSurfaceTypeFromSurfaceTypeId(BAM.NextByte());

                return ReturnInstance;
            }

        }
    }

}