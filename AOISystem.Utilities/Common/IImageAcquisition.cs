using System;

namespace AOISystem.Utilities.Common
{
    public delegate void ImageAcquiredEventHandler(object sender, int width, int height, IntPtr pointer);

    public delegate void ErrorRaisedEventHandler(object sender, int errorCode, string errorMsg);

    public enum GrabMode
    { 
        FreeRun,
        ImageTrigger,
        LineTrigger
    }

    public interface IImageAcquisition
    {
        event ImageAcquiredEventHandler ImageAcquired;

        event ErrorRaisedEventHandler ErrorRaised;

        bool OpenDevice();

        bool CloseDevice();

        bool Snap(GrabMode grabMode = GrabMode.FreeRun);

        bool Grab(GrabMode grabMode = GrabMode.FreeRun);

        bool Stop();

        void SetParam(string paramName, int paramValu);

        void SetParam(string paramName, string paramValu);

        void SetParam(string paramName, double paramValu);

        object InvokeMethod(string methodName, object[] parameters);
    }
}
