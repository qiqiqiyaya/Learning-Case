// See https://aka.ms/new-console-template for more information





var waitForStop = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

Console.WriteLine("start");
Task.Run(async () =>
{
    await Task.Delay(5000);
    waitForStop.TrySetResult(null);
    Console.WriteLine("set value");
});

Console.WriteLine("wait");
await waitForStop.Task;
Console.WriteLine("complete");


