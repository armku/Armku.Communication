using System;
using System.Collections.Generic;
using System.Text;

namespace Armku.Communication.NmeaParser
{
    /// <summary>
    /// 位置数据
    /// </summary>
    public class GPSData
    {
        /// <summary>
		/// Time of day fix was taken
		/// </summary>
		public TimeSpan FixTime { get; set; } = new TimeSpan();
        /// <summary>
        /// 纬度
        /// </summary>
        public Double Latitude;
        /// <summary>
        /// 经度
        /// </summary>
        public Double Longitude;
        /// <summary>
        /// 卫星数 00-12
        /// </summary>
        public int NumberOfSatellites;
        /// <summary>
        /// 水平图形强度因子 0.500-99.000(大于6不可用)
        /// </summary>
        public Double Hdop;
        /// <summary>
        /// 海拔高度 -9999.9-9999.9
        /// </summary>
        public Double HeightOfGeoid;
        
        /// <summary>
        /// 海拔高度单位
        /// </summary>
        public String HeightOfGeoidUnits;
        /// <summary>
        /// 参考站号 0000~1023；不使用DGPS时为空
        /// </summary>
        public int 参考站号;

        /// <summary>
        /// 真北航向 
        /// </summary>
        public Double TrueCourseOverGround;
        /// <summary>
        /// 磁北航向 
        /// </summary>
        public Double MagneticCourseOverGround;
        /// <summary>
        /// 地面速率节 
        /// </summary>
        public Double SpeedInKnots;
        /// <summary>
        /// 地面速率公里小时 
        /// </summary>
        public Double SpeedInKph;
        /// <summary>
        /// 速度模式指示  A=自主定位，D=差分,E=估算,N=数据无效
        /// </summary>
        public String 速度模式指示;
    }
}
