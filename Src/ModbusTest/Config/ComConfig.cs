using NewLife.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTest.Config
{
    /// <summary>
    /// 端口配置
    /// </summary>
    [Description("端口配置")]
    [XmlConfigFile(@"Config/ComConfig.config")]
    class ComConfig : XmlConfig<ComConfig>
    {
        /// <summary>
        /// 端口号
        /// </summary>
        [Description("端口号")]
        public String Port { get; set; } = "COM1";
        /// <summary>
        /// 波特率
        /// </summary>
        [Description("波特率")]
        public int Baudrate { get; set; } = 9600;
        /// <summary>
        /// 浮点数编码方式
        /// </summary>
        [Description("浮点数编码方式")]
        public int DecodeType{ get; set; } = 3;
    }
}
