using ModbusTest.Config;
using ModbusTest.Devices;
using ModbusTest.UIs;
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
            comboBox_PortName.Items.Clear();
            comboBox_PortName.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if(comboBox_PortName.Items.Count>0)
            {
                comboBox_PortName.SelectedIndex = comboBox_PortName.Items.Count - 1;
            }
            if(comboBox_DecodeType.Items.Count>0)
            {
                comboBox_DecodeType.SelectedIndex = comboBox_DecodeType.Items.Count - 1;
            }
            timerRefresh.Start();
            comboBox_PortName.Text=ComConfig.Current.Port ;
            comboBox_BaudRate.Text= ComConfig.Current.Baudrate.ToString();
            comboBox_DecodeType.SelectedIndex=ComConfig.Current.DecodeType;
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            ComConfig.Current.Port= comboBox_PortName.Text;
            ComConfig.Current.Baudrate = Convert.ToInt32(comboBox_BaudRate.Text);
            ComConfig.Current.DecodeType = comboBox_DecodeType.SelectedIndex;
            ComConfig.Current.Save();

            mb.Pipline.Bus.PortName = ComConfig.Current.Port;
            mb.Pipline.Bus.sp.BaudRate = ComConfig.Current.Baudrate;
            mb.DcodeType = ComConfig.Current.DecodeType;
            mb.Open();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            mb.Close();
        }

        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = mb.Status;
        }

        private void ButtonRead_Click(object sender, EventArgs e)
        {
            if (mb.Pipline.Bus.sp.IsOpen)
            {
                mb.RegHoildingRead(1, 0, 20);//读取温度
            }
            textBoxValue.Text =mb.Zero[0].ToString();
        }

        private void ButtonWrite_Click(object sender, EventArgs e)
        {
            mb.Zero[0] = Convert.ToSingle(textBoxValue.Text);
            mb.RegHoildingWrite(1, 0, 8);
        }
        /// <summary>
        /// 通信历史
        /// </summary>
        FormHis form;
        private void ButtonHis_Click(object sender, EventArgs e)
        {
            form = new FormHis(ref mb)               ;
            form.Show();   
        }
    }
}
