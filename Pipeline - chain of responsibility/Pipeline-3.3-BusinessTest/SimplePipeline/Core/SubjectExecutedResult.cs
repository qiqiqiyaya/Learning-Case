using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePipeline.Rule;

namespace SimplePipeline.Core
{
    public class SubjectExecutedResult
    {
        public string Status { get; set; }

        List<RuleCalculateResult> CalculateResults { get; set; }

    }
}
