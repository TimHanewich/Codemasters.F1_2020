using System;
using System.Collections.Generic;
using TimHanewich.Toolkit;

namespace Codemasters.F1_2020
{
    public class LobbyInfoPacket : Packet
    {
        public byte NumPlayers { get; set; }
        public LobbyInfoData[] LobbyPlayers { get; set; }

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            //Load header
            base.LoadBytes(BAM.NextBytes(24));

            this.NumPlayers = BAM.NextByte();
            this.LobbyPlayers = new LobbyInfoData[22];

            for (int i = 0; i < 22; i++)
            {
                var lobbyInfoData = new LobbyInfoData
                {
                    AiControlled = BAM.NextByte() == 1,
                    TeamId = CodemastersToolkit.GetTeamFromTeamId(BAM.NextByte()),
                    Nationality = BAM.NextByte(),
                    Name = ""
                };
                
                for (var t = 1; t <= 48; t++)
                {
                    var currentChar = Convert.ToChar(BAM.NextByte());
                    lobbyInfoData.Name += currentChar.ToString();
                }

                var readyStatusByte = BAM.NextByte();
                lobbyInfoData.ReadyStatus = readyStatusByte == 0 ? ReadyStatus.NotReady :
                    readyStatusByte == 1 ? ReadyStatus.Ready : ReadyStatus.Spectating;

                this.LobbyPlayers[i] = lobbyInfoData;
            }
        }
        
        public class LobbyInfoData
        {
            public bool AiControlled { get; set; }
            public Team TeamId { get; set; }
            public byte Nationality { get; set; }
            public string Name { get; set; }

            public ReadyStatus ReadyStatus { get; set; }
            
        }

        public enum ReadyStatus : byte
        {
            NotReady = 0,
            Ready = 1,
            Spectating = 2
        }
     
    }

}