using System;
using System.Drawing;
using System.Windows.Forms;

namespace AVICapTest
{
    public partial class Form1 : Form
    {
        private readonly Camera _camera;
        private int[] _cord;
        private Bitmap _image;
        private Color _selectedPixelColor;
        private bool _captureButtonClickFlag;
        private int _previousXcord;
        private int _previousYcord;

        public Form1()
        {
            InitializeComponent();
            try
            {
                _camera = new Camera();
                Text = _camera.Devices[0].Name + " " + _camera.Devices[0].Version;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }


        private void StartButtonClick(object sender, EventArgs e)
        {
            
            _camera.Connect(capturePictureBox, _camera.Devices[0]);
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            _camera.Disconnect();
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void FramePictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            //Draw a cross across the frame picture box to select pixel
            Region region = new Region();
            region.Union(new Rectangle(0, _previousYcord, framePictureBox.Width, 1));
            region.Union(new Rectangle(_previousXcord, 0, 1, framePictureBox.Height));
            framePictureBox.Invalidate(region);
            framePictureBox.Update();
            Graphics graphics = Graphics.FromHwnd(framePictureBox.Handle);
            graphics.DrawLine(Pens.Black, 0, e.Y, framePictureBox.Width, e.Y);
            graphics.DrawLine(Pens.Black, e.X, 0, e.X, framePictureBox.Height);
            _previousXcord = e.X;
            _previousYcord = e.Y;
        }

        private void FramePictureBoxMouseLeave(object sender, EventArgs e)
        {
            framePictureBox.Invalidate();
        }

        private void CaptureButtonClick(object sender, EventArgs e)
        {
            _image = Camera.Scale(framePictureBox,_camera.Capture());
            framePictureBox.Image = _image;
        }
    
        private void FramePictureBoxMouseClick(object sender, MouseEventArgs e)
        {
            _selectedPixelColor = _image.GetPixel(e.X, e.Y);
            _cord = Camera.GetPixelLocation(_image,_selectedPixelColor,5);
            _captureButtonClickFlag = true;
        }

        private void FramePictureBoxPaint(object sender, PaintEventArgs e)
        {
            //called when you set image
            if (_image == null || _cord == null) return;
            Repaint(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_camera == null || _captureButtonClickFlag == false || _image == null) return;
            TimeTickWork();
        }

        private void RecordButtonClick(object sender, EventArgs e)
        {
            _camera.Record();
        }

        private void StopRecordButtonClick(object sender, EventArgs e)
        {
            _camera.StopRecord();
        }

        #region Other Functions
        private void Repaint(Graphics graphics)
        {
            const int width = 20;
            const int height = 20;
            Rectangle rect = new Rectangle(_cord[0], _cord[1], width, height);
            Rectangle rect2 = new Rectangle(_cord[2], _cord[3], width, height);
            Rectangle rect3 = new Rectangle(_cord[4], _cord[5], width, height);
            Rectangle rect4 = new Rectangle(_cord[6], _cord[7], width, height);

            //Configure Pen Style
            Pen greenPen = new Pen(Color.Green, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };

            //Draw Rectangles
            graphics.DrawRectangle(greenPen, rect);
            graphics.DrawRectangle(greenPen, rect2);
            graphics.DrawRectangle(greenPen, rect3);
            graphics.DrawRectangle(greenPen, rect4);
        }

        private void TimeTickWork()
        {
            Bitmap capture = _camera.Capture();
            if (capture == null) return;
            _image = Camera.Scale(framePictureBox, capture);
            _cord = Camera.GetPixelLocation(_image, _selectedPixelColor, 5);
            SetImage(framePictureBox,_image);
        }

        private static void SetImage(PictureBox pictureBox,Image image)
        {
            if (pictureBox.InvokeRequired)
                pictureBox.Invoke((MethodInvoker)delegate { SetImage(pictureBox,image); });
            else pictureBox.Image = image;
        }
        #endregion
    }
}
