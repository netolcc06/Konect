using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectingTheDotsUserControl
{
    class ColorFrameReadyException : Exception
    {
        public ColorFrameReadyException(string msg) : base(msg) 
        { 
        
        }
    }
}
