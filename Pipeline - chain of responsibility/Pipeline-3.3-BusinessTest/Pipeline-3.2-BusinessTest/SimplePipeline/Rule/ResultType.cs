using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePipeline.Rule
{
    public enum ResultType
    {
        /// <summary>
        /// bool
        /// </summary>
        Boolean = 1,
        /// <summary>
        /// 数值
        /// </summary>
        Numerical = 2,
        /// <summary>
        /// 字符串
        /// </summary>
        String = 3,
        /// <summary>
        /// 字符串
        /// </summary>
        List = 4
    }
}
