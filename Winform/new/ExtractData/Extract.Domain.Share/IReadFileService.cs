namespace Extract.Domain.Share
{
    public interface IReadFileService
    {
        Task ReadFile(string file, PauseToken token, CancellationToken cancellationToken);
    }
}
