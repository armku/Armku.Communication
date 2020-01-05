using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.History
{
    /// <summary>
    /// 通信日志
    /// </summary>
    public class CommunicationLog
    {
        /// <summary>
        /// 时间点
        /// </summary>
        [Description("时间点")]
        public DateTime TimePoint { get; set; } = DateTime.Now;
        /// <summary>
        /// 通信方向 0:接收 其他：发送
        /// </summary>
        [Description("通信方向 0:发送 其他：接收")]
        public int ComDirect { get; set; } = 0;
        public String ComDirectStr
        {
            get
            {
                return ComDirect == 0 ? "发送" : "接收";
            }
        }
        /// <summary>
        /// 通信字符串
        /// </summary>
        [Description("通信字符串")]
        public String ValueStr
        {
            get
            {
                return Encoding.GetEncoding("GB1213").GetString(ValueByte); 
            }
        }
        /// <summary>
        /// 获取HEX表示
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private Byte _getStrHalf(byte b)
        {
            if (b < 10)
                return Convert.ToByte(b + '0');
            else
                return Convert.ToByte(b + 'A' - 10);
        }
        /// <summary>
        /// 获取hex表示
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private String _getStr(byte b)
        {
            var buf = new Byte[2];
            buf[0] = _getStrHalf(Convert.ToByte(b >> 4));
            buf[1]= _getStrHalf(Convert.ToByte(b & 0x0f));
          return  Encoding.GetEncoding("GB2312").GetString(buf);
        }
        /// <summary>
        /// 通信字符串-HEX格式
        /// </summary>
        [Description("通信字符串-HEX格式")]
        public String ValueHexStr
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var c in ValueByte)
                {
                    builder.Append(_getStr(c)).Append("-");
                }
                builder = builder.Remove(builder.Length - 1, 1);

                return builder.ToString();
            }
        }
        /// <summary>
        /// 通信字符串-HEX格式
        /// </summary>
        [Description("通信方向 0:接收 其他：发送")]
        public Byte[] ValueByte { get; set; } = null;
    }
}
