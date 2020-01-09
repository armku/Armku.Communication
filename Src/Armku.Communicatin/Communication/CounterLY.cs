using Armku.Communication.Iterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication
{
    /// <summary>
    /// 统计
    /// </summary>
    [Description("统计")]
    public class CounterLY: ICounterLY
    {
        /// <summary>数值</summary>
        public Int64 Value { get; private set; } = 0;

        /// <summary>增加</summary>
        /// <param name="amount">增加值</param>
        public void Increment(Int64 amount = 1)
        {
            Value += amount;
        }
    }
}
