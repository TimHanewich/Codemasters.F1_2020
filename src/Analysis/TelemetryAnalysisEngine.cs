using System;
using System.Collections.Generic;
using TimHanewich.Csv;

namespace Codemasters.F1_2020.Analysis
{
    public class TelemetryAnalysisEngine
    {
        private PacketFrame[] Frames {get; set;}

        #region "Constructors"
        public static TelemetryAnalysisEngine Create(PacketFrame[] session_frames)
        {
            TelemetryAnalysisEngine ToReturn = new TelemetryAnalysisEngine();

            ToReturn.Frames = session_frames;

            return ToReturn;
        }

        public static TelemetryAnalysisEngine Create(Packet[] session_packets)
        {
            PacketFrame[] frames = PacketFrame.CreateAll(session_packets);
            TelemetryAnalysisEngine ToReturn = TelemetryAnalysisEngine.Create(frames);
            return ToReturn;
        }

        public static TelemetryAnalysisEngine Create(List<byte[]> session_bytes)
        {
            Packet[] packets = CodemastersToolkit.BulkConvertByteArraysToPackets(session_bytes);
            TelemetryAnalysisEngine ToReturn = TelemetryAnalysisEngine.Create(packets);
            return ToReturn;
        }
        #endregion

        public string PrintTelemetryToCsvContent(byte driver_index)
        {
            
            CsvFile csv = new CsvFile();


            #region "Write headers"

            //Write headers
            DataRow dr_header = csv.AddNewRow();

            //Motion packet
            dr_header.Values.Add("Position X");
            dr_header.Values.Add("Position Y");
            dr_header.Values.Add("Position Z");
            dr_header.Values.Add("Velocity X");
            dr_header.Values.Add("Velocity Y");
            dr_header.Values.Add("Velocity Z");
            dr_header.Values.Add("Forward Direction X");
            dr_header.Values.Add("Forward Direction Y");
            dr_header.Values.Add("Forward Direction Z");
            dr_header.Values.Add("Right Direction X");
            dr_header.Values.Add("Right Direction Y");
            dr_header.Values.Add("Right Direction Z");
            dr_header.Values.Add("Lateral G Force");
            dr_header.Values.Add("Longitudinal G Force");
            dr_header.Values.Add("Vertical G Force");
            dr_header.Values.Add("Yaw");
            dr_header.Values.Add("Pitch");
            dr_header.Values.Add("Roll");

            //Motion packet (only for player data, not for other drivers. Will be blank if requesting for other players)
            dr_header.Values.Add("Suspension Position - Rear Left");
            dr_header.Values.Add("Suspension Position - Rear Right");
            dr_header.Values.Add("Suspennsion Position - Front Left");
            dr_header.Values.Add("Suspension Position - Front Right");
            dr_header.Values.Add("Suspension Velocity - Rear Left");
            dr_header.Values.Add("Suspension Velocity - Rear Right");
            dr_header.Values.Add("Suspension Velocity - Front Left");
            dr_header.Values.Add("Suspension Velocity - Front Right");
            dr_header.Values.Add("Suspension Acceleration - Rear Left");
            dr_header.Values.Add("Suspension Acceleration - Rear Right");
            dr_header.Values.Add("Suspension Acceleration - Front Left");
            dr_header.Values.Add("Suspension Acceleration - Front Right");
            dr_header.Values.Add("Wheel Speed - Rear Left");
            dr_header.Values.Add("Wheel Speed - Rear Right");
            dr_header.Values.Add("Wheel Speed - Front Left");
            dr_header.Values.Add("Wheel Speed - Front Right");
            dr_header.Values.Add("Wheel Slip - Rear Left");
            dr_header.Values.Add("Wheel Slip - Rear Right");
            dr_header.Values.Add("Wheel Slip - Front Left");
            dr_header.Values.Add("Wheel Slip - Front Right");
            dr_header.Values.Add("Local Velocity X");
            dr_header.Values.Add("Local Velocity Y");
            dr_header.Values.Add("Local Velocity Z");
            dr_header.Values.Add("Angular Velocity X");
            dr_header.Values.Add("Angular Velocity Y");
            dr_header.Values.Add("Angular Velocity Z");
            dr_header.Values.Add("Angular Acceleration X");
            dr_header.Values.Add("Angular Acceleration Y");
            dr_header.Values.Add("Angular Acceleration Z");
            dr_header.Values.Add("Front Wheels Angle");

            //Lap Packet
            dr_header.Values.Add("Lap Distance");
            dr_header.Values.Add("Total Distance");
            dr_header.Values.Add("Safety Car Delta");
            dr_header.Values.Add("Car Race Position");
            dr_header.Values.Add("Current Lap Number");
            dr_header.Values.Add("Pit Status");
            dr_header.Values.Add("Sector");
            dr_header.Values.Add("Current Lap Invalid");
            dr_header.Values.Add("Driver Status");



            //Telemetry Packet
            dr_header.Values.Add("Speed KPH");
            dr_header.Values.Add("Speed MPH");
            dr_header.Values.Add("Throttle");
            dr_header.Values.Add("Steer");
            dr_header.Values.Add("Brake");
            dr_header.Values.Add("Clutch");
            dr_header.Values.Add("Gear");
            dr_header.Values.Add("Engine RPM");
            dr_header.Values.Add("DRS Active");
            dr_header.Values.Add("Rev Lights Percentage");
            dr_header.Values.Add("Brake Temperature - Rear Left");
            dr_header.Values.Add("Brake Temperature - Rear Right");
            dr_header.Values.Add("Brake Temperature - Front Left");
            dr_header.Values.Add("Brake Temperature - Front Right");
            dr_header.Values.Add("Tyre Surface Temperature - Rear Left");
            dr_header.Values.Add("Tyre Surface Temperature - Rear Right");
            dr_header.Values.Add("Tyre Surface Temperature - Front Left");
            dr_header.Values.Add("Tyre Surface Temperature - Front Right");
            dr_header.Values.Add("Tyre Inner Temperature - Rear Left");
            dr_header.Values.Add("Tyre Inner Temperature - Rear Right");
            dr_header.Values.Add("Tyre Inner Temperature - Front Left");
            dr_header.Values.Add("Tyre Inner Temperature - Front Right");
            dr_header.Values.Add("Engine Temperature Celsius");
            dr_header.Values.Add("Tyre Pressure - Rear Left");
            dr_header.Values.Add("Tyre Pressure - Rear Right");
            dr_header.Values.Add("Tyre Pressure - Front Left");
            dr_header.Values.Add("Tyre Pressure - Front Right");
            dr_header.Values.Add("Surface Type - Rear Left");
            dr_header.Values.Add("Surface Type - Rear Right");
            dr_header.Values.Add("Surface Type - Front Left");
            dr_header.Values.Add("Surface Type - Front Right");

            //Car Status
            dr_header.Values.Add("Traction Control Level");
            dr_header.Values.Add("Anti Lock Brakes Active");
            dr_header.Values.Add("Fuel Mix");
            dr_header.Values.Add("Front Brake Bias Percentage");
            dr_header.Values.Add("Pit Limiter Active");
            dr_header.Values.Add("Fuel In Tank");
            dr_header.Values.Add("Max Fuel Capacity");
            dr_header.Values.Add("Fuel Remaining in Laps");
            dr_header.Values.Add("Max RPM");
            dr_header.Values.Add("Idle RPM");
            dr_header.Values.Add("Max Gears");
            dr_header.Values.Add("DRS Allowed");
            dr_header.Values.Add("DRS Activation Distance");
            dr_header.Values.Add("Tyre Wear - Rear Left");
            dr_header.Values.Add("Tyre Wear - Rear Right");
            dr_header.Values.Add("Tyre Wear - Front Left");
            dr_header.Values.Add("Tyre Wear - Front Right");
            dr_header.Values.Add("Actual Tyre Compound");
            dr_header.Values.Add("Visual Tyre Compound");
            dr_header.Values.Add("Tyre Age in Laps");
            dr_header.Values.Add("Tyre Damage Percentage - Rear Left");
            dr_header.Values.Add("Tyre Damage Percentage - Rear Right");
            dr_header.Values.Add("Tyre Damage Percentage - Front Left");
            dr_header.Values.Add("Tyre Damage Percentage - Front Right");
            dr_header.Values.Add("Front Left Wing Damage Percentage");
            dr_header.Values.Add("Front Right Wing Damage Percentage");
            dr_header.Values.Add("Rear Wing Damage Percentage");
            dr_header.Values.Add("DRS Failure");
            dr_header.Values.Add("Engine Damage Percentage");
            dr_header.Values.Add("Gear Box Damage Percentage");
            dr_header.Values.Add("FIA Flag");
            dr_header.Values.Add("ERS Stored Energy Joules");
            dr_header.Values.Add("ERS Deploy Mode");
            dr_header.Values.Add("ERS Harvested This Lap by MGUK");
            dr_header.Values.Add("ERS Harvested This Lap by MGUH");
            dr_header.Values.Add("ERS Deployed This Lap");
            #endregion

            //Write the data
            foreach (PacketFrame frame in Frames)
            {
                //Create the new row
                DataRow row = csv.AddNewRow();

                //Write Write motion data
                MotionPacket.CarMotionData cmd = frame.Motion.FieldMotionData[driver_index];
                row.Values.Add(cmd.PositionX.ToString());
                row.Values.Add(cmd.PositionY.ToString());
                row.Values.Add(cmd.PositionZ.ToString());
                row.Values.Add(cmd.VelocityX.ToString());
                row.Values.Add(cmd.VelocityY.ToString());
                row.Values.Add(cmd.VelocityZ.ToString());
                row.Values.Add(cmd.ForwardDirectionX.ToString());
                row.Values.Add(cmd.ForwardDirectionY.ToString());
                row.Values.Add(cmd.ForwardDirectionZ.ToString());
                row.Values.Add(cmd.RightDirectionX.ToString());
                row.Values.Add(cmd.RightDirectionY.ToString());
                row.Values.Add(cmd.RightDirectionZ.ToString());
                row.Values.Add(cmd.gForceLateral.ToString());
                row.Values.Add(cmd.gForceLongitudinal.ToString());
                row.Values.Add(cmd.gForceVertical.ToString());
                row.Values.Add(cmd.Yaw.ToString());
                row.Values.Add(cmd.Pitch.ToString());
                row.Values.Add(cmd.Roll.ToString());

                //Write motion data player-specific (if they are requesting for the player, return the data. Otherwise, return 0's!)
                if (driver_index == frame.Motion.PlayerCarIndex)
                {
                    row.Values.Add(frame.Motion.SuspensionPosition.RearLeft.ToString());
                    row.Values.Add(frame.Motion.SuspensionPosition.RearRight.ToString());
                    row.Values.Add(frame.Motion.SuspensionPosition.FrontLeft.ToString());
                    row.Values.Add(frame.Motion.SuspensionPosition.FrontRight.ToString());
                    row.Values.Add(frame.Motion.SuspensionVelocity.RearLeft.ToString());
                    row.Values.Add(frame.Motion.SuspensionVelocity.RearRight.ToString());
                    row.Values.Add(frame.Motion.SuspensionVelocity.FrontLeft.ToString());
                    row.Values.Add(frame.Motion.SuspensionVelocity.FrontRight.ToString());
                    row.Values.Add(frame.Motion.SuspensionAcceleration.RearLeft.ToString());
                    row.Values.Add(frame.Motion.SuspensionAcceleration.RearRight.ToString());
                    row.Values.Add(frame.Motion.SuspensionAcceleration.FrontLeft.ToString());
                    row.Values.Add(frame.Motion.SuspensionAcceleration.FrontRight.ToString());
                    row.Values.Add(frame.Motion.WheelSpeed.RearLeft.ToString());
                    row.Values.Add(frame.Motion.WheelSpeed.RearRight.ToString());
                    row.Values.Add(frame.Motion.WheelSpeed.FrontLeft.ToString());
                    row.Values.Add(frame.Motion.WheelSpeed.FrontRight.ToString());
                    row.Values.Add(frame.Motion.WheelSlip.RearLeft.ToString());
                    row.Values.Add(frame.Motion.WheelSlip.RearRight.ToString());
                    row.Values.Add(frame.Motion.WheelSlip.FrontLeft.ToString());
                    row.Values.Add(frame.Motion.WheelSlip.FrontRight.ToString());
                    row.Values.Add(frame.Motion.VelocityX.ToString());
                    row.Values.Add(frame.Motion.VelocityY.ToString());
                    row.Values.Add(frame.Motion.VelocityZ.ToString());
                    row.Values.Add(frame.Motion.AngularVelocityX.ToString());
                    row.Values.Add(frame.Motion.AngularVelocityY.ToString());
                    row.Values.Add(frame.Motion.AngularVelocityZ.ToString());
                    row.Values.Add(frame.Motion.AngularAccelerationX.ToString());
                    row.Values.Add(frame.Motion.AngularAccelerationY.ToString());
                    row.Values.Add(frame.Motion.AngularAccelerationZ.ToString());
                    row.Values.Add(frame.Motion.FrontWheelAngle.ToString());
                }
                else
                {
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                    row.Values.Add("-");
                }
                

                //Lap Packet
                LapPacket.LapData ld = frame.Lap.FieldLapData[driver_index];
                row.Values.Add(ld.LapDistance.ToString());
                row.Values.Add(ld.TotalDistance.ToString());
                row.Values.Add(ld.SafetyCarDelta.ToString());
                row.Values.Add(ld.CarPosition.ToString());
                row.Values.Add(ld.CurrentLapNumber.ToString());
                row.Values.Add(ld.CurrentPitStatus.ToString());
                row.Values.Add(ld.Sector.ToString());
                row.Values.Add(ld.CurrentLapInvalid.ToString());
                row.Values.Add(ld.CurrentDriverStatus.ToString());

                //Telemetry packet
                TelemetryPacket.CarTelemetryData ctd = frame.Telemetry.FieldTelemetryData[driver_index];
                row.Values.Add(ctd.SpeedKph.ToString());
                row.Values.Add(ctd.SpeedMph.ToString());
                row.Values.Add(ctd.Throttle.ToString());
                row.Values.Add(ctd.Steer.ToString());
                row.Values.Add(ctd.Brake.ToString());
                row.Values.Add(ctd.Clutch.ToString());
                row.Values.Add(ctd.Gear.ToString());
                row.Values.Add(ctd.EngineRpm.ToString());
                row.Values.Add(ctd.DrsActive.ToString());
                row.Values.Add(ctd.RevLightsPercentage.ToString());
                row.Values.Add(ctd.BrakeTemperature.RearLeft.ToString());
                row.Values.Add(ctd.BrakeTemperature.RearRight.ToString());
                row.Values.Add(ctd.BrakeTemperature.FrontLeft.ToString());
                row.Values.Add(ctd.BrakeTemperature.FrontRight.ToString());
                row.Values.Add(ctd.TyreSurfaceTemperature.RearLeft.ToString());
                row.Values.Add(ctd.TyreSurfaceTemperature.RearRight.ToString());
                row.Values.Add(ctd.TyreSurfaceTemperature.FrontLeft.ToString());
                row.Values.Add(ctd.TyreSurfaceTemperature.FrontRight.ToString());
                row.Values.Add(ctd.TyreInnerTemperature.RearLeft.ToString());
                row.Values.Add(ctd.TyreInnerTemperature.RearRight.ToString());
                row.Values.Add(ctd.TyreInnerTemperature.FrontLeft.ToString());
                row.Values.Add(ctd.TyreInnerTemperature.FrontRight.ToString());
                row.Values.Add(ctd.EngineTemperature.ToString());
                row.Values.Add(ctd.TyrePressure.RearLeft.ToString());
                row.Values.Add(ctd.TyrePressure.RearRight.ToString());
                row.Values.Add(ctd.TyrePressure.FrontLeft.ToString());
                row.Values.Add(ctd.TyrePressure.FrontRight.ToString());
                row.Values.Add(ctd.SurfaceType_RearLeft.ToString());
                row.Values.Add(ctd.SurfaceType_RearRight.ToString());
                row.Values.Add(ctd.SurfaceType_FrontLeft.ToString());
                row.Values.Add(ctd.SurfaceType_FrontRight.ToString());

                //Car status
                CarStatusPacket.CarStatusData csd = frame.CarStatus.FieldCarStatusData[driver_index];
                row.Values.Add(csd.TractionControlStatus.ToString());
                row.Values.Add(csd.AntiLockBrakesOn.ToString());
                row.Values.Add(csd.SelectedFuelMix.ToString());
                row.Values.Add(csd.FrontBrakeBiasPercentage.ToString());
                row.Values.Add(csd.PitLimiterOn.ToString());
                row.Values.Add(csd.FuelLevel.ToString());
                row.Values.Add(csd.FuelCapacity.ToString());
                row.Values.Add(csd.FuelRemainingLaps.ToString());
                row.Values.Add(csd.MaxRpm.ToString());
                row.Values.Add(csd.IdleRpm.ToString());
                row.Values.Add(csd.MaxGears.ToString());
                row.Values.Add(csd.DrsAllowed.ToString());
                row.Values.Add(csd.DrsActivationDistance.ToString());
                row.Values.Add(csd.TyreWearPercentage.RearLeft.ToString());
                row.Values.Add(csd.TyreWearPercentage.RearRight.ToString());
                row.Values.Add(csd.TyreWearPercentage.FrontLeft.ToString());
                row.Values.Add(csd.TyreWearPercentage.FrontRight.ToString());
                row.Values.Add(csd.EquippedTyreCompound.ToString());
                row.Values.Add(csd.EquippedVisualTyreCompoundId.ToString());
                row.Values.Add(csd.TyreAgeLaps.ToString());
                row.Values.Add(csd.TyreDamagePercentage.RearLeft.ToString());
                row.Values.Add(csd.TyreDamagePercentage.RearRight.ToString());
                row.Values.Add(csd.TyreDamagePercentage.FrontLeft.ToString());
                row.Values.Add(csd.TyreDamagePercentage.FrontRight.ToString());
                row.Values.Add(csd.FrontLeftWingDamagePercent.ToString());
                row.Values.Add(csd.FrontRightWingDamagePercent.ToString());
                row.Values.Add(csd.RearWingDamagePercent.ToString());
                row.Values.Add(csd.DrsFailure.ToString());
                row.Values.Add(csd.EngineDamagePercent.ToString());
                row.Values.Add(csd.VehicleFiaFlag.ToString());
                row.Values.Add(csd.ErsStoredEnergyJoules.ToString());
                row.Values.Add(csd.SelectedErsDeployMode.ToString());
                row.Values.Add(csd.ErsHarvestedThisLapByMGUK.ToString());
                row.Values.Add(csd.ErsHarvestedThisLapByMGUH.ToString());
                row.Values.Add(csd.ErsDeployedThisLap.ToString());

            }

            return csv.GenerateAsCsvFileContent();

        }


    }
}