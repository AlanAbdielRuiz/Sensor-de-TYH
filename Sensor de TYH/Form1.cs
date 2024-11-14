using System;
using System.IO.Ports;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Linq.Expressions;

namespace Sensor_de_TYH
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        bool puertoCerrado = false;
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.PortName = "COM6"; //cambia esto segun tu configuracion
            serialPort.BaudRate = 9600;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (puertoCerrado == false)
            {
                conectar();
            }
            else
            {
                noConectado();
            }
        }
        private void conectar()
        {
            try
            {
                puertoCerrado = true;
                serialPort.Open();
                button1.Text = "Desconectar";
                button1.BackColor = Color.Black;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            this.Invoke(new MethodInvoker(delegate
            {
                string[] Values = data.Split('\t');
                if (Values.Length == 2)
                {
                    label1.Text = Values[1];
                    label2.Text = Values[0];
                    listBox1.Items.Add(Values[1] + "   " + Values[0]);
                }
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void noConectado()
        {
            try
            {
                puertoCerrado = false;
                serialPort.Close();
                button1.Text = "Conectar";
                button1.BackColor = Color.Blue;
                listBox1.Items.Clear();
                label1.Text = "TEMPERATURA C";
                label2.Text = "HUMEDAD %";
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }
        }
    }
}
