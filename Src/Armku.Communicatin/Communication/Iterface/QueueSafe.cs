using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// 线程安全队列
    /// </summary>
    [Description("线程安全队列")]
    public class QueueSafe<T> : Queue<T>
    {
        /// <summary>
        /// 线程安全锁
        /// </summary>
        private Object SyncObj = new object();
        /// <summary>
        /// 线程安全
        /// </summary>
        /// <returns></returns>
        [Description("线程安全")]
        public new T Dequeue()
        {
            lock(SyncObj)
            {
                return base.Dequeue();
            }
        }
        /// <summary>
        /// 线程安全
        /// </summary>
        /// <param name="item"></param>
        [Description("线程安全")]
        public new void Enqueue(T item)
        {
            lock(SyncObj)
            {
                base.Enqueue(item);
            }
        }
        /// <summary>
        /// 线程安全
        /// </summary>
        [Description("线程安全")]
        public new int Count 
        {
            get
            {
                lock(SyncObj)
                {
                    return base.Count;
                }
            } 
        }
        /// <summary>
        /// 线程安全
        /// </summary>
        [Description("线程安全")]
        public new void Clear()
        {
            lock(SyncObj)
            {
                base.Clear();
            }
        }
    }
}
