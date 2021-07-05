using H.Socket.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace stream_test_core
{
    public partial class Form1 : Form
    {

        Thread camThread;
        Webcam webCap;
        //Task camCapture;
        string sid;
        bool receivedSid = false;
        bool isConnected = false;
        bool isLimbo = false;

        SocketIoClient client = new SocketIoClient();
        HttpClient httpClient = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            client.On("get_sid", message =>
            {
                sid = message;
                receivedSid = true;
            }
            );

            cbxCamera.Enabled = false;

            await client.ConnectAsync(new Uri("http://127.0.0.1:3000/"), namespaces: "stream");
            isLimbo = true;

            while (!receivedSid)
            {
                if (receivedSid)
                {
                    break;
                }
            }

            if (receivedSid)
            {
                receivedSid = false;

                Dictionary<string, string> content = new Dictionary<string, string>
                {
                {"user_id", txtUsername.Text },
                {"sid", sid }
                };

                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:3000/api/status", new FormUrlEncodedContent(content));
                string responseString = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                isLimbo = false;

                if (responseDict["status"] == "success")
                {
                    isConnected = true;
                    webCap.OnNewFrame += EmitImage;
                }
                else
                {
                    await client.DisconnectAsync();
                    isConnected = false;
                    MessageBox.Show(responseDict["message"]);
                }
            }
        }

        //private object lockObj = new object();
        Bitmap imgClone;
        byte[] imgBuffer;

        private void DisplayImage(object sender, CaptureEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            pictureBox1.Image = e.Image;
        }

        private void EmitImage(object sender, CaptureEventArgs e)
        {
            imgClone = (Bitmap)e.Image.Clone();

            using (var stream = new MemoryStream())
            {
                imgClone.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgBuffer = stream.ToArray();
                string base64 = Convert.ToBase64String(imgBuffer);
                string username = txtUsername.Text;
                string data = username + "|" + chckRecord.Checked.ToString() + "|" + base64;
                client.Emit("img", data, customNamespace: "stream");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            client.DisconnectAsync();
            isConnected = false;

            cbxCamera.Enabled = true;

            webCap.OnNewFrame -= EmitImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStop.Enabled = false;

            webCap = new Webcam();
            webCap.OnNewFrame += DisplayImage;

            camThread = new Thread(new ThreadStart(webCap.Capture));
            camThread.Start();
        }

        private void cbxCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            Webcam.cameraIndex = cbxCamera.SelectedIndex;
            Webcam.cancelFlag = true;

            Thread.Sleep(10);

            webCap = new Webcam();
            webCap.OnNewFrame += DisplayImage;

            camThread = new Thread(new ThreadStart(webCap.Capture));
            camThread.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isConnected)
            {
                chckRecord.Enabled = false;
                cbxCamera.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            else if (isLimbo)
            {
                chckRecord.Enabled = false;
                cbxCamera.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = false;
            }
            else
            {
                chckRecord.Enabled = true;
                cbxCamera.Enabled = true;
                btnStop.Enabled = false;
                btnStart.Enabled = true;
            }
        }
    }
}
