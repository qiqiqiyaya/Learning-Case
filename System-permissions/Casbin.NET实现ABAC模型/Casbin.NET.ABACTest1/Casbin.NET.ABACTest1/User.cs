using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casbin.NET.ABACTest1
{
    public class User
    {
        public string Id { get; set; }
        public string Department { get; set; }
        public int SecurityLevel { get; set; }
    }
}
