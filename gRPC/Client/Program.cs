// See https://aka.ms/new-console-template for more information

using Grpc.Core;
using Grpc.Net.Client;
using Proto.App;

using var channel = GrpcChannel.ForAddress("http://localhost:7267");
var client = new Calculator.CalculatorClient(channel);
var inputMessage = new InputMessage() { X = 1, Y = 0 };

await InvokeAsync(input => client.AddAsync(input), inputMessage, "+");
await InvokeAsync(input => client.SubstractAsync(input), inputMessage, "-");
await InvokeAsync(input => client.MutiplyAsync(input), inputMessage, "*");
await InvokeAsync(input => client.DivideAsync(input), inputMessage, "/");

static async Task InvokeAsync(Func<InputMessage, AsyncUnaryCall<OutputMessage>> invoker, InputMessage input,
    string @operator)
{
    var output = await invoker(input);
    if (output.Status == 0)
    {
        Console.WriteLine($"{input.X}{@operator}{input.Y}={output.Result}");
    }
    else
    {
        Console.WriteLine(output.Error);
    }
}

Console.Read();