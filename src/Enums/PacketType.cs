using System;

namespace Codemasters.F1_2020
{
    public enum PacketType
    {
        Motion, //1464 bytes
        Session, //251 bytes
        Lap, //1190 bytes
        Event, //35 bytes
        Participants, //1213 bytes
        CarSetup, //1102 bytes
        CarTelemetry, //1307 bytes
        CarStatus, //1344 bytes
        FinalClassification, //839 bytes    New to F1 2020.
        LobbyInfo //1169 bytes   New to F1 2020
    }
}