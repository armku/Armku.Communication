using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Armku.Communication.GPSDecode
{
    /// <summary>
    /// 位置数据
    /// </summary>
    public class GPSData
    {
        /// <summary>
		/// Time of day fix was taken
		/// </summary>
		[Description("Time of day fix was taken")]
        public TimeSpan FixTime { get; set; } = new TimeSpan();
        /// <summary>
        /// 纬度
        /// </summary>
        [Description("纬度")]
        public Double Latitude;
        /// <summary>
        /// 经度
        /// </summary>
        [Description("经度")]
        public Double Longitude;
        /// <summary>
        /// 卫星数 00-12
        /// </summary>
        [Description("卫星数 00-12")]
        public int NumberOfSatellites;
        /// <summary>
        /// 水平图形强度因子 0.500-99.000(大于6不可用)
        /// </summary>
        [Description("水平图形强度因子 0.500-99.000(大于6不可用)")]
        public Double Hdop;
        /// <summary>
        /// 海拔高度 -9999.9-9999.9
        /// </summary>
        [Description("海拔高度 -9999.9-9999.9")]
        public Double HeightOfGeoid;

        /// <summary>
        /// 海拔高度单位
        /// </summary>
        [Description("海拔高度单位")]
        public String HeightOfGeoidUnits;

        /// <summary>
        /// 真北航向 
        /// </summary>
        [Description("真北航向")]
        public Double TrueCourseOverGround;
        /// <summary>
        /// 磁北航向 
        /// </summary>
        [Description("磁北航向")]
        public Double MagneticCourseOverGround;
        /// <summary>
        /// 地面速率节 
        /// </summary>
        [Description("地面速率节")]
        public Double SpeedInKnots;
        /// <summary>
        /// 地面速率公里小时 
        /// </summary>
        [Description("地面速率公里小时 ")]
        public Double SpeedInKph;
    }
}
