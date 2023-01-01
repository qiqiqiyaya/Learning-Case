// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");

TestFuc();
ArgumentNotNull(new TestResult(){AAA = "fdsafsd"});
Console.ReadKey();



void TestFuc([CallerMemberName] string memberName = default, [CallerFilePath] string filePath = default, [CallerLineNumber] int lineNumber = default)
{
    Console.WriteLine("memberName " + memberName);
    Console.WriteLine("filePath " + filePath);
    Console.WriteLine("lineNumber " + lineNumber);
}

void ArgumentNotNull(TestResult argument, [CallerArgumentExpression("argument")] string argumentExpression = default)
{
    Console.WriteLine("argumentExpression " + argument);
}


public class TestResult
{
    public string AAA { get; set; }
}