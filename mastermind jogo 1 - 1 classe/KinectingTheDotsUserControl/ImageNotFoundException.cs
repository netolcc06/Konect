using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectingTheDotsUserControl
{
    class ImageNotFoundException : Exception
    {
        public ImageNotFoundException(string message) : base(message) 
        { 
        
        }
    }
}
