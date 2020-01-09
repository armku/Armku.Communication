using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.Iterface
{
    /// <summary>管道。进站顺序，出站逆序</summary>
    [Description("管道")]
    public interface IPipeline 
    {
        #region 基础方法
        /// <summary>添加处理器到开头</summary>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        void AddFirst(IHandler handler);

        /// <summary>添加处理器到末尾</summary>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        void AddLast(IHandler handler);

        /// <summary>添加处理器到指定名称之前</summary>
        /// <param name="baseHandler">基准处理器</param>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        void AddBefore(IHandler baseHandler, IHandler handler);

        /// <summary>添加处理器到指定名称之后</summary>
        /// <param name="baseHandler">基准处理器</param>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        void AddAfter(IHandler baseHandler, IHandler handler);

        /// <summary>删除处理器</summary>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        void Remove(IHandler handler);
        /// <summary>
        /// 执行接收逻辑
        /// </summary>
        /// <param name="buf"></param>
        void DealInBuf(byte[] buf);
        /// <summary>
        /// 执行发送逻辑
        /// </summary>
        /// <param name="buf"></param>
        void DealOutBuf(byte[] buf);
        #endregion
    }

    /// <summary>管道。进站顺序，出站逆序</summary>
    public class Pipeline : IPipeline
    {
        /// <summary>
        /// 处理列表
        /// </summary>
        public List<IHandler> Handler = new List<IHandler>();

        #region 构造
        #endregion

        #region 方法
        /// <summary>添加处理器到开头</summary>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        public virtual void AddFirst(IHandler handler)
        {
            Handler.Insert(0, handler);
        }

        /// <summary>添加处理器到末尾</summary>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        public virtual void AddLast(IHandler handler)
        {
            Handler.Add(handler);
        }

        /// <summary>添加处理器到指定名称之前</summary>
        /// <param name="baseHandler">基准处理器</param>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        public virtual void AddBefore(IHandler baseHandler, IHandler handler)
        {

        }

        /// <summary>添加处理器到指定名称之后</summary>
        /// <param name="baseHandler">基准处理器</param>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        public virtual void AddAfter(IHandler baseHandler, IHandler handler)
        {

        }

        /// <summary>删除处理器</summary>
        /// <param name="handler">处理器</param>
        /// <returns></returns>
        public virtual void Remove(IHandler handler)
        {

        }
        #endregion

        #region 执行逻辑
        /// <summary>
        /// 执行接收逻辑
        /// </summary>
        /// <param name="buf"></param>
        public void DealInBuf(byte[] buf)
        {
            for (int i = Handler.Count-1; i>=0; i--)
            {
                if (buf == null)
                    break;
                var bufdo = Handler[i].DataInDeal(buf);
                buf = bufdo;
            }
        }
        /// <summary>
        /// 执行发送逻辑
        /// </summary>
        /// <param name="buf"></param>
        public void DealOutBuf(byte[] buf)
        {
            if (buf == null)
                return;
            for (int i = 0; i < Handler.Count; i++)
            {                
                var bufdo = Handler[i].DataOutDeal(buf);
                buf = bufdo;
            }
        }
        #endregion
    }
}
