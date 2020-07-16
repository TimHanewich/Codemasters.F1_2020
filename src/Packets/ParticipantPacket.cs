using System;
using System.Collections.Generic;
using TimHanewichToolkit;

namespace Codemasters.F1_2020
{
    public class ParticipantPacket : Packet
    {

        public byte NumberOfActiveCars { get; set; }
        public ParticipantData[] FieldParticipantData { get; set; }

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);
            base.LoadBytes(BAM.NextBytes(24));

            NumberOfActiveCars = BAM.NextByte();

            List<ParticipantData> PDs = new List<ParticipantData>();
            int t = 1;
            for (t = 1; t <= 22; t++)
            {
                PDs.Add(ParticipantData.Create(BAM.NextBytes(54)));
            }
            FieldParticipantData = PDs.ToArray();
        }

        public class ParticipantData
        {
            public bool IsAiControlled { get; set; }
            public Driver PilotingDriver { get; set; }
            public Team ManufacturingTeam { get; set; }
            public byte CarRaceNumber { get; set; }
            public byte NationalityId { get; set; } //I'm too lazy to do this right now.  Will leave it as a byte ID for now... -Tim 1/26/2020
            public string Name { get; set; }
            public bool TelemetryPrivate { get; set; }

            public static ParticipantData Create(byte[] bytes)
            {
                ParticipantData ReturnInstance = new ParticipantData();
                ByteArrayManager BAM = new ByteArrayManager(bytes);

                //Get AI controlled
                byte nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.IsAiControlled = false;
                }
                else if (nb == 1)
                {
                    ReturnInstance.IsAiControlled = true;
                }


                //Get piloting driver
                ReturnInstance.PilotingDriver = CodemastersToolkit.GetDriverFromDriverId(BAM.NextByte(), Game.F1_2019);

                //Get Team
                ReturnInstance.ManufacturingTeam = CodemastersToolkit.GetTeamFromTeamId(BAM.NextByte());

                //Get race number
                ReturnInstance.CarRaceNumber = BAM.NextByte();

                //Get nationallity ID
                ReturnInstance.NationalityId = BAM.NextByte();

                //Get name
                string FullName = "";
                int t = 1;
                for (t = 1; t <= 48; t++)
                {
                    char ThisChar = Convert.ToChar(BAM.NextByte());
                    FullName = FullName + ThisChar.ToString();
                }
                ReturnInstance.Name = FullName.Trim();

                //Get telemetry private or not.
                nb = BAM.NextByte();
                if (nb == 0)
                {
                    ReturnInstance.TelemetryPrivate = true;
                }
                else if (nb == 1)
                {
                    ReturnInstance.TelemetryPrivate = false;
                }


                return ReturnInstance;
            }

        }
    }

}