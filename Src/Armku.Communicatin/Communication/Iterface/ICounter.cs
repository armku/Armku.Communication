using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.Iterface
{
    /// <summary>
    /// ICounterLY
    /// </summary>
    [Description("统计接口")]
    public interface ICounterLY
    {
        /// <summary>数值</summary>
        [Description("数值")]
        Int64 Value { get; }

        /// <summary>增加</summary>
        /// <param name="amount">增加值</param>
        [Description("增加")]
        void Increment(Int64 amount = 1);
    }
}
