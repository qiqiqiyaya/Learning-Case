namespace Extract.Domain.Share
{
    public interface IDbInitService
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InitAsync(CancellationToken cancellationToken);

        Task ClearDataAsync();
    }
}
