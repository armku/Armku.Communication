using Armku.Communication.Modbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTest.Devices
{

    public class ModbusDevicesDemo : ModBusBase
    {
        /// <summary>
        /// 温度值-共3个通道
        /// </summary>
        public float[] Temperature = new float[3];

        /// <summary>
        /// 零点-目前支持2通道
        /// </summary>
        public float[] Zero = new float[2];
        /// <summary>
        /// 更新输入参数
        /// </summary>
        private void DealInParaUpdate()
        {
            Zero[0] = Ushorts2Float(RegHoilding, 0, 3);
            Zero[1] = Ushorts2Float(RegHoilding, 6, 3);

            this.Temperature[0] = Convert.ToSingle(RegInput[0] / 10.0);
            this.Temperature[1] = Convert.ToSingle(RegInput[2] / 10.0);
            this.Temperature[2] = Convert.ToSingle(RegInput[4] / 10.0);
        }

        /// <summary>
        /// 更新输出参数
        /// </summary>
        public void DealOutParaUpdate()
        {
            Float2Ushort(Zero[0], ref RegHoilding, 0,3);
            Float2Ushort(Zero[1], ref RegHoilding, 6,3);
        }
        #region 通信处理
        /// <summary>
        /// 处理缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataInDeal(byte[] buf)
        {
            var bb = base.DataInDeal(buf);


            DealInParaUpdate();

            return bb;
        }
        /// <summary>
        /// 处理发送数据
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        [Description("处理发送数据")]
        public override byte[] DataOutDeal(byte[] buf)
        {
            DealOutParaUpdate();
            return buf;
        }
        #endregion
    }
}

