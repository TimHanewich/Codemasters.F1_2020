using System;
using Codemasters.F1_2020;

namespace Codemasters.F1_2020.Analysis
{
    public class SessionAnalysis
    {
        public ulong SessionId {get; set;}
        public LapAnalysis[] Laps {get; set;}

        //For reporting purposes
        public float PercentLoadComplete;
        public bool LoadComplete;

        public void Load(Packet[] packets)
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
        }

    }
}