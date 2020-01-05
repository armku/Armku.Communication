using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.Iterface
{
    /// <summary>
    /// ICounterLY
    /// </summary>
    public interface ICounterLY
    {
        /// <summary>数值</summary>
        Int64 Value { get; }

        /// <summary>增加</summary>
        /// <param name="amount">增加值</param>
        void Increment(Int64 amount = 1);
    }
}
