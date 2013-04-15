using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Windows;
using System.Windows.Shapes;
using Coding4Fun.Kinect.Wpf.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using System.Windows.Controls;



namespace KinectingTheDotsUserControl
{
    public class GameComponent
    {
        
        double _topBoundary; //tirar o static
        double _bottomBoundary;
        double _leftBoundary;
        double _rightBoundary;
        double _itemLeft;
        double _itemTop;
        byte[] colorBytes;
        Skeleton[] skeletons;
        KinectSensor runtime = KinectSensor.KinectSensors.FirstOrDefault();

        public void CheckButton(HoverButton button, Ellipse thumbStick)
        {


            if (IsItemMidpointInContainer(button, thumbStick))
            {
                button.Hovering();
                  
            }
            else
            {
                button.Release();
            }
        }



        public bool IsItemMidpointInContainer(FrameworkElement container, FrameworkElement target)
        {
            if (!container.IsVisible)
            {
                return false;
            }  
            FindValues(container, target);

            if (_itemTop < _topBoundary || _bottomBoundary < _itemTop)
            {
               return false;
            }

            if (_itemLeft < _leftBoundary || _rightBoundary < _itemLeft)
            {
               return false;
            }

            return true;
        }


       public void FindValues(FrameworkElement container, FrameworkElement target)
        {
            var containerTopLeft = container.PointToScreen(new Point());
            var itemTopLeft = target.PointToScreen(new Point());

            _topBoundary = containerTopLeft.Y;
            _bottomBoundary = _topBoundary + container.ActualHeight;
            _leftBoundary = containerTopLeft.X;
            _rightBoundary = _leftBoundary + container.ActualWidth;



            _itemLeft = itemTopLeft.X + (target.ActualWidth / 2);
            _itemTop = itemTopLeft.Y + (target.ActualHeight / 2);
        }

       public BitmapSource sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
       {
           using (var image = e.OpenColorImageFrame())
           {
               if (image == null)
                   throw(new ColorFrameReadyException("Out of color frame ready"));

               if (colorBytes == null ||
                   colorBytes.Length != image.PixelDataLength)
               {
                   colorBytes = new byte[image.PixelDataLength];
               }

               image.CopyPixelDataTo(colorBytes);

           
               int length = colorBytes.Length;
               //MessageBox.Show(length.ToString());
               for (int i = 0; i < length; i += 4)
               {
                   colorBytes[i + 3] = 255;
               }

               BitmapSource source = BitmapSource.Create(image.Width,
                   image.Height,
                   96,
                   96,
                   PixelFormats.Bgra32,
                   null,
                   colorBytes,
                   image.Width * image.BytesPerPixel);
               return source;

           }
       }

       public Skeleton SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
       {
           Skeleton data = null;
           using (var skeletonFrame = e.OpenSkeletonFrame())
           {
               if (skeletons == null ||
                   skeletons.Length != skeletonFrame.SkeletonArrayLength)
               {
                   skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
               }

               skeletonFrame.CopySkeletonDataTo(skeletons);

               data = (from s in skeletons
                                where s.TrackingState == SkeletonTrackingState.Tracked &&
                                      s.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked
                                select s).OrderBy(s => s.Joints[JointType.Head].Position.Z)
                                                   .FirstOrDefault();

               return data;

           }
       }

       public void SetEllipsePosition(Ellipse ellipse, Joint joint)
       {
           Microsoft.Kinect.SkeletonPoint vector = new Microsoft.Kinect.SkeletonPoint();
           vector.X = ScaleVector(800, joint.Position.X);
           vector.Y = ScaleVector(600, -joint.Position.Y);
           vector.Z = joint.Position.Z;

           Joint updatedJoint = new Joint();
           //updatedJoint.JointType = joint.JointType;
           updatedJoint.TrackingState = JointTrackingState.Tracked;
           updatedJoint.Position = vector;

           Canvas.SetLeft(ellipse, updatedJoint.Position.X);
           Canvas.SetTop(ellipse, updatedJoint.Position.Y);
       }

       private float ScaleVector(int length, float position)
       {
           float value = (((((float)length)) / 2f) * position) + (length / 2);
           if (value > length)
           {
               return (float)length;
           }
           if (value < 0f)
           {
               return 0f;
           }
           return value;
       }

       public void habilitarsensores()
       {

           if (runtime == null)
           {
               throw(new SensorsException("This application requires a Kinect sensor."));
           }

           runtime.Start();

           runtime.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

           runtime.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
           runtime.SkeletonStream.Enable();
       }

       public ImageSource nova_imagem(string x)
       {
           Uri novaImagem = new Uri(x, UriKind.Relative);
           ImageSource image = new BitmapImage(novaImagem);
           if(image == null)
               throw(new ImageNotFoundException("Image Not Found"));
           return image;

       }

    }
}