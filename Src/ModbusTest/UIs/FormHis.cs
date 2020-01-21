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

namespace ModbusTest.UIs
{
    public partial class FormHis : Form
    {
        ModbusDevicesDemo mbhis;
        public FormHis(ref ModbusDevicesDemo mb)
        {
            mbhis = mb;
            InitializeComponent();
        }

        private void FormHis_Load(object sender, EventArgs e)
        {
            var dgv = dataGridView1;
            dgv.Rows.Clear();
            foreach(var v in mbhis.ComHis.CommHis.Values)
            {
                dgv.Rows.Add();
            }
            for (int i=0;i<mbhis.ComHis.CommHis.Values.Count;i++)
            {
                var v = mbhis.ComHis.CommHis.Values[i];
                dgv.Rows[i].Cells[0].Value=v.TimePoint.ToString("HH:mm:ss");
                dgv.Rows[i].Cells[1].Value = v.ComDirectStr;
                dgv.Rows[i].Cells[2].Value = v.ValueByte.Length.ToString(); 
                dgv.Rows[i].Cells[3].Value = v.ValueHexStr;
            }
        }
    }
}
