namespace Extract.Domain.Share
{
    public interface IFileDataExportService
    {
        Task<string> ExportExcel(string savePath, PauseToken token, CancellationToken cancellationToken);
    }
}
