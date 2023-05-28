using Extract.Domain.Share;
using ExtractData.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Extract.Domain
{
    public class DbInitService : IDbInitService
    {
        private readonly FileDataDbContext _dbContext;
        private readonly IMediator _mediator;

        public DbInitService(FileDataDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task InitAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(new OutputMessage("数据库初始化"), cancellationToken);
            await _dbContext.Database.MigrateAsync(cancellationToken);
            await _mediator.Publish(new OutputMessage("数据库初始化完成"), cancellationToken);
        }

        public async Task ClearDataAsync()
        {
            // 清楚跟踪的实体
            _dbContext.ChangeTracker.Clear();
            await _dbContext.Database.ExecuteSqlAsync(FormattableStringFactory.Create(@"delete from FileData"));
        }
    }
}
