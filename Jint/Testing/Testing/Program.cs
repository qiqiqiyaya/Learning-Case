// See https://aka.ms/new-console-template for more information
using Jint;

Console.WriteLine("Hello, World!");






var engine = new Engine()
    .SetValue("log", new Action<object>(Console.WriteLine));

engine.Execute(@"
    function hello() { 
        log('Hello World');
    };
 
    hello();
");