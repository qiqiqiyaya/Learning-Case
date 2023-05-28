using Extract.Domain.Share;
using ExtractData.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Extract.Domain
{
    public class DbInitService : IDbInitService
    {
        private readonly FileDataDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public DbInitService(FileDataDbContext dbContext, IMediator mediator,ILogger<DbInitService> logger)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _logger= logger;
        }

        public async Task InitAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(new OutputMessage("数据库初始化"), cancellationToken);
            await _dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("数据库初始化完成.");
            await _mediator.Publish(new OutputMessage("数据库初始化完成"), cancellationToken);
        }

        public async Task ClearDataAsync()
        {
            // 清楚跟踪的实体
            _dbContext.ChangeTracker.Clear();
            await _dbContext.Database.ExecuteSqlAsync(FormattableStringFactory.Create(@"delete from FileData"));
            _logger.LogInformation("删除FileData表数据.");
        }
    }
}
