using System;
using System.Collections.Generic;
using TimHanewichToolkit;

namespace Codemasters.F1_2020
{
    public class MotionPacket : Packet
    {
        public CarMotionData[] FieldMotionData { get; set; }

        //The below are the player's data
        public WheelDataArray SuspensionPosition { get; set; }
        public WheelDataArray SuspensionVelocity { get; set; }
        public WheelDataArray SuspensionAcceleration { get; set; }
        public WheelDataArray WheelSpeed { get; set; }
        public WheelDataArray WheelSlip { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float VelocityZ { get; set; }
        public float AngularVelocityX { get; set; }
        public float AngularVelocityY { get; set; }
        public float AngularVelocityZ { get; set; }
        public float AngularAccelerationX { get; set; }
        public float AngularAccelerationY { get; set; }
        public float AngularAccelerationZ { get; set; }
        public float FrontWheelAngle { get; set; }

        public override void LoadBytes(byte[] bytes)
        {
            ByteArrayManager BAM = new ByteArrayManager(bytes);

            //Update the header.
            base.LoadBytes(BAM.NextBytes(24));




            //Create car motion data
            int t = 1;
            List<CarMotionData> AllCarData = new List<CarMotionData>();
            for (t = 1; t <= 22; t++)
            {
                CarMotionData thiscardata = CarMotionData.Create(BAM.NextBytes(60));
                AllCarData.Add(thiscardata);
            }
            FieldMotionData = AllCarData.ToArray();


            //Get suspension position
            SuspensionPosition = new WheelDataArray();
            SuspensionPosition.RearLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionPosition.RearRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionPosition.FrontLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionPosition.FrontRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get suspension velocity
            SuspensionVelocity = new WheelDataArray();
            SuspensionVelocity.RearLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionVelocity.RearRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionVelocity.FrontLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionVelocity.FrontRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get suspension acceleration
            SuspensionAcceleration = new WheelDataArray();
            SuspensionAcceleration.RearLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionAcceleration.RearRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionAcceleration.FrontLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            SuspensionAcceleration.FrontRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get wheel speed
            WheelSpeed = new WheelDataArray();
            WheelSpeed.RearLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            WheelSpeed.RearRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            WheelSpeed.FrontLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            WheelSpeed.FrontRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get WheelSlip
            WheelSlip = new WheelDataArray();
            WheelSlip.RearLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            WheelSlip.RearRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            WheelSlip.FrontLeft = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            WheelSlip.FrontRight = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get velocity X,Y,Z
            VelocityX = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            VelocityY = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            VelocityZ = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get angular velocity X,Y,Z
            AngularVelocityX = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            AngularVelocityY = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            AngularVelocityZ = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get angular acceleration X,Y,Z
            AngularAccelerationX = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            AngularAccelerationY = BitConverter.ToSingle(BAM.NextBytes(4), 0);
            AngularAccelerationZ = BitConverter.ToSingle(BAM.NextBytes(4), 0);

            //Get front wheel angle
            FrontWheelAngle = BitConverter.ToSingle(BAM.NextBytes(4), 0);



        }

        //60 bytes
        public class CarMotionData
        {
            public float PositionX { get; set; }
            public float PositionY { get; set; }
            public float PositionZ { get; set; }
            public float VelocityX { get; set; }
            public float VelocityY { get; set; }
            public float VelocityZ { get; set; }
            public short ForwardDirectionX { get; set; }
            public short ForwardDirectionY { get; set; }
            public short ForwardDirectionZ { get; set; }
            public short RightDirectionX { get; set; }
            public short RightDirectionY { get; set; }
            public short RightDirectionZ { get; set; }
            public float gForceLateral { get; set; }
            public float gForceLongitudinal { get; set; }
            public float gForceVertical { get; set; }
            public float Yaw { get; set; }
            public float Pitch { get; set; }
            public float Roll { get; set; }


            /// <summary>
            /// Takes 60 bytes to make.
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            public static CarMotionData Create(byte[] bytes)
            {
                CarMotionData ReturnInstance = new CarMotionData();
                ByteArrayManager BAM = new ByteArrayManager(bytes);
                List<byte> ToConvert = new List<byte>();

                //Get position X,Y,Z
                ReturnInstance.PositionX = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.PositionY = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.PositionZ = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get Velocity X,Y,Z
                ReturnInstance.VelocityX = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.VelocityY = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.VelocityZ = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get forward direction X,Y,Z
                ReturnInstance.ForwardDirectionX = BitConverter.ToInt16(BAM.NextBytes(2), 0);
                ReturnInstance.ForwardDirectionY = BitConverter.ToInt16(BAM.NextBytes(2), 0);
                ReturnInstance.ForwardDirectionZ = BitConverter.ToInt16(BAM.NextBytes(2), 0);

                //Get right direction X,Y,Z
                ReturnInstance.RightDirectionX = BitConverter.ToInt16(BAM.NextBytes(2), 0);
                ReturnInstance.RightDirectionY = BitConverter.ToInt16(BAM.NextBytes(2), 0);
                ReturnInstance.RightDirectionZ = BitConverter.ToInt16(BAM.NextBytes(2), 0);

                //Get G Forces
                ReturnInstance.gForceLateral = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.gForceLongitudinal = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.gForceVertical = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                //Get Yaw, Pitch, Role
                ReturnInstance.Yaw = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.Pitch = BitConverter.ToSingle(BAM.NextBytes(4), 0);
                ReturnInstance.Roll = BitConverter.ToSingle(BAM.NextBytes(4), 0);

                return ReturnInstance;
            }

        }
    }

}