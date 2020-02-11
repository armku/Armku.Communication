using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// 线程安全队列
    /// </summary>
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
        public new void Clear()
        {
            lock(SyncObj)
            {
                base.Clear();
            }
        }
    }
}
