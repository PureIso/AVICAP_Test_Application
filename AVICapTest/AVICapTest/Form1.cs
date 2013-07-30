using System;
using System.Windows.Forms;

namespace AVICapTest
{
    public partial class Form1 : Form
    {
        private Camera _camera;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Thread oThread = new Thread(CameraThread);
            //oThread.Start();
            _camera = new Camera();
            _camera.Connect(pictureBox1, _camera.Devices[0]);
        }

        private void CameraThread()
        {
            _camera = new Camera();
            _camera.Connect(pictureBox1, _camera.Devices[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _camera.Disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = _camera.Scale(pictureBox2,_camera.Capture());
        }
    }
}
