using Armku.Communication.Iterface;
using Armku.Communication.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Armku.Communication
{
    /// <summary>
    /// 通信历史
    /// </summary>
    [Description("通信历史")]
    public class pipComHis : Handler
    {
        /// <summary>
        /// 通信历史
        /// </summary>
        [Description("通信历史")]
        public CommValues CommHis = new CommValues();
        #region IHandler
        /// <summary>
        /// 处理发送缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataOutDeal(byte[] buf)
        {
            var ch = new CommunicationLog
            {
                ComDirect = 0,
                ValueByte = buf
            };
            this.CommHis.Add(ch);
            return buf;
        }

        /// <summary>
        /// 处理缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public override byte[] DataInDeal(byte[] buf)
        {
            var ch = new CommunicationLog
            {
                ComDirect = 1,
                ValueByte = buf
            };
            this.CommHis.Add(ch);
            return buf;
        }
        #endregion
    }
}

