using Armku.Communication.Iterface;
using Armku.Communication.Modbus.Iterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.Modbus
{
    /// <summary>
    /// 数据封包，拆包
    /// </summary>
    public class PackageModbus : Handler
    {
        #region IHandler
        /// <summary>
        /// 处理发送缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataOutDeal(byte[] buf)
        {
            var bufout = new byte[buf.Length + 2];
            Buffer.BlockCopy(buf, 0, bufout, 0, buf.Length);

            var chkss=  Packet485Helper.CrcByte(buf);
            bufout[bufout.Length - 2] = chkss[0];
            bufout[bufout.Length - 1] = chkss[1];
                        
            return bufout;
        }
        
        /// <summary>
        /// 处理缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataInDeal(byte[] buf)
        {
            if (buf == null)
                return buf;
            if (buf.Length < 4)
                return null;
            var chkss = Packet485Helper.CrcByte(buf,0,buf.Length-2);
            if((chkss[0]==buf[buf.Length-2]) && (chkss[1]==buf[buf.Length-1]))
            {
                var bufout = new Byte[buf.Length - 2];
                Buffer.BlockCopy(buf, 0, bufout, 0, bufout.Length);
                return bufout;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
