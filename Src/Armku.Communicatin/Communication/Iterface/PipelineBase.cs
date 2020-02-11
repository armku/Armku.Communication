﻿using Armku.Communication.Bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Armku.Communication.Iterface
{
    /// <summary>管道。进站顺序，出站逆序</summary>
    public class PipelineBase : IPipeline
    {
        /// <summary>
        /// 处理列表
        /// </summary>
        public List<IHandler> Handler = new List<IHandler>();
        /// <summary>
        /// 通信接口
        /// </summary>
        [Description("通信接口")]
        public readonly ComBus Bus = new ComBus();
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
        /// <summary>
        /// 数据发送接收延时时间-毫秒
        /// </summary>
        public int SleepMicroSeconds
        {
            get
            {
                int ret = 50;

                switch (this.Bus.sp.BaudRate)
                {
                    case 9600:
                        ret = 100;
                        break;
                    case 115200:
                        ret = 50;
                        break;
                    default:
                        break;
                }

                return ret;
            }
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
            for (int i = Handler.Count - 1; i >= 0; i--)
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
            this.Bus.DataOutDeal(buf);
        }
        #endregion
    }
}