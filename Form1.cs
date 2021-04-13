using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace DigitalTimer
{
    public partial class Form1 : Form
    {
        //Data Members : 
        System.Timers.Timer scheduleTimer = new System.Timers.Timer();
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        String filePath = "";
        bool isBrowsed = false;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // This is to display the Circular Clock.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            // Now here, we need to make a new timer for the schedule.
            scheduleTimer.Interval = 1000;
            scheduleTimer.Elapsed += ScheduleTimer_Elapsed;
        }

        //Best way to make form moves lol...
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }


        //This method is for the schedule time...
        private void ScheduleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            DateTime userTime = dateTimePicker1.Value;
            //Now check if the current time is equal to the user time he scheduled..
            if (currentTime.Hour == userTime.Hour && currentTime.Minute == userTime.Minute && currentTime.Second == userTime.Second)
            {
                //Stop the alarm from calculating
                scheduleTimer.Stop();
                //Now Play a sound here ....
                try
                {
                    if (isBrowsed)
                    {
                        player.URL = filePath;
                    }
                    else
                    {
                        player.URL = "electronic_music.mp3";
                        
                    }
                    player.controls.play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //This method for the clock timer display ...
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            circularProgressBar1.Invoke((MethodInvoker)delegate
           {
               circularProgressBar1.Text = DateTime.Now.ToString("hh:mm:ss");
               circularProgressBar1.SubscriptText = DateTime.Now.ToString("tt");
               circularProgressBar1.Value = Convert.ToInt32(DateTime.Now.ToString("mm"));
               lblDayName.Text = DateTime.Now.ToString("dddd");
               lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
           });
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void scheduleBtn_Click(object sender, EventArgs e)
        {
            scheduleTimer.Start();
        }

        private void btnStopRing_Click(object sender, EventArgs e)
        {
            scheduleTimer.Stop();
            player.controls.stop();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //How to minimize ur cute application..
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.pathTextBox.Text = openFileDialog.FileName;
                filePath = openFileDialog.FileName;
                isBrowsed = true;
                
            }
        }

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isBrowsed = false;
            pathTextBox.Text = "electronic_music.mp3";
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            player.settings.setMode("loop", true);
        }
    }
}
