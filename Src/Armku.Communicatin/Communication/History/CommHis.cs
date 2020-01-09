using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armku.Communication.History
{
    /// <summary>
    /// 通信值
    /// </summary>
    [Description("通信值")]
    public class CommValues
    {
        /// <summary>
        /// 通信历史
        /// </summary>
        public List<CommunicationLog> Values = new List<CommunicationLog>();
        public void Add(CommunicationLog val)
        {
            Values.Add(val);
            if (Values.Count > 10)
                Values.RemoveAt(0);
        }
    }
}
