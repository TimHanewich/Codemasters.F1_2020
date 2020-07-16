using System;
using Codemasters.F1_2020;
using System.Collections.Generic;

namespace Codemasters.F1_2020.Analysis
{
    public class PacketFrame
    {
        public MotionPacket Motion {get; set;}
        public LapPacket Lap {get; set;}
        public TelemetryPacket Telemetry {get; set;}
        public CarStatusPacket CarStatus {get; set;}

        public static PacketFrame Create(Packet[] all_session_packets, uint frame_identifier)
        {

            PacketFrame ToReturn = new PacketFrame();
            foreach (Packet p in all_session_packets)
            {
                if (p.FrameIdentifier == frame_identifier)
                {
                    if (p.PacketType == PacketType.Motion)
                    {
                        ToReturn.Motion = (MotionPacket)p;
                    }
                    else if (p.PacketType == PacketType.Lap)
                    {
                        ToReturn.Lap = (LapPacket)p;
                    }
                    else if (p.PacketType == PacketType.CarTelemetry)
                    {
                        ToReturn.Telemetry = (TelemetryPacket)p;
                    }
                    else if (p.PacketType == PacketType.CarStatus)
                    {
                        ToReturn.CarStatus = (CarStatusPacket)p;
                    }
                }
            }
       
            //Check if anything is missing
            if (ToReturn.Motion == null)
            {
                throw new Exception("Unable to find motion packet for frame " + frame_identifier.ToString() + ".");
            }
            else if (ToReturn.Lap == null)
            {
                throw new Exception("Unable to find lap packet for frame " + frame_identifier.ToString() + ".");
            }
            else if (ToReturn.Telemetry == null)
            {
                throw new Exception("Unable to find telemetry packet for frame " + frame_identifier.ToString() + ".");
            }
            else if (ToReturn.CarStatus == null)
            {
                throw new Exception("Unable to find car status packet for frame " + frame_identifier.ToString() + ".");
            }

            return ToReturn;
            
       
        }
    
        /// <summary>
        /// Creates all Packet frames for a particular session
        /// </summary>
        public static PacketFrame[] CreateAll(Packet[] all_session_packets)
        {
            List<PacketFrame> ToReturn = new List<PacketFrame>();
            foreach (Packet p in all_session_packets)
            {
                if (p.PacketType == PacketType.CarTelemetry) //We'll make car telemetry the root
                {
                    try
                    {
                        PacketFrame pf = PacketFrame.Create(all_session_packets, p.FrameIdentifier);
                        ToReturn.Add(pf);
                    }
                    catch
                    {
                        
                    }
                }
            }
            return ToReturn.ToArray();
        }
    }
}