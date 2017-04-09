using System;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQCore;
using RabbitMQCore.Configuration;

namespace RabbitMQSender
{
    public partial class Form1 : Form
    {
        private readonly IRabbitMQClient _rabbitClient;

        public Form1()
        {
            InitializeComponent();
            var conFactory = new UriRabbitMqConfiguration("GPSLocation",
                "amqp://kkxglvxw:WhGhDnh9_wSe-Jf_TTjwevZkG9A9bZhv@clam.rmq.cloudamqp.com/kkxglvxw");
            _rabbitClient = new RabbitMQClient(conFactory);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;
            _rabbitClient.SendMessage(txtMessage.Text);
            txtSentMessages.Text += Environment.NewLine + txtMessage.Text;
            txtMessage.Text = string.Empty;
        }
    }
}
