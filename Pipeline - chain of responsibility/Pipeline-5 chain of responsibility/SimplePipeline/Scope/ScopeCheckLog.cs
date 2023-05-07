using Bogus.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SimplePipeline.Scope
{
    public class ScopeCheckLog
    {
        public ScopeCheckLog(string typeName, string json)
        {
            TypeName = typeName;
            Json = json;
        }

        public string TypeName { get; set; }

        public string Json { get; set; }

        public bool Successed { get; set; }

        public static ScopeCheckLog SuccessedLog(string typeName, string json)
        {
            return new ScopeCheckLog(typeName, json) { Successed = true };
        }

        public static ScopeCheckLog FailedLog(string typeName, string json)
        {
            return new ScopeCheckLog(typeName, json) { Successed = false };
        }
    }
}
