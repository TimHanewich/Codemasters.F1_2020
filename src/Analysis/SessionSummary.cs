using System;

namespace Codemasters.F1_2020.Analysis
{
    public class SessionSummary
    {
        public ulong SessionId {get; set;}
        public Track Circuit {get; set;}
        public SessionPacket.SessionType SessionMode {get; set;}
        public Team SelectedTeam {get; set;}
        public Driver SelectedDriver {get; set;}
        public string DriverName {get; set;}
        public DateTimeOffset SessionSummaryCreatedAt {get; set;}

        public static SessionSummary Create(Packet[] packets, byte driver_index)
        {
            if (packets.Length == 0)
            {
                throw new Exception("The length of the supplied packet array was 0!");
            }



            SessionSummary ToReturn = new SessionSummary();

            ToReturn.SessionId = packets[0].UniqueSessionId;
            
            //Get circuit
            foreach (Packet p in packets)
            {
                if (p.PacketType == PacketType.Session)
                {
                    SessionPacket sp = (SessionPacket)p;

                    //Circuit
                    ToReturn.Circuit = sp.SessionTrack;

                    //Session mode
                    ToReturn.SessionMode = sp.SessionTypeMode;
                }

                if (p.PacketType == PacketType.Participants)
                {
                    ParticipantPacket pp = (ParticipantPacket)p;

                    //Selected team
                    ToReturn.SelectedTeam = pp.FieldParticipantData[driver_index].ManufacturingTeam;

                    //Selected Driver
                    ToReturn.SelectedDriver = pp.FieldParticipantData[driver_index].PilotingDriver;

                    //Name
                    ToReturn.DriverName = pp.FieldParticipantData[driver_index].Name;
                }



            }


            ToReturn.SessionSummaryCreatedAt = DateTimeOffset.Now;
            return ToReturn;
        }

    }
}