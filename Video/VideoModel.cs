using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UARTconnection
{
    public class VideoModel
    {
        private VideoCapture maincamera;

        public VideoCapture MainCamera
        {
            get { return maincamera; }
            set { maincamera = value; }
        }
    }
}
