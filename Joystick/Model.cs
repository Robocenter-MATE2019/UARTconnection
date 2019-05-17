using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace UARTconnection
{


    public class Model
    {
        private static int Airpressure;
        private RotateTransform yawangle;
        private string advdepth;
        private static string sendingdata = "";
        private static int speedmode;
        private static int motorpower = 0;
        private static int lightbrightness = 0;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GM
        {
            public sbyte button_data1;
            public sbyte button_data2;
            public sbyte axisX_p;
            public sbyte axisY_p;
            public sbyte axisW_p;
            public sbyte manipulator_p;
            public sbyte slighter_p;
            public sbyte axisZ_p;

        };

        public static GM vGM;//M<

        public static int SpeedMode
        {
            get { return speedmode; }
            set { speedmode = value; }
        }
        public static int MotorPower
        {
            get
            {
                return motorpower;
            }
            set
            {
                motorpower = value;
            }
        }
        public static int LightBrightness
        {
            get
            {
                return lightbrightness;
            }
            set
            {
                lightbrightness = value;
            }
        }
        public static int Direction
        {
            get;
            set;
        }
        public static string SendingData
        {
            get
            {
                return sendingdata;
            }
            set
            {
                sendingdata = value;
            }
        }
        public Model()
        {
            speedmode      = 1;
        }

}
    
}
