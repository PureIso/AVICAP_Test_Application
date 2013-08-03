namespace AVICapTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.capturePictureBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.captureFrameButton = new System.Windows.Forms.Button();
            this.framePictureBox = new System.Windows.Forms.PictureBox();
            this.recordButton = new System.Windows.Forms.Button();
            this.stopRecordButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.capturePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.framePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(13, 13);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(13, 43);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // capturePictureBox
            // 
            this.capturePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.capturePictureBox.Location = new System.Drawing.Point(140, 13);
            this.capturePictureBox.Name = "capturePictureBox";
            this.capturePictureBox.Size = new System.Drawing.Size(273, 227);
            this.capturePictureBox.TabIndex = 2;
            this.capturePictureBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // captureFrameButton
            // 
            this.captureFrameButton.Location = new System.Drawing.Point(13, 72);
            this.captureFrameButton.Name = "captureFrameButton";
            this.captureFrameButton.Size = new System.Drawing.Size(75, 23);
            this.captureFrameButton.TabIndex = 5;
            this.captureFrameButton.Text = "Capture";
            this.captureFrameButton.UseVisualStyleBackColor = true;
            this.captureFrameButton.Click += new System.EventHandler(this.CaptureButtonClick);
            // 
            // framePictureBox
            // 
            this.framePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.framePictureBox.Location = new System.Drawing.Point(419, 13);
            this.framePictureBox.Name = "framePictureBox";
            this.framePictureBox.Size = new System.Drawing.Size(273, 227);
            this.framePictureBox.TabIndex = 8;
            this.framePictureBox.TabStop = false;
            this.framePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.FramePictureBoxPaint);
            this.framePictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FramePictureBoxMouseClick);
            this.framePictureBox.MouseLeave += new System.EventHandler(this.FramePictureBoxMouseLeave);
            this.framePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FramePictureBoxMouseMove);
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(13, 137);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(75, 23);
            this.recordButton.TabIndex = 9;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.RecordButtonClick);
            // 
            // stopRecordButton
            // 
            this.stopRecordButton.Location = new System.Drawing.Point(13, 166);
            this.stopRecordButton.Name = "stopRecordButton";
            this.stopRecordButton.Size = new System.Drawing.Size(75, 23);
            this.stopRecordButton.TabIndex = 10;
            this.stopRecordButton.Text = "Stop Record";
            this.stopRecordButton.UseVisualStyleBackColor = true;
            this.stopRecordButton.Click += new System.EventHandler(this.StopRecordButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 269);
            this.Controls.Add(this.stopRecordButton);
            this.Controls.Add(this.recordButton);
            this.Controls.Add(this.framePictureBox);
            this.Controls.Add(this.captureFrameButton);
            this.Controls.Add(this.capturePictureBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.capturePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.framePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.PictureBox capturePictureBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button captureFrameButton;
        private System.Windows.Forms.PictureBox framePictureBox;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button stopRecordButton;
    }
}

