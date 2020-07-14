using System;
using System.Collections.Generic;
using TimHanewichToolkit;

namespace Codemasters.F1_2020
{
    public class LapPacket : Packet
    {

        public LapData[] FieldLapData { get; set; }

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            //Get header
            base.LoadBytes(BAM.NextBytes(24));




            //Get the next 20 data packages
            List<LapData> LDs = new List<LapData>();
            int t = 1;
            for (t = 1; t <= 22; t++)
            {
                LDs.Add(LapData.Create(BAM.NextBytes(53)));
            }
            FieldLapData = LDs.ToArray();
        }

        /// <summary>
        /// 53 bytes long.
        /// </summary>
        public class LapData
        {
            public float LastLapTime { get; set; }
            public float CurrentLapTime { get; set; }
            public ushort Sector1TimeMilliseconds { get; set; }
            public ushort Sector2TimeMilliseconds { get; set; }
            public float BestLapTimeSeconds { get; set; }
            public byte BestLapNumber {get; set;}
            public ushort BestLapSector1TimeMilliseconds {get; set;}
            public ushort BestLapSector2TimeMilliseconds {get; set;}
            public ushort BestLapSector3TimeMilliseconds {get; set;}
            public ushort BestOverallSector1TimeMilliseconds {get; set;}
            public byte BestOverallSector1TimeLapNumber {get; set;}
            public ushort BestOverallSector2TimeMilliseconds {get; set;}
            public byte BestOverallSector2TimeLapNumber {get; set;}
            public ushort BestOverallSector3TimeMilliseconds {get; set;}
            public byte BestOverallSector3TimeLapNumber {get; set;}          
            public float LapDistance { get; set; }
            public float TotalDistance { get; set; }
            public float SafetyCarDelta { get; set; }
            public byte CarPosition { get; set; }
            public byte CurrentLapNumber { get; set; }
            public PitStatus CurrentPitStatus { get; set; }
            public byte Sector { get; set; }
            public bool CurrentLapInvalid { get; set; }
            public byte Penalties { get; set; }
            public byte StartingGridPosition { get; set; }
            public DriverStatus CurrentDriverStatus { get; set; }
            public ResultStatus FinalResultStatus { get; set; }

            public static LapData Create(byte[] bytes)
            {
                LapData ReturnInstance = new LapData();
                ByteArrayManager BAM = new ByteArrayManager(bytes);

                ReturnInstance.LastLapTime = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.CurrentLapTime = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                ReturnInstance.Sector1TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.Sector2TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);


                ReturnInstance.BestLapTimeSeconds = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.BestLapNumber = BAM.NextByte();

                ReturnInstance.BestLapSector1TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BestLapSector2TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BestLapSector3TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);

                ReturnInstance.BestOverallSector1TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BestOverallSector1TimeLapNumber = BAM.NextByte();
                ReturnInstance.BestOverallSector2TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BestOverallSector2TimeLapNumber = BAM.NextByte();
                ReturnInstance.BestOverallSector3TimeMilliseconds = BitConverter.ToUInt16(BAM.NextBytes(2), 0);
                ReturnInstance.BestOverallSector3TimeLapNumber = BAM.NextByte();

                ReturnInstance.LapDistance = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.TotalDistance = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.SafetyCarDelta = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.CarPosition = BAM.NextByte();
                ReturnInstance.CurrentLapNumber = BAM.NextByte();

                //Get pit status
                byte nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.CurrentPitStatus = PitStatus.OnTrack;
                }
                else if (nb == 1)
                {
                    ReturnInstance.CurrentPitStatus = PitStatus.PitLane;
                }
                else if (nb == 2)
                {
                    ReturnInstance.CurrentPitStatus = PitStatus.PitArea;
                }

                //Get sector
                ReturnInstance.Sector = System.Convert.ToByte(BAM.NextByte() + 1);

                //Get current lap invalid
                nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.CurrentLapInvalid = false;
                }
                else if (nb == 1)
                {
                    ReturnInstance.CurrentLapInvalid = true;
                }

                //Get penalties
                ReturnInstance.Penalties = BAM.NextByte();

                //Get grid position
                ReturnInstance.StartingGridPosition = BAM.NextByte();

                //Get driver status
                nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.CurrentDriverStatus = DriverStatus.InGarage;
                }
                else if (nb == 1)
                {
                    ReturnInstance.CurrentDriverStatus = DriverStatus.FlyingLap;
                }
                else if (nb == 2)
                {
                    ReturnInstance.CurrentDriverStatus = DriverStatus.InLap;
                }
                else if (nb == 3)
                {
                    ReturnInstance.CurrentDriverStatus = DriverStatus.OutLap;
                }
                else if (nb == 4)
                {
                    ReturnInstance.CurrentDriverStatus = DriverStatus.OnTrack;
                }


                //Get result status
                nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.Invalid;
                }
                else if (nb == 1)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.Inactive;
                }
                else if (nb == 2)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.Active;
                }
                else if (nb == 3)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.Finished;
                }
                else if (nb == 4)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.Disqualified;
                }
                else if (nb == 5)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.NotClassified;
                }
                else if (nb == 6)
                {
                    ReturnInstance.FinalResultStatus = ResultStatus.Retired;
                }

                return ReturnInstance;
            }

        }

        public enum DriverStatus
        {
            InGarage,
            FlyingLap,
            InLap,
            OutLap,
            OnTrack
        }

        public enum ResultStatus
        {
            Invalid,
            Inactive,
            Active,
            Finished,
            Disqualified,
            NotClassified,
            Retired
        }


    }

}