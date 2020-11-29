using System;

namespace Codemasters.F1_2020
{
    public enum PacketType
    {
        Motion = 0, //1464 bytes
        Session = 1, //251 bytes
        Lap = 2, //1190 bytes
        Event = 3, //35 bytes
        Participants = 4, //1213 bytes
        CarSetup = 5, //1102 bytes
        CarTelemetry = 6, //1307 bytes
        CarStatus = 7, //1344 bytes
        FinalClassification = 8, //839 bytes    New to F1 2020.
        LobbyInfo = 9 //1169 bytes   New to F1 2020
    }
}