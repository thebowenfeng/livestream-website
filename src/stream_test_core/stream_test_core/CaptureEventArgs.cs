using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace stream_test_core
{
    public class CaptureEventArgs : EventArgs
    {
        public Bitmap Image { get; set; }
    }
}
