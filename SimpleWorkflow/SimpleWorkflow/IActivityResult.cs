using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkflow
{
    /// <summary>
    /// 一个activity执行后的结果
    /// 一个步骤执行后的结果
    /// </summary>
    public interface IActivityResult
    {
        public string GetValue();
    }
}
