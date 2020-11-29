using System;
using System.Collections.Generic;
using TimHanewich.Toolkit;

namespace Codemasters.F1_2020
{
    public class CarStatusPacket : Packet
    {
        public CarStatusData[] FieldCarStatusData { get; set; }

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager bam = new ByteArrayManager(bytes);
            base.LoadBytes(bam.NextBytes(24));


            List<CarStatusData> csds = new List<CarStatusData>();
            int t = 1;
            for (t = 1; t <= 22; t++)
            {
                csds.Add(CarStatusData.CreateFromBytes(bam.NextBytes(60)));
            }
            FieldCarStatusData = csds.ToArray();

        }

        public class CarStatusData
        {
            public TractionControlLevel TractionControlStatus { get; set; }
            public bool AntiLockBrakesOn { get; set; }
            public FuelMix SelectedFuelMix { get; set; }
            public byte FrontBrakeBiasPercentage { get; set; }
            public bool PitLimiterOn { get; set; }
            public float FuelLevel { get; set; }
            public float FuelCapacity { get; set; }
            public float FuelRemainingLaps { get; set; }
            public ushort MaxRpm { get; set; }
            public ushort IdleRpm { get; set; }
            public byte MaxGears { get; set; }
            public bool DrsAllowed { get; set; }
            public ushort DrsActivationDistance {get; set;} //New in F1 2020
            public WheelDataArray TyreWearPercentage { get; set; }
            public TyreCompound EquippedTyreCompound { get; set; }
            public byte EquippedVisualTyreCompoundId { get; set; }
            public byte TyreAgeLaps {get; set;} //New in F1 2020
            public WheelDataArray TyreDamagePercentage { get; set; }
            public byte FrontLeftWingDamagePercent { get; set; }
            public byte FrontRightWingDamagePercent { get; set; }
            public byte RearWingDamagePercent { get; set; }
            public bool DrsFailure {get; set;} //New in F1 2020
            public byte EngineDamagePercent { get; set; }
            public byte GearBoxDamagePercent { get; set; }
            public FiaFlag VehicleFiaFlag { get; set; }
            public float ErsStoredEnergyJoules { get; set; }
            public ErsDeployMode SelectedErsDeployMode { get; set; }
            public float ErsHarvestedThisLapByMGUK { get; set; }
            public float ErsHarvestedThisLapByMGUH { get; set; }
            public float ErsDeployedThisLap { get; set; }

            /// <summary>
            /// Takes 56 bytes
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            public static CarStatusData CreateFromBytes(byte[] bytes)
            {
                CarStatusData ToReturn = new CarStatusData();
                ByteArrayManager bam = new ByteArrayManager(bytes);

                byte nb = 0;

                //Traction control
                nb = bam.NextByte();
                if (nb == 0)
                {
                    ToReturn.TractionControlStatus = TractionControlLevel.Off;
                }
                else if (nb == 1)
                {
                    ToReturn.TractionControlStatus = TractionControlLevel.Low;
                }
                else if (nb == 2)
                {
                    ToReturn.TractionControlStatus = TractionControlLevel.High;
                }
                else
                {
                    ToReturn.TractionControlStatus = TractionControlLevel.Off;
                }

                //Anti-lock brakes
                if (bam.NextByte() == 1)
                {
                    ToReturn.AntiLockBrakesOn = true;
                }
                else
                {
                    ToReturn.AntiLockBrakesOn = false;
                }

                //Fuel mix
                nb = bam.NextByte();
                if (nb == 0)
                {
                    ToReturn.SelectedFuelMix = FuelMix.Lean;
                }
                else if (nb == 1)
                {
                    ToReturn.SelectedFuelMix = FuelMix.Standard;
                }
                else if (nb == 2)
                {
                    ToReturn.SelectedFuelMix = FuelMix.Rich;
                }
                else if (nb == 3)
                {
                    ToReturn.SelectedFuelMix = FuelMix.Max;
                }

                //Front Brake bias percentage
                ToReturn.FrontBrakeBiasPercentage = bam.NextByte();

                //Pit limiter
                if (bam.NextByte() == 1)
                {
                    ToReturn.PitLimiterOn = true;
                }
                else
                {
                    ToReturn.PitLimiterOn = false;
                }

                //Fuel in tank
                ToReturn.FuelLevel = BitConverter.ToSingle(bam.NextBytes(4), 0);

                //Fuel capacity
                ToReturn.FuelCapacity = BitConverter.ToSingle(bam.NextBytes(4), 0);

                //Fuel remaining laps 
                ToReturn.FuelRemainingLaps = BitConverter.ToSingle(bam.NextBytes(4), 0);

                //Max RPM
                ToReturn.MaxRpm = BitConverter.ToUInt16(bam.NextBytes(2), 0);

                //Idle RPM
                ToReturn.IdleRpm = BitConverter.ToUInt16(bam.NextBytes(2), 0);

                //Max gears
                ToReturn.MaxGears = bam.NextByte();

                //DRS allowed
                nb = bam.NextByte();
                if (nb == 1)
                {
                    ToReturn.DrsAllowed = true;
                }
                else
                {
                    ToReturn.DrsAllowed = false;
                }

                //DRS activation distance
                ToReturn.DrsActivationDistance = BitConverter.ToUInt16(bam.NextBytes(2), 0);


                //Tyre Wear
                ToReturn.TyreWearPercentage = new WheelDataArray();
                ToReturn.TyreWearPercentage.FrontLeft = Convert.ToSingle(bam.NextByte());
                ToReturn.TyreWearPercentage.FrontRight = Convert.ToSingle(bam.NextByte());
                ToReturn.TyreWearPercentage.RearLeft = Convert.ToSingle(bam.NextByte());
                ToReturn.TyreWearPercentage.RearRight = Convert.ToSingle(bam.NextByte());

                //Tyre Compound
                nb = bam.NextByte();
                if (nb == 16)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.C5;
                }
                else if (nb == 17)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.C4;
                }
                else if (nb == 18)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.C3;
                }
                else if (nb == 19)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.C2;
                }
                else if (nb == 20)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.C1;
                }
                else if (nb == 7)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.Inter;
                }
                else if (nb == 8)
                {
                    ToReturn.EquippedTyreCompound = TyreCompound.Wet;
                }

                //Tyre visual compound
                ToReturn.EquippedVisualTyreCompoundId = bam.NextByte();

                //Tyre age in laps
                ToReturn.TyreAgeLaps = bam.NextByte();

                //Tyre damage
                ToReturn.TyreDamagePercentage = new WheelDataArray();
                ToReturn.TyreDamagePercentage.FrontLeft = Convert.ToSingle(bam.NextByte());
                ToReturn.TyreDamagePercentage.FrontRight = Convert.ToSingle(bam.NextByte());
                ToReturn.TyreDamagePercentage.RearLeft = Convert.ToSingle(bam.NextByte());
                ToReturn.TyreDamagePercentage.RearRight = Convert.ToSingle(bam.NextByte());

                //Front left wing damage
                ToReturn.FrontLeftWingDamagePercent = bam.NextByte();

                //Front right wing damage
                ToReturn.FrontRightWingDamagePercent = bam.NextByte();

                //Rear wing damage
                ToReturn.RearWingDamagePercent = bam.NextByte();

                //DRS failure
                nb = bam.NextByte();
                if (nb == 0)
                {
                    ToReturn.DrsFailure = false;
                }
                else
                {
                    ToReturn.DrsFailure = true;
                }

                //Engine damage
                ToReturn.EngineDamagePercent = bam.NextByte();

                //Gear box damage
                ToReturn.GearBoxDamagePercent = bam.NextByte();

                //FIA Vehicle Flag
                sbyte fiavf = Convert.ToSByte(bam.NextByte());
                if (fiavf == -1)
                {
                    ToReturn.VehicleFiaFlag = FiaFlag.Unknown;
                }
                else if (fiavf == 0)
                {
                    ToReturn.VehicleFiaFlag = FiaFlag.None;
                }
                else if (fiavf == 1)
                {
                    ToReturn.VehicleFiaFlag = FiaFlag.Green;
                }
                else if (fiavf == 2)
                {
                    ToReturn.VehicleFiaFlag = FiaFlag.Blue;
                }
                else if (fiavf == 3)
                {
                    ToReturn.VehicleFiaFlag = FiaFlag.Yellow;
                }
                else if (fiavf == 4)
                {
                    ToReturn.VehicleFiaFlag = FiaFlag.Red;
                }


                //Ers Store energy
                ToReturn.ErsStoredEnergyJoules = BitConverter.ToSingle(bam.NextBytes(4), 0);

                //Ers deploy mode
                nb = bam.NextByte();
                if (nb == 0)
                {
                    ToReturn.SelectedErsDeployMode = ErsDeployMode.None;
                }
                else if (nb == 1)
                {
                    ToReturn.SelectedErsDeployMode = ErsDeployMode.Medium;
                }
                else if (nb == 2)
                {
                    ToReturn.SelectedErsDeployMode = ErsDeployMode.Overtake;
                }
                else if (nb == 3)
                {
                    ToReturn.SelectedErsDeployMode = ErsDeployMode.HotLap;
                }

                //Ers Harvested this lap by MGUK
                ToReturn.ErsHarvestedThisLapByMGUK = BitConverter.ToSingle(bam.NextBytes(4), 0);

                //Ers harvested this lap by MGUH
                ToReturn.ErsHarvestedThisLapByMGUH = BitConverter.ToSingle(bam.NextBytes(4), 0);

                //Ers deployed this lap
                ToReturn.ErsDeployedThisLap = BitConverter.ToSingle(bam.NextBytes(4), 0);



                return ToReturn;
            }
        }

    }

}