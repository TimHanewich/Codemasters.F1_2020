using System;
using TimHanewichToolkit;
using System.Collections.Generic;

namespace Codemasters.F1_2020
{
    /// <summary>
    /// Foundation class.  All other packet types inherit this class.  24 bytes.
    /// </summary>
    public class Packet
    {

        public Game PacketFormat { get; set; }
        public byte GameMajorVersion { get; set; }
        public byte GameMinorVersion { get; set; }
        public byte PacketVersion { get; set; }
        public PacketType PacketType { get; set; }
        public ulong UniqueSessionId { get; set; }
        public float SessionTime { get; set; }
        public uint FrameIdentifier { get; set; }
        public byte PlayerCarIndex { get; set; }
        public byte SecondaryPlayerCarIndex {get; set;} //New to F1 2020. The index of the secondayr player's car (splitscreen).  255 if no second player.

        public virtual void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);
            List<byte> ToConvert = new List<byte>();

            //Get packet format.
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ushort i = BitConverter.ToUInt16(ToConvert.ToArray(), 0);
            if (i.ToString() == "2017")
            {
                PacketFormat = Game.F1_2017;
            }
            else if (i.ToString() == "2018")
            {
                PacketFormat = Game.F1_2018;
            }
            else if (i.ToString() == "2019")
            {
                PacketFormat = Game.F1_2019;
            }
            else if (i.ToString() == "2020")
            {
                PacketFormat = Game.F1_2020;
            }
            else
            {
                PacketFormat = Game.Unknown;
            }
            ToConvert.Clear();

            //Get major version
            GameMajorVersion = BAM.NextByte();

            //Get minor version
            GameMinorVersion = BAM.NextByte();

            //Get packet version
            PacketVersion = BAM.NextByte();

            //Get packet type
            byte PackVer = BAM.NextByte();
            if (PackVer == 0)
            {
                PacketType = PacketType.Motion;
            }
            else if (PackVer == 1)
            {
                PacketType = PacketType.Session;
            }
            else if (PackVer == 2)
            {
                PacketType = PacketType.Lap;
            }
            else if (PackVer == 3)
            {
                PacketType = PacketType.Event;
            }
            else if (PackVer == 4)
            {
                PacketType = PacketType.Participants;
            }
            else if (PackVer == 5)
            {
                PacketType = PacketType.CarSetup;
            }
            else if (PackVer == 6)
            {
                PacketType = PacketType.CarTelemetry;
            }
            else if (PackVer == 7)
            {
                PacketType = PacketType.CarStatus;
            }
            else if (PackVer == 8)
            {
                PacketType = PacketType.FinalClassification;
            }
            else if (PackVer == 9)
            {
                PacketType = PacketType.LobbyInfo;
            }


            //Get unique session ID
            ToConvert.Clear();
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            ToConvert.Add(BAM.NextByte());
            UniqueSessionId = BitConverter.ToUInt64(ToConvert.ToArray(), 0);
            ToConvert.Clear();

            //Get session time
            SessionTime = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get frame identifier
            FrameIdentifier = BitConverter.ToUInt32(BAM.NextBytes(4), 0);

            //Get player car index
            PlayerCarIndex = BAM.NextByte();

            //Get secondary player car index
            SecondaryPlayerCarIndex = BAM.NextByte();
        }

        public Packet GetRelatedPacket(Packet[] family, PacketType desired_packet)
        {
            //Throw an error if it is trying to get itself (the same type)!
            if (desired_packet == this.PacketType)
            {
                throw new Exception("You are trying to get a related packet of the same type.");
            }

            //Get a list of the packet types that are the one they want.
            List<Packet> CorrectTypes = new List<Packet>();
            foreach (Packet p in family)
            {
                if (p.PacketType == desired_packet)
                {
                    CorrectTypes.Add(p);
                }
            }

            //Get the one that is most recent
            Packet winner = family[0];
            foreach (Packet p in CorrectTypes)
            {
                uint FrameDistance = FrameIdentifier - p.FrameIdentifier;
                uint WinningFrameDistance = FrameIdentifier - winner.FrameIdentifier;
                if (FrameDistance >= 0) //Only consider the ones that are BEHIND.  
                {
                    if (FrameDistance <= WinningFrameDistance)
                    {
                        winner = p;
                    }
                }
            }

            return winner;

        }
    }
}