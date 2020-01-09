using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.Iterface
{
    /// <summary>
    /// 处理器
    /// </summary>
    [Description("处理器")]
    public interface IHandler
    {
        /// <summary>
        /// 发送
        /// </summary>
        [Description("发送")]
        int TxCnt { get;}
        /// <summary>
        /// 接收
        /// </summary>
        [Description("接收")]
        int RxCnt { get;}
        /// <summary>
        /// 处理发送缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        byte[] DataOutDeal(byte[] buf);
        /// <summary>
        /// 处理接收缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        byte[] DataInDeal(byte[] buf);
    }
    /// <summary>处理器</summary>
    public abstract class Handler : IHandler
    {
        /// <summary>
        /// 发送
        /// </summary>
        [Description("发送")]
        public int TxCnt { get;protected set; } = 0;
        /// <summary>
        /// 接收
        /// </summary>
        [Description("接收")]
        public int RxCnt { get;protected set; } = 0;
        /// <summary>
        /// 处理发送缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public abstract byte[] DataOutDeal(byte[] buf);
        /// <summary>
        /// 处理接收缓冲区
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public abstract byte[] DataInDeal(byte[] buf);
    }
}
