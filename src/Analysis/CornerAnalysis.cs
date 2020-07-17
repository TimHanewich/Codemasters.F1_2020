using System;

namespace Codemasters.F1_2020.Analysis
{
    public class CornerAnalysis
    {
        public byte CornerNumber {get; set;}
        public PacketFrame TelemetryData {get; set;}
        public TrackLocation CornerData {get; set;}
    }
}