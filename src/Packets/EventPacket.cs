using System;
using System.ComponentModel;
using TimHanewich.Toolkit;

namespace Codemasters.F1_2020
{
    public class EventPacket : Packet
    {
        public EventType EventStringCode { get; set; }
        public object EventDataDetails { get; set; }

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            // Load header
            base.LoadBytes(BAM.NextBytes(24));

            // Event string code
            String allChars = "";
            for (int i = 0; i < 4; i++)
            {
                Char currentChar = Convert.ToChar(BAM.NextByte());
                allChars += currentChar.ToString();           
            }
            Enum.TryParse<EventType>(allChars, out EventType e);
            this.EventStringCode = e;

            // Handle each type of event
            switch (this.EventStringCode)
            {
                case EventType.SSTA:
                    this.EventDataDetails = null;
                    break;

                case EventType.SEND:
                    this.EventDataDetails = null;
                    break;

                case EventType.FTLP:
                    this.EventDataDetails = FastestLapData.Create(BAM.NextBytes(5));
                    break;

                case EventType.RTMT:
                    this.EventDataDetails = RetirementData.Create(BAM.NextBytes(1));
                    break;

                case EventType.DRSE:
                    this.EventDataDetails = null;
                    break;

                case EventType.DRSD:
                    this.EventDataDetails = null;
                    break;

                case EventType.TMPT:
                    this.EventDataDetails = TeamMateInPitsData.Create(BAM.NextBytes(1));
                    break;

                case EventType.CHQF:
                    this.EventDataDetails = null;
                    break;

                case EventType.RCWN:
                    this.EventDataDetails = RaceWinnerData.Create(BAM.NextBytes(1));
                    break;

                case EventType.PENA:
                    this.EventDataDetails = PenaltyData.Create(BAM.NextBytes(7));
                    break;

                case EventType.SPTP:
                    this.EventDataDetails = SpeedTrapData.Create(BAM.NextBytes(7));
                    break;

            }
        }
    }
    
    public class FastestLapData
    {
        public byte VehicleID { get; set; }
        public float LapTime { get; set; }

        public static FastestLapData Create(byte[] bytes)
        {
            FastestLapData ReturnInstance = new FastestLapData();
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            ReturnInstance.VehicleID = BAM.NextByte();
            ReturnInstance.LapTime = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            return ReturnInstance;
        }
    }

    public class RetirementData
    {
        public byte VehicleID { get; set; }

        public static RetirementData Create(byte[] bytes)
        {
            RetirementData ReturnInstance = new RetirementData();
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            ReturnInstance.VehicleID = BAM.NextByte();

            return ReturnInstance;
        }
    }

    public class TeamMateInPitsData
    {
        public byte VehicleID { get; set; }

        public static TeamMateInPitsData Create(byte[] bytes)
        {
            TeamMateInPitsData ReturnInstance = new TeamMateInPitsData();
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            ReturnInstance.VehicleID = BAM.NextByte();

            return ReturnInstance;
        }
    }

    public class RaceWinnerData
    {
        public byte VehicleID { get; set; }

        public static RaceWinnerData Create(byte[] bytes)
        {
            RaceWinnerData ReturnInstance = new RaceWinnerData();
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            ReturnInstance.VehicleID = BAM.NextByte();

            return ReturnInstance;
        }
    }

    public class PenaltyData
    {
        public PenaltyType PenaltyType { get; set; }
        public InfringementType InfringementType { get; set; }
        public byte VehicleID { get; set; }
        public byte OtherVehicleID { get; set; }
        public byte Time { get; set; }
        public byte LapNum { get; set; }
        public byte PlacesGained { get; set; }

        public static PenaltyData Create(byte[] bytes)
        {
            PenaltyData ReturnInstance = new PenaltyData();
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            Enum.TryParse<PenaltyType>(BAM.NextByte().ToString(), out var pt);
            ReturnInstance.PenaltyType = pt;
            Enum.TryParse<InfringementType>(BAM.NextByte().ToString(), out var it);
            ReturnInstance.InfringementType = it;
            ReturnInstance.VehicleID = BAM.NextByte();
            ReturnInstance.OtherVehicleID = BAM.NextByte();
            ReturnInstance.Time = BAM.NextByte();
            ReturnInstance.LapNum = BAM.NextByte();
            ReturnInstance.PlacesGained = BAM.NextByte();

            return ReturnInstance;
        }
    }

    public class SpeedTrapData
    {
        public byte VehicleID { get; set; }
        public float Speed { get; set; }
        public byte OverallFastestInSession { get; set; }
        public byte PersonalFastestInSession { get; set; }


        public static SpeedTrapData Create(byte[] bytes)
        {
            SpeedTrapData ReturnInstance = new SpeedTrapData();
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            ReturnInstance.VehicleID = BAM.NextByte();
            ReturnInstance.Speed = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            ReturnInstance.OverallFastestInSession = BAM.NextByte();
            ReturnInstance.PersonalFastestInSession = BAM.NextByte();

            return ReturnInstance;
        }
    }

    public enum EventType : byte
    {
        [Description("Session Started")]
        SSTA,
        [Description("Session Ended")]
        SEND,
        [Description("Fastest Lap")]
        FTLP,
        [Description("Retirement")]
        RTMT,
        [Description("DRS Enabled")]
        DRSE,
        [Description("DRS Disabled")]
        DRSD,
        [Description("Team mate in pits")]
        TMPT,
        [Description("Chequered Flag")]
        CHQF,
        [Description("Race Winner")]
        RCWN,
        [Description("Penalty Issued")]
        PENA,
        [Description("Speed Trap Triggered")]
        SPTP

    }

    public enum PenaltyType : byte
    {
        DriveThrough,
        StopGo,
        GridPenalty,
        PenaltyReminder,
        TimePenalty,
        Warning,
        Disqualified,
        RemovedFromFormationLap,
        ParkedTooLongTimer,
        TyreRegulations,
        CurrentLapInvalidated,
        CurrentAndNextLapInvalidated,
        CurrentLapInvalidatedWithoutReason,
        CurrentAndNextLapInvalidatedWithoutReason,
        ThisAndPreviousLapInvalidated,
        ThisAndPreviousLapInvalidatedWithoutReason,
        Retired,
        BlackFlagTimer

    }

    public enum InfringementType : byte
    {
        BlockingBySlowDriving,
        BlockingByWrongWayDriving,
        ReversingOffStartLine,
        BigCollision,
        SmallCollision,
        CollisionFailedToHandBackPositionSingle,
        CollissionFailedToHandBackPositionMultiple,
        CornerCuttingGainedTime,
        CornerCuttingOvertakeSingle,
        CornerCuttingOvertakeMultiple,
        CrossedPitExitLine,
        IgnoringBlueFlags,
        IgnoringYellowFlags,
        IgnoringDriveThrough,
        TooManyDriveThroughs,
        DriveThroughReminderNLaps,
        DriveThroughReminderCurrentLap,
        PitLaneSpeeding,
        ParkedTooLong,
        IgnoringTyreRegulations,
        TooManyPenalties,
        MultipleWarnings,
        ApproachingDisqualification,
        TyreRegulationsSelectSingle,
        TyreRegulationsSelectMultiple,
        LapInvalidatedCornerCutting,
        LapInvalidatedRunningWide,
        CornerCuttingRanWideGainedTimeMinor,
        CornerCuttingRanWideGainedTimeSignificant,
        CornerCuttingRanWideGainedTimeExtreme,
        LapInvalidatedWallRiding,
        LapInvalidatedFlashbackUsed,
        LapInvalidatedResetToTrack,
        BlockingPitlane,
        JumpStart,
        SafetyCarCollision,
        SafetyCarIllegalOvertake,
        SafetyCarExceedingAllowedPace,
        VirtualSafetyCarExceedingAllowedPace,
        FormationLapBelowAllowedSpeed,
        RetiredMechanicalFailure,
        RetiredTerminallyDamaged,
        SafetyCarFallingTooFarBack,
        BlackFlagTimer,
        UnservedStopGoPenalty,
        UnservedDriveThroughPenalty,
        EngineComponentChange,
        GearboxChange,
        LeagueGridPenalty,
        RetryPenalty,
        IllegalTimeGain,
        MandatoryPitstop

    }
}