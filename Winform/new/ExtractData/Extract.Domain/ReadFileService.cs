using System.Data;
using Extract.Domain.Share;
using System.Text;
using System.Text.RegularExpressions;
using ExtractData.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Extract.Domain
{
    public class ReadFileService : IReadFileService
    {
        private readonly FileDataDbContext _dbContext;
        private readonly IFileDataChecker _fileDataChecker;
        private readonly IMediator _mediator;

        public ReadFileService(FileDataDbContext dbContext, IFileDataChecker fileDataChecker, IMediator mediator)
        {
            _dbContext = dbContext;
            _fileDataChecker = fileDataChecker;
            _mediator = mediator;
        }

        public async Task ReadFile(string file, CancellationToken cancellationToken, PauseToken token)
        {
            StreamReader reader = new StreamReader(file, Encoding.UTF8);
            int index = 0;
            int total = 0;
            string? str;
            bool readFirstNum = true;

            await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);
            try
            {
                while (!reader.EndOfStream)
                {
                    if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();

                    str = await reader.ReadLineAsync();
                    await Task.Delay(150, cancellationToken);

                    if (str == null || string.IsNullOrWhiteSpace(str))
                    {
                        var num = index + 1;
                        await OutputMessage($"当前行数 {num} ，无法获取到数据记录，跳过此行", cancellationToken);
                        continue;
                    }

                    // 第一行为 记录数量
                    if (readFirstNum)
                    {
                        readFirstNum = false;
                        var numberStr = Regex.Replace(str, @"\s", "");
                        if (int.TryParse(numberStr, out int count))
                        {
                            total = count;
                            await OutputMessage($"获取记录数量：{total}", cancellationToken);
                            continue;
                        }
                    }

                    var result = _fileDataChecker.ConvertAndCheck(str);
                    if (!result.Successed)
                    {
                        await OutputMessage(result.FailedMessage + " , 行数据：" + str, cancellationToken);
                        continue;
                    }

                    await _dbContext.FileData.AddAsync(result.FileData, cancellationToken);
                    if (index == 100)
                    {
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }
                    await OutputMessage(str, cancellationToken);

                    index++;
                    if (token.IsPaused) await token.WaitAsync();
                }

                if (index != total)
                {
                    await OutputMessage($"记录数量：{total} , 但实际上获取了的记录数量：{index}", cancellationToken);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                // 清楚跟踪的实体
                _dbContext.ChangeTracker.Clear();
                // 不用取消回滚
                await _dbContext.Database.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }

        private async Task OutputMessage(string msg, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new OutputMessage(msg), cancellationToken);
        }
    }
}
