using Armku.Communication.Iterface;
using Armku.Communication.Modbus.Iterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.Bus
{
   public class ComBus: Handler
    {
        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort sp = new SerialPort();
        /// <summary>
        /// 端口号
        /// </summary>
        [Description("端口号")]
        public string PortName
        {
            get
            {
                return sp.PortName;
            }
            set
            {
                if (sp != null)
                {
                    if(sp.IsOpen)
                    {
                        sp.Close();
                        sp.PortName = value;
                        sp.Open();
                    }
                    else
                    {
                        sp.PortName = value;
                    }
                }
            }
        }
        public void Open()
        {
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            try
            {
                sp.Open();
            }
            catch(Exception eComOpen)
            {
                Console.WriteLine("Comm Open Error");
                Console.WriteLine(sp.PortName);
                Console.WriteLine(eComOpen.ToString());
            }
        }
        /// <summary>
        /// 通信状态
        /// </summary>
        [Description("通信状态")]
        public string Status
        {
            get
            {
                var builder = new StringBuilder();

                builder.Append(PortName).Append(" ").Append(sp.BaudRate.ToString()).Append(" ").Append(sp.IsOpen ? "Open " : "Close ");
                builder.Append("Rx:").Append(StatReceive.Value.ToString()).Append("-").Append("Tx:").Append(StatSend.Value.ToString());

                return builder.ToString();
            }
        }

        /// <summary>
        /// 发送数据包统计信息
        /// </summary>
        [Description("发送数据包统计信息")]
        public ICounterLY StatSend { get; set; } = new CounterLY();
        /// <summary>
        /// 接收数据包统计信息
        /// </summary>
        [Description("接收数据包统计信息")]
        public ICounterLY StatReceive { get; set; } = new CounterLY();
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public byte[] Recv()
        {
            if (sp.IsOpen)
            {
                var cnt = sp.BytesToRead;
                var databuf = new byte[cnt];
                var cntread = sp.Read(databuf, 0, cnt);
                return databuf;
            }
            else
            {
                return null;
            }
        }

        #region IHandler
        /// <summary>
        /// 处理发送缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataOutDeal(byte[] buf)
        {
            if (this.sp.IsOpen)
            {
                this.sp.Write(buf, 0, buf.Length);
                TxCnt += buf.Length;
                StatSend.Increment(buf.Length);
            }
            return buf;
        }
        /// <summary>
        /// 处理缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataInDeal(byte[] buf)
        {
            RxCnt += buf.Length;
            StatReceive.Increment(buf.Length);
            return buf;
        }
        #endregion
    }
}
