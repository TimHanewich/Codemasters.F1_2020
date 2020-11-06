Codemasters.F1_2020
===================
A package of resources for deserializing and analyzing UDP telemetry data from the F1 2020 video game by Codemasters.
------

The F1 2020 video game will provide to you a UDP data package in the form of a byte array (`byte[]`). There are several different types of data packets that the game will broadcast. Import the following namespace to use the basic resources for reading data:

    using Codemasters.F1_2020;

Every packet type derives from the same base class, `Packet`. The `Packet` class contains the following properties:  

`PacketFormat` - Describes the game the packet is from (F1 2020)  
`GameMajorVersion` - The major version of the game at the time of broadcast  
`GameMinorVersion` - The minor version of the game at the time of broadcast  
`PacketVersion`  
`PacketType` - The type of packet this data is (i.e. Telemetry, status, lap data, etc.)  
`UniqueSessionId` - The unique ID of your session.  
`SessioTime` - The timestamp of the current session when this packet was recorded and broadcasted.  
`FrameIdentifier` - Which grouping of packets this data belongs to.  
`PlayerCarIndex` - The index of the player's car in field arrays in all corresponding packets.  
`SecondaryPlayerCarIndex` - If you are playing two player (split screen), this will be player 2's index in the field array.  

To tell what type of packet any array of bytes that was provided to you is, use the CodemastersToolkit. The `bytes` variable below would come from your telemetry broadcast, or from a deserialized file if you saved telemetry on disk.

    byte[] bytes; //Your telemetry data package
    PacketType pt = CodemastersToolkit.GetPacketType(bytes);
    Console.WriteLine(pt.ToString());

Console output of the above: "CarTelemetry"  
Since we know that this particular data package is a telemetry packet, we can create a telemetry package:

    TelemetryPacket tp = new TelemetryPacket();
    tp.LoadBytes(bytes);

The `TelemetryPacket` class contains an array of `CarTelemetryData`, one for each car in the field.  
As an exmaple, the below code will print the throttle pressure that every driver is applying at the moment this data was broadcasted.

    foreach (TelemetryPacket.CarTelemetryData ctd in tp.FieldTelemetryData)
    {
        Console.WriteLine(ctd.Throttle.ToString());
    }

Many of the packets follow a similar format as is seen above with the `TelemetryPacket`.  

### Converting all telemetry to packets  
You can convert all of the byte array packages that you received. Example:  

    List<byte[]> telemetry;
    Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(telemetry);

You can then convert each packet from the returned array of packets. For example, converting a `Packet` to the `TelemetryPacket`:

    foreach (Packet p in packets)
    {
        if (p.PacketType == PacketType.CarTelemetry)
        {
            TelemetryPacket telpack = (TelemetryPacket)p;
        }
    }

### Getting a related packet
You may need to, for example, find the accompanying `CarStatusPacket` for a particular `TelemetryPacket`. To do this:

    TelemetryPacket telpack;
    CarStatusPacket csp = (CarStatusPacket)telpack.GetRelatedPacket(packets, PacketType.CarStatus);

**If you previously used the Codemasters.F1_2020.Analysis namespace and now find it missing: The analysis namespace has been moved under the ApexVisual.F1_2020 NuGet package (https://www.nuget.org/packages/ApexVisual.F1_2020/)