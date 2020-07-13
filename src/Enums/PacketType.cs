using System;

namespace Codemasters.F1_2020
{
    public enum PacketType
    {
        Motion,
        Session,
        Lap,
        Event,
        Participants,
        CarSetup,
        CarTelemetry,
        CarStatus,
        FinalClassification, //New to F1 2020
        LobbyInfo //New to F1 2020
    }
}