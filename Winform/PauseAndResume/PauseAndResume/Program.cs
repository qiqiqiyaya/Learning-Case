


Test().Wait();
Console.WriteLine("Press enter to exit...");
Console.ReadLine();

static async Task SomeMethodAsync(PauseToken pause)
{
    try
    {
        while (true)
        {
            await Task.Delay(1000).ConfigureAwait(false);
            Console.WriteLine("Before await pause.WaitWhilePausedAsync()");
            
            await pause.WaitAsync();
            Console.WriteLine("After await pause.WaitWhilePausedAsync()");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: {0}", e);
        throw;
    }
}

static async Task Test()
{
    var pts = new PauseTokenSource();
    var task = SomeMethodAsync(pts.Token);

    // sync version
    Console.WriteLine("Before pause requested");
    pts.PauseAsync().Wait();
    Console.WriteLine("After pause requested, paused: " + pts.Token.IsPaused);
    pts.PauseAsync().Wait();
    Console.WriteLine("After pause requested, paused: " + pts.Token.IsPaused);
    Console.WriteLine("Press enter to resume...");
    Console.ReadLine();
    pts.Resume();

    // async version:

    Console.WriteLine("Before pause requested");
    await pts.PauseAsync();
    Console.WriteLine("After pause requested, paused: " + pts.Token.IsPaused);
    await pts.PauseAsync();
    Console.WriteLine("After pause requested, paused: " + pts.Token.IsPaused);
    Console.WriteLine("Press enter to resume...");
    Console.ReadLine();
    pts.Resume();


    Console.WriteLine("Before pause requested");
    pts.Pause();
    Console.WriteLine("After pause requested, paused: " + pts.Token.IsPaused);
    Console.WriteLine("Press enter to resume after the task has confirmed paused...");
}


public struct PauseToken
{
    private readonly PauseTokenSource _source;
    public PauseToken(PauseTokenSource source) { this._source = source; }

    public bool IsPaused => _source != null && _source.IsPaused;

    /// <summary>
    /// 暂停等待
    /// </summary>
    /// <returns></returns>
    public Task WaitAsync()
    {
        return IsPaused ?
            _source.WaitAsync() :
            PauseTokenSource.CompletedTask;
    }
}


public class PauseTokenSource
{
    private readonly object _lock = new Object();
    bool _paused = false; // could use resumeRequest as flag too

    internal static readonly Task CompletedTask = Task.FromResult(true);
    /// <summary>
    /// 暂停响应
    /// </summary>
    private TaskCompletionSource<bool>? _pauseResponse;
    /// <summary>
    /// 重用请求
    /// </summary>
    private TaskCompletionSource<bool>? _resumeRequest;

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        lock (_lock)
        {
            // 已暂停，直接放回
            if (_paused)
                return;
            _paused = true;
            _pauseResponse = null;
            _resumeRequest = new TaskCompletionSource<bool>();
        }
    }

    public void Resume()
    {
        TaskCompletionSource<bool>? resumeRequest = null;

        lock (_lock)
        {
            if (!_paused)
                return;
            _paused = false;
            resumeRequest = _resumeRequest;
            _resumeRequest = null;
        }

        resumeRequest?.TrySetResult(true);
    }

    // pause with feedback
    // that the producer task has reached the paused state
    public Task PauseAsync()
    {
        Task? responseTask = null;

        lock (_lock)
        {
            if (_paused)
                return _pauseResponse?.Task!;
            _paused = true;
            _pauseResponse = new TaskCompletionSource<bool>();
            _resumeRequest = new TaskCompletionSource<bool>();
            responseTask = _pauseResponse.Task;
        }

        return responseTask;
    }

    public Task WaitAsync()
    {
        Task? resumeTask = null;
        TaskCompletionSource<bool>? response = null;

        lock (_lock)
        {
            if (!_paused)
                return CompletedTask;
            response = _pauseResponse;
            resumeTask = _resumeRequest?.Task!;
        }

        response?.TrySetResult(true);
        return resumeTask;
    }

    public bool IsPaused
    {
        get
        {
            lock (_lock)
                return _paused;
        }
    }

    public PauseToken Token => new PauseToken(this);
}