using Armku.Communication.NmeaParser.Nmea;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Text;

namespace Armku.Communication.GPSDecoder
{
    /// <summary>
    /// GPS解码
    /// </summary>
    public class GPSDecode
    {
        /// <summary>
        /// 位置数据
        /// </summary>
        public GPSData gpsData = new GPSData();
        /// <summary>
        /// 头部
        /// </summary>
        private const Byte Head = (Byte)'$';
        /// <summary>
        /// 倒数第二尾部
        /// </summary>
        private const Byte Tail1 = 0X0D;
        /// <summary>
        /// 最后尾部
        /// </summary>
        private const Byte Tail2 = 0X0A;
        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort spGps = new SerialPort();
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public StringBuilder builderbuf = new StringBuilder();
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public Byte[] BufIn = new Byte[0x1000];
        /// <summary>
        /// 指针
        /// </summary>
        public int BufPos { get; set; } = 0;
        /// <summary>
        /// 是否打开
        /// </summary>
        public Boolean IsOpen { get; private set; } = false;
        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            if (IsOpen)
                return;
            IsOpen = true;
            spGps.BaudRate = 9600;
            spGps.DataReceived += SpGps_DataReceived;
            try
            {
                spGps.Open();
                //Console.WriteLine("GPS Port OPen:"+ spGps.PortName);
            }
            catch(Exception eGpsOpen)
            {
                Console.WriteLine("GPS Open Error");
                Console.WriteLine(eGpsOpen.ToString());
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpGps_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var sp = sender as SerialPort;
                var str = sp.ReadExisting();
                //Console.WriteLine(str);
                var buf = Encoding.ASCII.GetBytes(str);
                if (buf.Length + BufPos >= BufIn.Length)
                {
                    BufPos = 0;
                    Console.WriteLine("接收缓冲区超长，接收复位");
                    return;
                }
                Buffer.BlockCopy(buf, 0, BufIn, BufPos, buf.Length);
                BufPos += buf.Length;

                DealData();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 处理数据
        /// </summary>
        private void DealData()
        {
            //查找头部
            while (BufPos != 0)
            {
                if (BufIn[0] != Head)
                {
                    Buffer.BlockCopy(BufIn, 1, BufIn, 0, BufPos - 1);
                    BufPos--;
                    continue;
                }
                break;
            }
            //查找尾部
            var tail = SearchTail();
            if (tail < 0)
                return;
            var bufdo = new Byte[tail + 2];
            Buffer.BlockCopy(BufIn, 0, bufdo, 0, tail + 2);
            Buffer.BlockCopy(BufIn, tail + 2, BufIn, 0, BufPos - 2 - tail);
            BufPos = BufPos - 2 - tail;
            var str = Encoding.ASCII.GetString(bufdo);
            this.Decode(str);
            if(Double.IsNaN(this.gpsData.Latitude))
            {
                this.gpsData.Latitude = 0;
            }
            if (Double.IsNaN(this.gpsData.Longitude))
            {
                this.gpsData.Longitude = 0;
            }
            if (Double.IsNaN(this.gpsData.SpeedInKph))
            {
                this.gpsData.SpeedInKph = 0;
            }
            if (Double.IsNaN(this.gpsData.TrueCourseOverGround))
            {
                this.gpsData.TrueCourseOverGround = 0;
            }
            if (Double.IsNaN(this.gpsData.SpeedInKnots))
            {
                this.gpsData.SpeedInKnots = 0;
            }
            if (Double.IsNaN(this.gpsData.MagneticCourseOverGround))
            {
                this.gpsData.MagneticCourseOverGround = 0;
            }
            if (Double.IsNaN(this.gpsData.HeightOfGeoid))
            {
                this.gpsData.HeightOfGeoid = 0;
            }
            if (Double.IsNaN(this.gpsData.Hdop))
            {
                this.gpsData.Hdop = 0;
            }
            //Console.WriteLine("buflen:{0}",BufPos);
        }
        /// <summary>
        /// 查找尾部
        /// </summary>
        /// <returns></returns>
        private int SearchTail()
        {
            if (BufPos < 5)
                return -1;
            for (int i = 2; i < BufPos; i++)
            {
                if ((BufIn[i] == Tail2) && (BufIn[i - 1] == Tail1))
                {
                    return i - 1;
                }
            }

            return -1;
        }
        /// <summary>
        /// 字符串校验正常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private Boolean IsCheckSumOK(String str)
        {
            var ret = true;

            int checksum = 0;

            var idx = str.IndexOf('*');
            if (idx >= 0)
            {
                try
                {
                    checksum = Convert.ToInt32(str.Substring(idx + 1), 16);
                    str = str.Substring(0, str.IndexOf('*'));
                }
                catch(Exception e01)
                {
                    Console.WriteLine(e01.ToString());
                }
            }
            if (checksum > -1)
            {
                int checksumTest = 0;
                for (int i = 1; i < str.Length; i++)
                {
                    checksumTest ^= Convert.ToByte(str[i]);
                }
                if (checksum != checksumTest)
                    ret = false;
            }

            return ret;
        }
        /// <summary>
        /// 解码接收到的数据
        /// </summary>
        /// <param name="str"></param>
        private void Decode(String str)
        {
            int sumhead = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == (char)Head)
                {
                    sumhead++;
                }
            }
            str = str.Substring(0, str.Length - 2);//否则String.Split运行错误
            ///数据有重复，抛弃
            if (sumhead != 1)
                return;
            if(!IsCheckSumOK(str))
            {
                Console.WriteLine("CheckSumError");
                Console.WriteLine(str);
                return;
            }
            if (str.StartsWith("$GPGSV"))
            {
                //可见卫星信息
                //$GPGSV,4,2,13,31,48,273,18,10,41,188,35,12,25,043,16,26,14,206,*7E
            }
            else if (str.StartsWith("$GPGSA"))
            {
                //GPS定位信息
                //$GPGGA,065041.000,3617.4983,N,12017.8286,E,1,7,1.26,12.2,M,4.3,M,,*5A
            }
            else if (str.StartsWith("$GPGLL"))
            {
                //可见卫星信息
                //$GPGLL,3617.4983,N,12017.8286,E,065041.000,A,A*5B
            }
            else if (str.StartsWith("$GPGGA"))
            {
                //GPS定位信息
                //$GPGGA,064849.000,3617.5014,N,12017.8309,E,1,6,1.32,10.4,M,4.3,M,,*5B
                this.DecodeGGA(str);

            }
            else if (str.StartsWith("$GPVTG"))
            {
                //地面速度信息
                //$GPVTG,313.43,T,,M,0.31,N,0.58,K,A*34
                this.DecodeVTG(str);
            }
            else if (str.StartsWith("$GPRMC"))
            {
                //推荐定位信息
                //$GPRMC,065231.000,A,3617.4994,N,12017.8304,E,0.21,173.96,191219,,,A*6E
            }
            else
            {
                Console.WriteLine(str);
            }
        }
        /// <summary>
        /// 解码定位信息
        /// </summary>
        /// <param name="str"></param>
        private void DecodeGGA(String str)
        {
            try
            {
                var aa = NmeaMessage.Parse(str);
                var gga = aa as Gga;

                //Console.WriteLine("经度{0}",gga.Latitude);
                //Console.WriteLine("纬度{0}", gga.Longitude);
                //Console.WriteLine("高度{0}{1}", gga.HeightOfGeoid,gga.HeightOfGeoidUnits);
                //Console.WriteLine("时间{0}",gga.FixTime);

                this.gpsData.Latitude = gga.Latitude;
                this.gpsData.Longitude = gga.Longitude;
                this.gpsData.Hdop = gga.Hdop;
                this.gpsData.NumberOfSatellites = gga.NumberOfSatellites;
                this.gpsData.HeightOfGeoid = gga.Altitude;
                this.gpsData.HeightOfGeoidUnits = gga.AltitudeUnits;
                this.gpsData.FixTime = gga.FixTime;
            }
            catch (Exception edecode)
            {
                Console.WriteLine(edecode.ToString());
                return;
            }
        }
        /// <summary>
        /// 解码定位信息
        /// </summary>
        /// <param name="str"></param>
        private void DecodeVTG(String str)
        {
            try
            {
                var aa = NmeaMessage.Parse(str);
                var vtg = aa as Vtg;

                //Console.WriteLine("真北航向{0}", vtg.TrueCourseOverGround);
                //Console.WriteLine("磁北航向{0}", vtg.MagneticCourseOverGround);
                //Console.WriteLine("地面速率节{0}", vtg.SpeedInKnots);
                //Console.WriteLine("地面速率公里小时{0}", vtg.SpeedInKph);

                this.gpsData.TrueCourseOverGround = vtg.TrueCourseOverGround;
                this.gpsData.MagneticCourseOverGround = vtg.MagneticCourseOverGround;
                this.gpsData.SpeedInKnots = vtg.SpeedInKnots;
                this.gpsData.SpeedInKph = vtg.SpeedInKph;
            }
            catch (Exception edecode)
            {
                Console.WriteLine(edecode.ToString());
                return;
            }
        }
    }
}
