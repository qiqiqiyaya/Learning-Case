namespace Extract.Domain.Share
{
    public interface IReadFileService
    {
        Task ReadFile(string file, CancellationToken cancellationToken, PauseToken token);
    }
}
