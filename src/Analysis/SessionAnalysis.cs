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
                            if (this_distance < min_distance_found)
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

            //Close off
            Laps = _LapAnalysis.ToArray();

            //Shut down
            PercentLoadComplete = 1;
            LoadComplete = true;



        }

    }
}