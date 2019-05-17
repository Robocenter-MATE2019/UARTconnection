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
        private string sendingdata = "";
        private int motorpower = 0;
        private int lightbrightness = 0;

        public int MotorPower
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
        public int LightBrightness
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
        public int Direction
        {
            get;
            set;
        }
        public string SendingData
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

        }

}
    
}
