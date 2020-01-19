using ModbusTest.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusTest
{
    public partial class FormMain : Form
    {
        ModbusDevicesDemo mb = new ModbusDevicesDemo();
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            comboBoxDev1_PortName.Items.Clear();
            comboBoxDev1_PortName.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if(comboBoxDev1_PortName.Items.Count>0)
            {
                comboBoxDev1_PortName.SelectedIndex = comboBoxDev1_PortName.Items.Count - 1;
            }
            if(comboBoxDecodeType.Items.Count>0)
            {
                comboBoxDecodeType.SelectedIndex = comboBoxDecodeType.Items.Count - 1;
            }
            timerRefresh.Start();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            mb.Bus.PortName = comboBoxDev1_PortName.Text;
            mb.Bus.sp.BaudRate = Convert.ToInt32(textBoxDev1_BaudRate.Text);
            mb.DcodeType = comboBoxDecodeType.SelectedIndex;
            mb.Open();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            mb.Close();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = mb.Status;
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (mb.Bus.sp.IsOpen)
            {
                mb.RegHoildingRead(1, 0, 8);//读取温度
            }
            textBoxValue.Text =mb.Zero[0].ToString();
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            mb.Zero[0] = Convert.ToSingle(textBoxValue.Text);
            mb.RegHoildingWrite(1, 0, 8);
        }
    }
}
