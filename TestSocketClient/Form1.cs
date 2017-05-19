using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSocketClient
{
    public partial class Form1 : Form
    {
        public delegate void SetMessage(string text);
        public SetMessage _myDelegate;

        private AsynchronousClient _asynClient = null;
        private AsynchronousClient asynClient
        {
            get
            {
                if (_asynClient == null)
                    _asynClient = new AsynchronousClient(this);
                return _asynClient;
            }
        }
        public Form1()
        {
            InitializeComponent();
            _myDelegate = new SetMessage(SetMessageMethod);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            //var result = await AsynchronousClient.StartClient();

            //new Thread(() =>
            //{
            //    string result = LongRunningMethod("World");
            //    Dispatcher.BeginInvoke((Action)(() => Label1.Content = result));

            //}).Start();
            //Label1.Content = "Working...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "MESSAGE: " + textBox1.Text;

            //new Action(async () =>
            //{
            //    await Task.Run(() => asynClient.StartClient(message));
            //}).Invoke();

            new Thread(async () =>
            {
                await Task.Run(() => asynClient.StartClient(message)); // msg
            }).Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "CONNECT";
            new Thread(async () =>
            {
                await Task.Run(() => asynClient.StartClient(message)); // connect
            }).Start();
        }

        public void SetMessageMethod(string text)
        {
            var oldText = txtMessage.Text;
            txtMessage.Text = "Message : " + text + Environment.NewLine + oldText;
        }



    }
}
