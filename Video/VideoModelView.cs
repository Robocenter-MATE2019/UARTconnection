using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UARTconnection
{
    public class VideoModelView
    {
        private VideoModel videoModel;

        public VideoCapture MainCapture
        {
            get { return videoModel.MainCamera; }
            set { videoModel.MainCamera = value; }
        }
        public VideoModelView(VideoModel videoModel)
        {
            this.videoModel = videoModel;
        }
    }
}
