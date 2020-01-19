using Armku.Communication.Bus;
using Armku.Communication.Iterface;
using Armku.Communication.Modbus.Iterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Armku.Communication.Modbus
{
    /// <summary>
    /// MODBUS通信基类
    /// </summary>
    [Description(" MODBUS通信基类")]
    public class ModBusBase : Handler
    {
        /// <summary>
        /// 通信接口
        /// </summary>
        [Description("通信接口")]
        public readonly ComBus Bus = new ComBus();
        /// <summary>
        /// 数据处理管道
        /// </summary>
        [Description("数据处理管道")]
        public Pipeline Pipline { get; set; }
        /// <summary>
        /// 通信历史
        /// </summary>
        [Description("通信历史")]
        public pipComHis ComHis = new pipComHis();
        /// <summary>
        /// 浮点数编码方式 0:big-endian 1:little-endian 2:big-endian byte swap 3:little-endian byte swap
        /// </summary>
        [Description("浮点数编码方式 0:big-endian 1:little-endian 2:big-endian byte swap 3:little-endian byte swap")]
        public int dcodeType { get; set; } = 3;
        /// <summary>
        /// 数组转换为浮点数
        /// </summary>
        /// <param name="values"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [Description("数组转换为浮点数")]
        protected float Ushorts2Float(ushort[] values, int pos)
        {
            float ret = 0;
            var byts = new Byte[4];
            var tmp = new Byte[4];
            Buffer.BlockCopy(values, pos * 2, tmp, 0, 4);
            switch (dcodeType)
            {
                case 0:
                    //0:big-endian
                    byts[0] = tmp[2];
                    byts[1] = tmp[3];
                    byts[2] = tmp[0];
                    byts[3] = tmp[1];
                    break;
                case 1:
                    //1:little-endian
                    byts[0] = tmp[1];
                    byts[1] = tmp[0];
                    byts[2] = tmp[3];
                    byts[3] = tmp[2];
                    break;
                case 2:
                    //2:big-endian byte swap
                    byts[0] = tmp[2];
                    byts[1] = tmp[3];
                    byts[2] = tmp[1];
                    byts[3] = tmp[0];
                    break;
                case 3:
                    //3:little-endian byte swap
                    byts[0] = tmp[0];
                    byts[1] = tmp[1];
                    byts[2] = tmp[2];
                    byts[3] = tmp[3];
                    break;
                default:
                    break;
            }
            ret = BitConverter.ToSingle(byts, 0);
            return ret;
        }
        /// <summary>
        /// 浮点数转换为Ushort数组
        /// </summary>
        /// <param name="da"></param>
        /// <param name="val"></param>
        /// <param name="pos"></param>
        [Description("浮点数转换为Ushort数组")]
        protected void Float2Ushort(float da, ref ushort[] val, int pos = 0)
        {
            var tmp0 = new Byte[4];
            var tmp = new Byte[4];
            Buffer.BlockCopy(BitConverter.GetBytes(da), 0, tmp0, 0, 4);
            
            switch (dcodeType)
            {
                case 0:
                    //0:big-endian
                    tmp[0] = tmp0[2];
                    tmp[1] = tmp0[3];
                    tmp[2] = tmp0[0];
                    tmp[3] = tmp0[1];
                    break;
                case 1:
                    //1:little-endian
                    tmp[0] = tmp0[1];
                    tmp[1] = tmp0[0];
                    tmp[2] = tmp0[3];
                    tmp[3] = tmp0[2];
                    break;
                case 2:
                    //2:big-endian byte swap
                    tmp[0] = tmp0[3];
                    tmp[1] = tmp0[2];
                    tmp[2] = tmp0[1];
                    tmp[3] = tmp0[0];
                    break;
                case 3:
                    //3:little-endian byte swap
                    tmp[0] = tmp0[0];
                    tmp[1] = tmp0[1];
                    tmp[2] = tmp0[2];
                    tmp[3] = tmp0[3];
                    break;
                default:
                    break;
            }
            Buffer.BlockCopy(tmp, 0, val, pos * 2, 4);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        [Description("初始化")]
        public void Init()
        {
            Pipline = new Pipeline();
            Pipline.AddLast(this);
            Pipline.AddLast(new PackageModbus());
            Pipline.AddLast(this.ComHis);
            Pipline.AddLast(Bus);
        }
        /// <summary>
        /// 输出继电器读取长度
        /// </summary>
        private int regCoilReadLength = 0;
        /// <summary>
        /// 输出继电器读取地址
        /// </summary>
        private int regCoilReadAddr = 0;
        /// <summary>
        /// 读取线圈寄存器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addr"></param>
        /// <param name="value"></param>
        [Description("读取线圈寄存器")]
        public void RegCoilRead(byte id, UInt16 addr, UInt16 len)
        {
            var buf = new byte[6];
            ReadInputAddress = addr;
            regCoilReadLength = len;
            regCoilReadAddr = addr;
            //01 02 00 00 00 0A F8 0D
            buf[0] = id;
            buf[1] = 0x02;
            buf[2] = Convert.ToByte(addr >> 8);
            buf[3] = Convert.ToByte(addr & 0xFF);
            buf[4] = Convert.ToByte(len >> 8);
            buf[5] = Convert.ToByte(len & 0xFF);
            Pipline.DealOutBuf(buf);

            System.Threading.Thread.Sleep(50);
            var bufrcv = Bus.Recv();
            if (bufrcv != null && bufrcv.Length != 0)
            {
                Pipline.DealInBuf(bufrcv);//此处为接收
            }
        }
        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        [Description("读取输入寄存器")]
        public void RegInputRead(Byte id, UInt16 addr, UInt16 len)
        {
            var buf = new byte[6];
            ReadInputAddress = addr;
            //01 04 00 00 00 0A 70 0D
            buf[0] = id;
            buf[1] = 0x04;
            buf[2] = Convert.ToByte(addr >> 8);
            buf[3] = Convert.ToByte(addr & 0xFF);
            buf[4] = Convert.ToByte(len >> 8);
            buf[5] = Convert.ToByte(len & 0xFF);
            Pipline.DealOutBuf(buf);

            System.Threading.Thread.Sleep(50);
            var bufrcv = Bus.Recv();
            if (bufrcv != null && bufrcv.Length != 0)
            {
                Pipline.DealInBuf(bufrcv);//此处为接收
            }
        }
        /// <summary>
        /// 写保持寄存器
        /// </summary>
        [Description("写保持寄存器")]
        public void RegHoildingWrite(Byte id, UInt16 addr, UInt16 len)
        {
            var buf = new byte[7 + len * 2];
            ReadInputAddress = addr;
            //Tx:01 10 00 13 00 01 02 00 7B E4 D0
            //通道19设置123
            buf[0] = id;
            buf[1] = 0x10;
            buf[2] = Convert.ToByte(addr >> 8);
            buf[3] = Convert.ToByte(addr & 0xFF);
            buf[4] = Convert.ToByte(len >> 8);
            buf[5] = Convert.ToByte(len & 0xFF);
            buf[6] = Convert.ToByte(len * 2);
            for (int i = 0; i < len; i++)
            {
                buf[7 + i * 2] = Convert.ToByte(RegHoilding[addr+i] >> 8);
                buf[8 + i * 2] = Convert.ToByte(RegHoilding[addr+i] & 0xff);
            }


            Pipline.DealOutBuf(buf);

            System.Threading.Thread.Sleep(50);
            var bufrcv = Bus.Recv();
            if (bufrcv != null && bufrcv.Length != 0)
            {
                Pipline.DealInBuf(bufrcv);//此处为接收
            }
        }
        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        [Description("读取保持寄存器")]
        public void RegHoildingRead(Byte id, UInt16 addr, UInt16 len)
        {
            var buf = new byte[6];
            ReadInputAddress = addr;
            //01 04 00 00 00 0A 70 0D
            buf[0] = id;
            buf[1] = 0x03;
            buf[2] = Convert.ToByte(addr >> 8);
            buf[3] = Convert.ToByte(addr & 0xFF);
            buf[4] = Convert.ToByte(len >> 8);
            buf[5] = Convert.ToByte(len & 0xFF);
            Pipline.DealOutBuf(buf);

            System.Threading.Thread.Sleep(50);
            var bufrcv = Bus.Recv();
            if (bufrcv != null && bufrcv.Length != 0)
            {
                Pipline.DealInBuf(bufrcv);//此处为接收
            }
        }
        /// <summary>
        /// 设置单个线圈寄存器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addr"></param>
        /// <param name="value"></param>
        [Description("设置单个线圈寄存器")]
        public void RegCoilWriteSingle(byte id, UInt16 addr)
        {
            var buf = new byte[6];
            ReadInputAddress = addr;
            //01 05 00 00 FF 00 8C 3A
            buf[0] = id;
            buf[1] = 0x05;
            buf[2] = Convert.ToByte(addr >> 8);
            buf[3] = Convert.ToByte(addr & 0xFF);
            buf[4] = Convert.ToByte(this.RegCoil[addr] ? 0XFF : 0X00);
            buf[5] = 0X00;
            Pipline.DealOutBuf(buf);

            System.Threading.Thread.Sleep(50);
            var bufrcv = Bus.Recv();
            if (bufrcv != null && bufrcv.Length != 0)
            {
                Pipline.DealInBuf(bufrcv);//此处为接收
            }
        }
        /// <summary>
        /// 打开
        /// </summary>
        [Description("打开")]
        public void Open()
        {
            Bus.sp.Close();
            Bus.Open();
            this.Init();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        public void Close()
        {
            Bus.sp.Close();
        }
        /// <summary>
        /// 通信状态字符串
        /// </summary>
        [Description("通信状态字符串")]
        public String Status
        {
            get
            {
                return Bus.Status;
            }
        }
        /// <summary>
        /// 读取的地址
        /// </summary>
        [Description("读取的地址")]
        public int ReadInputAddress { get; set; } = 0;
        [Description("")]
        protected byte ASCII_2_uint8(byte dat)
        {
            byte retVal = 0;
            if ('0' <= dat && dat <= '9')
            {
                retVal = Convert.ToByte(dat - '0');
            }
            else if ('A' <= dat && dat <= 'F')
            {
                retVal = Convert.ToByte(dat - 'A' + 10);
            }
            else if ('a' <= dat && dat <= 'f')
            {
                retVal = Convert.ToByte(dat - 'a' + 10);
            }
            return retVal;
        }
        [Description("")]
        protected float GetValueRaw(byte[] buf, int pos)
        {
            UInt16 b0, b1, b2, b3;
            if (buf.Length < pos + 4)
                return 0;
            b0 = ASCII_2_uint8(buf[pos]);
            b1 = ASCII_2_uint8(buf[pos + 1]);
            b2 = ASCII_2_uint8(buf[pos + 2]);
            b3 = ASCII_2_uint8(buf[pos + 3]);

            var ret = b0 << 12;
            ret += b1 << 8;
            ret += b2 << 4;
            ret += b3;

            return ret;
        }
        /// <summary>
        /// 输入寄存器，默认长度10000
        /// </summary>
        [Description("输入寄存器，默认长度10000")]
        public ushort[] RegInput = new ushort[10000];
        /// <summary>
        ///保持寄存器，默认长度10000
        /// </summary>
        [Description("保持寄存器，默认长度10000")]
        public ushort[] RegHoilding = new ushort[10000];
        /// <summary>
        ///输出继电器,默认长度1000
        /// </summary>
        [Description("输出继电器,默认长度1000")]
        public Boolean[] RegCoil = new Boolean[1000];

        /// <summary>
        /// 处理发送数据
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        [Description("处理发送数据")]
        public override byte[] DataOutDeal(byte[] buf)
        {
            TxCnt += buf.Length;
            return buf;
        }
        #region IHandler
        /// <summary>
        /// 处理缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataInDeal(byte[] buf)
        {
            //解码测量值
            var reglen = (buf.Length - 3) / 2;

            switch (buf[1])
            {
                case 2:
                    //输出继电器
                    for (int i = 0; i < regCoilReadLength; i++)
                    {
                        int pos = i % 8;
                        byte d = Convert.ToByte(1 << pos);
                        if ((buf[3] & d) != 0)
                            this.RegCoil[regCoilReadAddr+i] = true;
                        else
                            this.RegCoil[regCoilReadAddr+i] = false;
                    }
                    break;
                case 3:
                    ///保持寄存器
                    for (int i = 0; i < reglen; i++)
                    {
                        RegHoilding[ReadInputAddress + i] = buf[3 + i * 2];
                        RegHoilding[ReadInputAddress + i] <<= 8;
                        RegHoilding[ReadInputAddress + i] |= buf[4 + i * 2];
                    }
                    break;
                case 4:
                    //输入寄存器
                    for (int i = 0; i < reglen; i++)
                    {
                        RegInput[ReadInputAddress + i] = buf[3 + i * 2];
                        RegInput[ReadInputAddress + i] <<= 8;
                        RegInput[ReadInputAddress + i] |= buf[4 + i * 2];
                    }
                    break;
                default:
                    break;
            }
            RxCnt += buf.Length;
            return buf;
        }

        #endregion
        #region 备用
        /// <summary>
        /// getValueFloat
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public float GetValueFloat(byte[] buf, int pos)
        {
            var ret = BitConverter.ToSingle(buf, pos);

            return ret;
        }
        /// <summary>
        /// 浮点型数据到字节数组转换
        /// </summary>
        /// <param name="buf">数组</param>
        /// <param name="da">浮点数</param>
        /// <param name="pos">位置</param>
        public void FloatToBuf(byte[] buf, float da, int pos = 0)
        {
            var buff = BitConverter.GetBytes(da);
            for (int i = 0; i < 4; i++)
            {
                buf[i + pos] = buff[i];
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="cmd">指令码</param>
        /// <param name="buf">数据</param>
        public void SendStr(byte cmd, byte[] buf)
        {
            var bufsend = new byte[buf.Length + 1];
            bufsend[0] = cmd;
            Buffer.BlockCopy(buf, 0, bufsend, 1, buf.Length);
            SendStr(bufsend);
        }
        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="str">发送字符串</param>
        public void SendStr(byte[] str)
        {
            //this.PiplineSend.Write(str);
        }
        #endregion
    }
}
