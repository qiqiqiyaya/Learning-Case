using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Net4._7Caller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestFuc();
            Console.ReadKey();

        }

        static void TestFuc([CallerMemberName] string memberName = default, [CallerFilePath] string filePath = default, [CallerLineNumber] int lineNumber = default)
        {
            Console.WriteLine("memberName " + memberName);
            Console.WriteLine("filePath " + filePath);
            Console.WriteLine("lineNumber " + lineNumber);
        }


        public class TestResult
        {
            public string AAA { get; set; }
        }
    }
}


