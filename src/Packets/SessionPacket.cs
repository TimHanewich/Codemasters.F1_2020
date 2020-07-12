using System;
using System.Collections.Generic;
using TimHanewichToolkit;

namespace Codemasters.F1_2020
{
    public class SessionPacket : Packet
    {

        public WeatherCondition CurrentWeatherCondition { get; set; }
        public byte TrackTemperatureCelsius { get; set; }
        public byte AirTemperatureCelsius { get; set; }
        public byte TotalLapsInRace { get; set; }
        public ushort TrackLengthMeters { get; set; }
        public SessionType SessionTypeMode { get; set; }
        public Track SessionTrack { get; set; }
        public FormulaType Formula { get; set; }
        public ushort SessionTimeLeft { get; set; }
        public ushort SessionDuration { get; set; }
        public byte PitSpeedLimitKph { get; set; }
        public bool GamePaused { get; set; }
        public bool IsSpectating { get; set; }
        public byte CarIndexBeingSpectated { get; set; }
        public bool SliProNativeSupport { get; set; }
        public byte NumberOfMarshallZones { get; set; }
        public MarshallZone[] MarshallZones { get; set; }
        public SafetyCarStatus CurrentSafetyCarStatus { get; set; }
        public bool IsNetworkGame { get; set; }



        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            //Load header
            base.LoadBytes(BAM.NextBytes(23));

            //Get weather
            byte nb = BAM.NextByte();
            if (nb == 0)
            {
                CurrentWeatherCondition = WeatherCondition.Clear;
            }
            else if (nb == 1)
            {
                CurrentWeatherCondition = WeatherCondition.LightClouds;
            }
            else if (nb == 2)
            {
                CurrentWeatherCondition = WeatherCondition.Overcast;
            }
            else if (nb == 3)
            {
                CurrentWeatherCondition = WeatherCondition.LightRain;
            }
            else if (nb == 4)
            {
                CurrentWeatherCondition = WeatherCondition.HeavyRain;
            }
            else if (nb == 5)
            {
                CurrentWeatherCondition = WeatherCondition.Storm;
            }


            //Get track temperature
            TrackTemperatureCelsius = BAM.NextByte();

            //Get air temperature
            AirTemperatureCelsius = BAM.NextByte();

            //Get total laps
            TotalLapsInRace = BAM.NextByte();

            //get track length
            TrackLengthMeters = BitConverter.ToUInt16(BAM.NextBytes(2), 0);

            //Get session type
            nb = BAM.NextByte();
            if (nb == 0)
            {
                SessionTypeMode = SessionType.Unknown;
            }
            else if (nb == 1)
            {
                SessionTypeMode = SessionType.Practice1;
            }
            else if (nb == 2)
            {
                SessionTypeMode = SessionType.Practice2;
            }
            else if (nb == 3)
            {
                SessionTypeMode = SessionType.Practice3;
            }
            else if (nb == 4)
            {
                SessionTypeMode = SessionType.ShortPractice;
            }
            else if (nb == 5)
            {
                SessionTypeMode = SessionType.Qualifying1;
            }
            else if (nb == 6)
            {
                SessionTypeMode = SessionType.Qualifying2;
            }
            else if (nb == 7)
            {
                SessionTypeMode = SessionType.Qualifying3;
            }
            else if (nb == 8)
            {
                SessionTypeMode = SessionType.ShortPractice;
            }
            else if (nb == 9)
            {
                SessionTypeMode = SessionType.OneShotQualifying;
            }
            else if (nb == 10)
            {
                SessionTypeMode = SessionType.Race;
            }
            else if (nb == 11)
            {
                SessionTypeMode = SessionType.Race2;
            }
            else if (nb == 12)
            {
                SessionTypeMode = SessionType.TimeTrial;
            }

            //Get track
            SessionTrack = CodemastersToolkit.GetTrackFromTrackId(BAM.NextByte());

            //Get formula
            nb = BAM.NextByte();
            if (nb == 0)
            {
                Formula = FormulaType.Formula1Modern;
            }
            else if (nb == 1)
            {
                Formula = FormulaType.Formula1Classic;
            }
            else if (nb == 2)
            {
                Formula = FormulaType.Formula2;
            }
            else if (nb == 3)
            {
                Formula = FormulaType.Formula1Generic;
            }


            //Get session time left
            SessionTimeLeft = BitConverter.ToUInt16(BAM.NextBytes(2), 0);

            //Get session duration
            SessionDuration = BitConverter.ToUInt16(BAM.NextBytes(2), 0);

            //Get pit speed limit
            PitSpeedLimitKph = BAM.NextByte();

            //get game paused
            nb = BAM.NextByte();
            if (nb == 0)
            {
                GamePaused = false;
            }
            else if (nb == 1)
            {
                GamePaused = true;
            }

            //Get is spectating
            nb = BAM.NextByte();
            if (nb == 0)
            {
                IsSpectating = false;
            }
            else if (nb == 1)
            {
                IsSpectating = true;
            }


            //Get spectating car index
            CarIndexBeingSpectated = BAM.NextByte();

            //Get sli pro native support
            nb = BAM.NextByte();
            if (nb == 0)
            {
                SliProNativeSupport = false;
            }
            else if (nb == 1)
            {
                SliProNativeSupport = true;
            }

            //Get number of marshall zones
            NumberOfMarshallZones = BAM.NextByte();

            //Get marshall zones
            List<MarshallZone> MZs = new List<MarshallZone>();
            int t = 1;
            for (t = 1; t <= 21; t++)
            {
                MZs.Add(MarshallZone.Create(BAM.NextBytes(5)));
            }


            //Get safety car status
            nb = BAM.NextByte();
            if (nb == 0)
            {
                CurrentSafetyCarStatus = SafetyCarStatus.None;
            }
            else if (nb == 1)
            {
                CurrentSafetyCarStatus = SafetyCarStatus.Full;
            }
            else if (nb == 2)
            {
                CurrentSafetyCarStatus = SafetyCarStatus.Virtual;
            }

            //Get network game boolean
            nb = BAM.NextByte();
            if (nb == 0)
            {
                IsNetworkGame = false;
            }
            else if (nb == 1)
            {
                IsNetworkGame = true;
            }


        }




        public class MarshallZone
        {
            public float ZoneStart { get; set; }
            public FiaFlag ZoneFlag { get; set; }

            public static MarshallZone Create(byte[] bytes)
            {
                MarshallZone ReturnInstance = new MarshallZone();
                ByteArrayManager BAM = new ByteArrayManager(bytes);

                //Get zone start
                ReturnInstance.ZoneStart = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get zone flag
                byte nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.ZoneFlag = FiaFlag.None;
                }
                else if (nb == 1)
                {
                    ReturnInstance.ZoneFlag = FiaFlag.Green;
                }
                else if (nb == 2)
                {
                    ReturnInstance.ZoneFlag = FiaFlag.Blue;
                }
                else if (nb == 3)
                {
                    ReturnInstance.ZoneFlag = FiaFlag.Yellow;
                }
                else if (nb == 4)
                {
                    ReturnInstance.ZoneFlag = FiaFlag.Red;
                }

                return ReturnInstance;
            }

        }

        public enum WeatherCondition
        {
            Clear,
            LightClouds,
            Overcast,
            LightRain,
            HeavyRain,
            Storm
        }

        public enum SafetyCarStatus
        {
            None,
            Full,
            Virtual
        }

        public enum FormulaType
        {
            Formula1Modern,
            Formula1Classic,
            Formula2,
            Formula1Generic
        }

        public enum SessionType
        {
            Unknown,
            Practice,
            Qualifying,
            Race,
            Practice1,
            Practice2,
            Practice3,
            ShortPractice,
            Qualifying1,
            Qualifying2,
            Qualifying3,
            ShortQualifying,
            OneShotQualifying,
            Race2,
            TimeTrial
        }
    }

}