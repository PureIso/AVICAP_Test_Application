using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//http://www.gentle.it/alvise/AVICAP.TXT
namespace AVICapTest
{
    public class Camera
    {
        /// <summary>
        /// The capGetDriverDescription function retrieves the version description of the capture driver.
        /// </summary>
        /// <param name="wDriverIndex">Index of the capture driver. The index can range from 0 through 9.</param>
        /// <param name="lpszName">Pointer to a buffer containing a null-terminated string corresponding to the capture driver name.</param>
        /// <param name="cbName">Length, in bytes, of the buffer pointed to by lpszName.</param>
        /// <param name="lpszVer">Pointer to a buffer containing a null-terminated string corresponding to the description of the capture driver.</param>
        /// <param name="cbVer">Length, in bytes, of the buffer pointed to by lpszVer.</param>
        /// <returns></returns>
        [DllImport("avicap32.dll")]
        private static extern bool capGetDriverDescription(
            short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszName,
            int cbName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer,
            int cbVer);


        /// <summary>
        /// The capCreateCaptureWindow function creates a capture window.
        /// </summary>
        /// <param name="lpszWindowName">Null-terminated string containing the name used for the capture window. </param>
        /// <param name="dwStyle">Window styles used for the capture window. Window styles are described with the CreateWindowEx function. </param>
        /// <param name="x">The x-coordinate of the upper left corner of the capture window. </param>
        /// <param name="y">The y-coordinate of the upper left corner of the capture window. </param>
        /// <param name="nWidth">Width of the capture window. </param>
        /// <param name="nHeight">Height of the capture window. </param>
        /// <param name="hWnd">Handle to the parent window. </param>
        /// <param name="nID">Window identifier. </param>
        /// <returns></returns>
        [DllImport("avicap32.dll")]
        private static extern int capCreateCaptureWindow(
            string lpszWindowName,
            int dwStyle, 
            int x, 
            int y, 
            int nWidth, 
            int nHeight,
            int hWnd, 
            int nID);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="wMsg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport("user32", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(
            int hwnd, 
            uint wMsg,
            int wParam,
            [MarshalAs(UnmanagedType.AsAny)] object lParam);

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order. </param>
        /// <param name="x">The new position of the left side of the window, in client coordinates. </param>
        /// <param name="y">The new position of the top of the window, in client coordinates. </param>
        /// <param name="cx">The new width of the window, in pixels. </param>
        /// <param name="cy">The new height of the window, in pixels. </param>
        /// <param name="uFlags">The window sizing and positioning flags. This parameter can be a combination of the following values. </param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern int SetWindowPos(
            int hwnd, 
            int hWndInsertAfter, 
            int x, int y, int cx, int cy,
            uint uFlags);

        /// <summary>
        /// Destroys the specified window. 
        /// </summary>
        /// <param name="hwnd">A handle to the window to be destroyed. </param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern bool DestroyWindow(int hwnd);

        public struct UpnpCaptureDriver
        {
            public int DeviceIndex;
            public string Name;
            public string Version;
        }

        private const short WmCapStart = 0x400;

        private const int WmCapDriverConnect = WmCapStart + 10;
        private const int WmCapDriverDisconnect = WmCapStart + 11;
        //Preview
        private const int WmCapSetPreview = WmCapStart + 50;
        private const int WmCapSetPreviewrate = WmCapStart + 52;
        private const int WmCapSetScale = WmCapStart + 53;

        private const int WmCapSequence = WmCapStart + 62;
        private const int WmCapFileSaveas = WmCapStart + 23;
        private const int WsChild = 0x40000000;
        private const int WsVisible = 0x10000000;

        private const int WmCapEditCopy = 0x41E;
        //private const integer WmCapSetOverlay = 0x433;

        private readonly List<UpnpCaptureDriver> _devices = new List<UpnpCaptureDriver>(10);
        private int _deviceHandle;
        private UpnpCaptureDriver _currentDevice;
        
        public Camera()
        {
            //Get device details
            for (byte i = 0; i < _devices.Capacity; i++)
            {
                string deviceName = "".PadLeft(80);
                string deviceVersion = "".PadLeft(80);

                if (!capGetDriverDescription(i,ref deviceName, 80, ref deviceVersion, 80)) 
                    continue;
                
                UpnpCaptureDriver device = new UpnpCaptureDriver
                    {
                        DeviceIndex = i,
                        Name = deviceName.Trim('\0'),
                        Version = deviceVersion.Trim('\0')
                    };
                _devices.Add(device); 
            }
        }

        public void Connect(Control control,UpnpCaptureDriver device)
        {
            _currentDevice = device;
            _deviceHandle = capCreateCaptureWindow(_currentDevice.DeviceIndex.ToString(), 
                WsVisible | WsChild,
                0, 0, 
                GetControlWidth(control),
                GetControlHeight(control), 
                GetControlHandle(control), 0);

            if (SendMessage(_deviceHandle, WmCapDriverConnect, _currentDevice.DeviceIndex, 0) <= 0)
                DestroyWindow(_deviceHandle);
            //set the preview scale
            SendMessage(_deviceHandle, WmCapSetScale, 1, 0);
            //overlay
            //SendMessage(_deviceHandle, WmCapSetOverlay, 1, 0);
            //set the preview rate (ms)
            SendMessage(_deviceHandle, WmCapSetPreviewrate, 66, 0);
            //start previewing the image
            SendMessage(_deviceHandle, WmCapSetPreview, 1, 0);
            

            //resize window to fit in PictureBox control
            SetWindowPos(_deviceHandle, 1, 0, 0, 
                GetControlWidth(control), 
                GetControlHeight(control), (2 | 4));
        }

        public void Record()
        {
            //start recording
            SendMessage(_deviceHandle, WmCapSequence, 0, 0);
        }

        public void StopRecord()
        {
            //start recording
            SendMessage(_deviceHandle, WmCapDriverDisconnect, _currentDevice.DeviceIndex, 0);
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                          "\\Clip.avi";
            if(File.Exists(path))
                File.Delete(path);
            SendMessage(_deviceHandle, WmCapFileSaveas, 0, path);
            DestroyWindow(_deviceHandle);
        }

        public void Disconnect()
        {
            DestroyWindow(_deviceHandle);
        }
        
        public Bitmap Capture()
        {
            if (SendMessage(_deviceHandle, WmCapEditCopy, _currentDevice.DeviceIndex, 0) > 0)
            {
                return (Bitmap)Clipboard.GetData(DataFormats.Bitmap);
            }
            return null;
        }

        public List<UpnpCaptureDriver> Devices
        {
            get { return _devices; }
        }

        [MTAThread]
        public static int[] GetPixelLocation(Bitmap bitmap, Color color, int range)
        {
            int maxRed = color.R + range;
            int minRed = color.R - range;
            int maxGreen = color.G + range;
            int minGreen = color.G - range;
            int maxBlue = color.B + range;
            int minBlue = color.B - range;

            List<int> column = new List<int>(4);
            List<int> rows = new List<int>(4);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);

                    if (pixel.R <= minRed || pixel.R >= maxRed || pixel.G <= minGreen || pixel.G >= maxGreen ||
                        pixel.B <= minBlue || pixel.B >= maxBlue) continue;

                    column.Add(x);
                    rows.Add(y);

                    if (column.Count > 5)
                    {
                        return new[]
                        {
                            column[0], rows[0], column[1], rows[1] ,
                            column[2], rows[2],column[3], rows[3]
                        };
                    }
                }
            }
            return null;
        }

        [MTAThread]
        public static Bitmap Scale(Control control, Image file)
        {
            Bitmap image = new Bitmap(file);

            float width = GetControlWidth(control);
            float height = GetControlHeight(control);
            
            float scale = Math.Min(width / image.Width, height / image.Height);
            int scaleWidth = (int)(image.Width * scale);
            int scaleHeight = (int)(image.Height * scale);

            Bitmap bmp = new Bitmap((int) width, (int)height);
            Graphics graph = Graphics.FromImage(bmp);
            
            //graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
            graph.DrawImage(image, new Rectangle((int) ((width - scaleWidth) / 2), 
                (int) ((height - scaleHeight) / 2), scaleWidth, scaleHeight));
            return bmp;
        }

        #region delegates
        private static int GetControlHeight(Control control)
        {
            int height = 0;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker) delegate { height = GetControlHeight(control); });
            else return control.Height;
            return height;
        }

        private static int GetControlWidth(Control control)
        {
            int width = 0;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { width = GetControlWidth(control); });
            else return control.Width;
            return width;
        }

        private static int GetControlHandle(Control control)
        {
            int handle = 0;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { handle = GetControlHandle(control); });
            else return control.Handle.ToInt32();
            return handle;
        }
        #endregion
    }
}
