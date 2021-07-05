using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace stream_test_core
{
    class Webcam
    {
        public static int cameraIndex = 1;
        Bitmap image;
        public static bool cancelFlag = false;

        VideoCapture vidCap = new VideoCapture(cameraIndex);

        public event EventHandler<CaptureEventArgs> OnNewFrame;

        public void Capture()
        {
            var args = new CaptureEventArgs();

            Mat frame = new Mat();
            vidCap.Open(cameraIndex);
            while (vidCap.IsOpened())
            {
                try
                {
                    vidCap.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    args.Image = image;

                    if (cancelFlag == true)
                    {
                        vidCap.Dispose();
                        cancelFlag = false;
                        break;
                    }
                    else
                    {
                        OnNewFrameRendered(args);
                    }
                }
                catch
                {
                    
                }
            }
        }

        protected virtual void OnNewFrameRendered(CaptureEventArgs e)
        {
            OnNewFrame?.Invoke(this, e);
        }

    }
}
