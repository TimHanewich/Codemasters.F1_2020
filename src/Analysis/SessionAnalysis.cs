using System;
using Codemasters.F1_2020;
using System.Collections.Generic;
using System.Linq;

namespace Codemasters.F1_2020.Analysis
{
    public class SessionAnalysis
    {
        public ulong SessionId {get; set;}
        public LapAnalysis[] Laps {get; set;}

        //For reporting purposes
        public float PercentLoadComplete;
        public bool LoadComplete;

        public void Load(Packet[] packets, byte driver_index)
        {
            PercentLoadComplete = 0;
            LoadComplete = false;

            //Summon this track
            Track ToLoad = Track.Unknown;
            foreach (Packet p in packets)
            {
                if (p.PacketType == PacketType.Session)
                {
                    SessionPacket sp = (SessionPacket)p;
                    ToLoad = sp.SessionTrack;
                    SessionId = sp.UniqueSessionId;
                    break;
                }
            }
            if (ToLoad == Track.Unknown)
            {
                throw new Exception("The track you are racing on in this session is unkown!");
            }
            TrackDataContainer tdc = TrackDataContainer.LoadTrack(ToLoad);

            //Get list of all laps
            List<byte> AllLaps = new List<byte>();
            foreach (Packet p in packets)
            {
                if (p.PacketType == PacketType.Lap)
                {
                    LapPacket lp = (LapPacket)p;
                    byte this_lap_num = lp.FieldLapData[driver_index].CurrentLapNumber;
                    if (AllLaps.Contains(this_lap_num) == false)
                    {
                        AllLaps.Add(this_lap_num);
                    }
                }
            }

            // //remove the highest lap (because you crossed the line and likely didn't finished it)
            // AllLaps.Remove(AllLaps.Max());

            //Set the total number of corners that need to be analyzed (this is used only for progress reporting purposes)
            int number_of_corners = tdc.Corners.Length * AllLaps.Count;


            //Generate the frames
            PacketFrame[] frames = PacketFrame.CreateAll(packets);

            //Create the lap analysis objects and fill it with corner analysis data.
            //This process also fills in the LapNumber property
            List<LapAnalysis> _LapAnalysis = new List<LapAnalysis>();
            foreach (byte lap_num in AllLaps)
            {

                LapAnalysis this_lap_analysis = new LapAnalysis();

                //Fill in the lap number
                this_lap_analysis.LapNumber = lap_num;

                //Get all of the frames for this lap
                List<PacketFrame> ThisLapFrames = new List<PacketFrame>();
                foreach (PacketFrame pf in frames)
                {
                    if (pf.Lap.FieldLapData[driver_index].CurrentLapNumber == lap_num)
                    {
                        ThisLapFrames.Add(pf);
                    }
                }



                //Find the best packetframe for each corner
                List<CornerAnalysis> _CornerAnalysis = new List<CornerAnalysis>();
                int c = 1;
                for (c=1;c<=tdc.Corners.Length;c++)
                {

                    //Find the best packetframe for this corner
                    TrackLocation this_corner = tdc.Corners[c-1];
                    PacketFrame winner = ThisLapFrames[0];
                    float min_distance_found = float.MaxValue;
                    foreach (PacketFrame pf in ThisLapFrames)
                    {
                        TrackLocation this_location = new TrackLocation();
                        this_location.PositionX = pf.Motion.FieldMotionData[driver_index].PositionX;
                        this_location.PositionY = pf.Motion.FieldMotionData[driver_index].PositionY;
                        this_location.PositionZ = pf.Motion.FieldMotionData[driver_index].PositionZ;
                        this_location.Sector = pf.Lap.FieldLapData[driver_index].Sector;

                        if (this_location.Sector == this_corner.Sector) //Only consider packets that are in the same sector
                        {
                            float this_distance = CodemastersToolkit.DistanceBetweenTwoPoints(this_corner, this_location);
                            if (this_distance < min_distance_found && this_distance < 40) //It has to be be within 40. If it isn't, it is not considered a corner hit!
                            {
                                winner = pf;
                                min_distance_found = this_distance;
                            }
                        }
                    }

                    //Add the corner analysis
                    CornerAnalysis ca = new CornerAnalysis();
                    ca.CornerNumber = (byte)c;
                    ca.CornerData = this_corner;
                    if (min_distance_found < float.MaxValue) //we found a suitable packet, so therefore the min distance shold be less than max
                    {
                        ca.Motion = winner.Motion.FieldMotionData[driver_index];
                        ca.Lap = winner.Lap.FieldLapData[driver_index];
                        ca.Telemetry = winner.Telemetry.FieldTelemetryData[driver_index];
                        ca.CarStatus = winner.CarStatus.FieldCarStatusData[driver_index];
                    }
                    else //if we were not able to find a suitable packet for that corner, populate it with just a blank PacketFrame as a place holder.
                    {
                        ca.Motion = null;
                        ca.Lap = null;
                        ca.Telemetry = null;
                        ca.CarStatus = null;
                    }
                    _CornerAnalysis.Add(ca);
                }
                this_lap_analysis.Corners = _CornerAnalysis.ToArray();
                

                _LapAnalysis.Add(this_lap_analysis);

            }
            

            //Sort the packets by time - this is not required for the above process, so I am doing it here for the timing stuff
            List<PacketFrame> frames_aslist = frames.ToList();
            List<PacketFrame> frames_sorted = new List<PacketFrame>();
            while (frames_aslist.Count > 0)
            {
                PacketFrame winner = frames_aslist[0];
                foreach (PacketFrame pf in frames_aslist)
                {
                    if (pf.Telemetry.SessionTime < winner.Telemetry.SessionTime)
                    {
                        winner = pf;
                    }
                }
                frames_sorted.Add(winner);
                frames_aslist.Remove(winner);
            }


            //Plug in the sector times and lap times
            PacketFrame last_frame = null;
            foreach (PacketFrame this_frame in frames_sorted)
            {
                if (last_frame != null)
                {

                    float S1_Time_S = 0;
                    float S2_Time_S = 0;
                    float S3_Time_S = 0;
                    float LapTime_S = 0;
                    bool Lap_Invalid_In_Last_Frame = false;

                    if (last_frame.Lap.FieldLapData[driver_index].Sector == 1 && this_frame.Lap.FieldLapData[driver_index].Sector == 2) //We went from sector 1 to sector 2
                    {
                        S1_Time_S = (float)this_frame.Lap.FieldLapData[driver_index].Sector1TimeMilliseconds / 1000f;
                    }
                    else if (last_frame.Lap.FieldLapData[driver_index].Sector == 2 && this_frame.Lap.FieldLapData[driver_index].Sector == 3) //We went from sector 2 to sector 3
                    {
                        S2_Time_S = (float)this_frame.Lap.FieldLapData[driver_index].Sector2TimeMilliseconds / 1000f;
                    }
                    else if (last_frame.Lap.FieldLapData[driver_index].CurrentLapNumber < this_frame.Lap.FieldLapData[driver_index].CurrentLapNumber) //We just finished the lap, and thus sector 3
                    {
                        float last_s1_seconds = (float)last_frame.Lap.FieldLapData[driver_index].Sector1TimeMilliseconds / 1000f;
                        float last_s2_seconds = (float)last_frame.Lap.FieldLapData[driver_index].Sector2TimeMilliseconds / 1000f;
                        LapTime_S = this_frame.Lap.FieldLapData[driver_index].LastLapTime;
                        S3_Time_S = LapTime_S - last_s1_seconds - last_s2_seconds;
                    }

                    if (last_frame.Lap.FieldLapData[driver_index].CurrentLapInvalid)
                    {
                        Lap_Invalid_In_Last_Frame = true;
                    }


                    //If any of the numbers up there changed (are not 0), it means that we either changed sector or changed lap. If we did, we need to plug that data into the lapanalysis
                    if (S1_Time_S > 0 || S2_Time_S > 0 || S3_Time_S > 0 || Lap_Invalid_In_Last_Frame)
                    {
                        //Find the lap analysis
                        foreach (LapAnalysis la in _LapAnalysis)
                        {
                            if (la.LapNumber == last_frame.Lap.FieldLapData[driver_index].CurrentLapNumber)
                            {
                                
                                if (S1_Time_S > 0)
                                {
                                    la.Sector1Time = S1_Time_S;
                                }

                                if (S2_Time_S > 0)
                                {
                                    la.Sector2Time = S2_Time_S;
                                }

                                if (S3_Time_S > 0)
                                {
                                    la.Sector3Time = S3_Time_S;
                                }

                                if (LapTime_S > 0)
                                {
                                    la.LapTime = LapTime_S;
                                }

                                if (Lap_Invalid_In_Last_Frame)
                                {
                                    la.LapInvalid = true;
                                }

                            }
                        }
                    }



                }
                last_frame = this_frame;
            }


            #region "Get fuel consumption for each lap"

            foreach (byte lapnum in AllLaps)
            {
                //Get all packets for this lap
                List<PacketFrame> lap_frames = new List<PacketFrame>();
                foreach (PacketFrame frame in frames_sorted)
                {
                    if (frame.Lap.FieldLapData[driver_index].CurrentLapNumber == lapnum)
                    {
                        lap_frames.Add(frame);
                    }
                }

                //Get the min and max and then plug it in
                if (lap_frames.Count > 0)
                {
                    float fuel_start = lap_frames[0].CarStatus.FieldCarStatusData[driver_index].FuelLevel;
                    float fuel_end = lap_frames[lap_frames.Count - 1].CarStatus.FieldCarStatusData[driver_index].FuelLevel;
                    float fuel_used = fuel_end - fuel_start;
                    fuel_used = fuel_used * -1;

                    foreach (LapAnalysis la in _LapAnalysis)
                    {
                        if (la.LapNumber == lapnum)
                        {
                            la.FuelConsumed = fuel_used;
                        }
                    }
                }
            }

            #endregion

            #region "Get percent on throttle/brake/coasting/max throttle/max brake"

            foreach (byte lapnum in AllLaps)
            {
                //Get all packets for this lap
                List<PacketFrame> lap_frames = new List<PacketFrame>();
                foreach (PacketFrame frame in frames_sorted)
                {
                    if (frame.Lap.FieldLapData[driver_index].CurrentLapNumber == lapnum)
                    {
                        lap_frames.Add(frame);
                    }
                }

                //Set up counter variables
                int OnThrottle = 0;
                int OnBrake = 0;
                int Coasting = 0;
                int Overlap = 0;
                int FullThrottle = 0;
                int FullBrake = 0;

                //Do the calculations
                foreach (PacketFrame frame in lap_frames)
                {
                    TelemetryPacket.CarTelemetryData ctd = frame.Telemetry.FieldTelemetryData[driver_index];


                    //Basics
                    if (ctd.Throttle > 0 && ctd.Brake == 0)
                    {
                        OnThrottle = OnThrottle + 1;
                    }
                    else if (ctd.Brake > 0 && ctd.Throttle == 0)
                    {
                        OnBrake = OnBrake + 1;
                    }
                    else if (ctd.Throttle == 0 && ctd.Brake == 0)
                    {
                        Coasting = Coasting + 1;
                    }
                    else if (ctd.Throttle > 0 && ctd.Brake > 0)
                    {
                        Overlap = Overlap + 1;
                    }

                    //Full pressures
                    if (ctd.Throttle == 1)
                    {
                        FullThrottle = FullThrottle + 1;
                    }
                    if (ctd.Brake == 1)
                    {
                        FullBrake = FullBrake + 1;
                    }


                }
                
                //Do the calculations
                float percent_on_throttle = (float)OnThrottle / (float)lap_frames.Count;
                float percent_on_brake = (float)OnBrake / (float)lap_frames.Count;
                float percent_coasting = (float)Coasting / (float)lap_frames.Count;
                float percent_overlap = (float)Overlap / (float)lap_frames.Count;
                float full_throttle = (float)FullThrottle / (float)lap_frames.Count;
                float full_brake = (float)FullBrake / (float)lap_frames.Count;

                //plug them in
                foreach (LapAnalysis la in _LapAnalysis)
                {
                    if (la.LapNumber == lapnum)
                    {
                        la.PercentOnThrottle = percent_on_throttle;
                        la.PercentOnBrake = percent_on_brake;
                        la.PercentCoasting = percent_coasting;
                        la.PercentThrottleBrakeOverlap = percent_overlap;
                        la.PercentOnMaxThrottle = full_throttle;
                        la.PercentOnMaxBrake = full_brake;
                    }
                }
            }

            #endregion

            #region "Get ERS deployed + harvested"

            foreach (byte lapnum in AllLaps)
            {
                //Get all packets for this lap
                List<PacketFrame> lap_frames = new List<PacketFrame>();
                foreach (PacketFrame frame in frames_sorted)
                {
                    if (frame.Lap.FieldLapData[driver_index].CurrentLapNumber == lapnum)
                    {
                        lap_frames.Add(frame);
                    }
                }

                float ers_dep = lap_frames[lap_frames.Count-1].CarStatus.FieldCarStatusData[driver_index].ErsDeployedThisLap;
                float ers_har = lap_frames[lap_frames.Count-1].CarStatus.FieldCarStatusData[driver_index].ErsHarvestedThisLapByMGUH + lap_frames[lap_frames.Count-1].CarStatus.FieldCarStatusData[driver_index].ErsHarvestedThisLapByMGUK;

                //plug them in
                foreach (LapAnalysis la in _LapAnalysis)
                {
                    if (la.LapNumber == lapnum)
                    {
                        la.ErsDeployed = ers_dep;
                        la.ErsHarvested = ers_har;
                    }
                }
            }

            #endregion

            #region "Get gear changes"

            foreach (byte lapnum in AllLaps)
            {
                //Get all packets for this lap
                List<PacketFrame> lap_frames = new List<PacketFrame>();
                foreach (PacketFrame frame in frames_sorted)
                {
                    if (frame.Lap.FieldLapData[driver_index].CurrentLapNumber == lapnum)
                    {
                        lap_frames.Add(frame);
                    }
                }

                //Count the number of gear changes
                int gear_changes = 0;
                PacketFrame last_frame_ = null;
                foreach (PacketFrame frame in lap_frames)
                {
                    if (last_frame_ != null)
                    {
                        sbyte last_gear = last_frame_.Telemetry.FieldTelemetryData[driver_index].Gear;
                        sbyte this_gear = frame.Telemetry.FieldTelemetryData[driver_index].Gear;
                        if (this_gear != last_gear)
                        {
                            gear_changes = gear_changes + 1;
                        }
                    }
                    last_frame_ = frame;
                }

                //Plug it in
                foreach (LapAnalysis la in _LapAnalysis)
                {
                    if (la.LapNumber == lapnum)
                    {
                        la.GearChanges = gear_changes;
                    }
                }

            }

            #endregion

            #region  "Get Top Speed"

            foreach (byte lapnum in AllLaps)
            {
                //Get all packets for this lap
                List<PacketFrame> lap_frames = new List<PacketFrame>();
                foreach (PacketFrame frame in frames_sorted)
                {
                    if (frame.Lap.FieldLapData[driver_index].CurrentLapNumber == lapnum)
                    {
                        lap_frames.Add(frame);
                    }
                }

                //Get values
                ushort max_kph = 0;
                ushort max_mph = 0;
                foreach (PacketFrame frame in lap_frames)
                {
                    TelemetryPacket.CarTelemetryData ctd = frame.Telemetry.FieldTelemetryData[driver_index];
                    if (ctd.SpeedKph > max_kph)
                    {
                        max_kph = ctd.SpeedKph;
                    }
                    if (ctd.SpeedMph > max_mph)
                    {
                        max_mph = ctd.SpeedMph;
                    }
                }

                //Plug it in
                foreach (LapAnalysis la in _LapAnalysis)
                {
                    if (la.LapNumber == lapnum)
                    {
                        la.TopSpeedKph = max_kph;
                        la.TopSpeedMph = max_mph;
                    }
                }


            }

            #endregion

            #region "Get average incremental tyre wear"

            foreach (byte lapnum in AllLaps)
            {
                //Get all packets for this lap
                List<PacketFrame> lap_frames = new List<PacketFrame>();
                foreach (PacketFrame frame in frames_sorted)
                {
                    if (frame.Lap.FieldLapData[driver_index].CurrentLapNumber == lapnum)
                    {
                        lap_frames.Add(frame);
                    }
                }

                WheelDataArray tyrewear_start = lap_frames[0].CarStatus.FieldCarStatusData[driver_index].TyreWearPercentage;
                WheelDataArray tyrewear_end = lap_frames[lap_frames.Count-1].CarStatus.FieldCarStatusData[driver_index].TyreWearPercentage;
                float AvgTyreWear_Start = (tyrewear_start.RearLeft + tyrewear_start.RearRight + tyrewear_start.FrontLeft + tyrewear_start.FrontRight) / 4f;
                float AvgTyreWear_End = (tyrewear_end.RearLeft + tyrewear_end.RearRight + tyrewear_end.FrontLeft + tyrewear_end.FrontRight) / 4f;
                float avginctyrewear = AvgTyreWear_End - AvgTyreWear_Start;

                //Plug it in
                foreach (LapAnalysis la in _LapAnalysis)
                {
                    if (la.LapNumber == lapnum)
                    {
                        la.IncrementalAverageTyreWear = avginctyrewear;
                    }
                }

            }

            #endregion

            //Close off
            Laps = _LapAnalysis.ToArray();

            //Shut down
            PercentLoadComplete = 1;
            LoadComplete = true;



        }

    }
}