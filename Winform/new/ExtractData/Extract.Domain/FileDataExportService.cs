using Extract.Domain.Share;
using ExtractData.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.XSSF.UserModel;

namespace Extract.Domain
{
    public class FileDataExportService : IFileDataExportService
    {
        private readonly FileDataDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FileDataExportService(FileDataDbContext dbContext, IMediator mediator, ILogger<DbInitService> logger)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<string> ExportExcel(string savePath, PauseToken token, CancellationToken cancellationToken)
        {
            var list = await _dbContext.FileData.ToListAsync(cancellationToken);
            var data = new List<ExcelExportData>();
            await _mediator.Publish(new OutputMessage("准备数据导出"), cancellationToken);
            foreach (var row in list)
            {
                if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
                if (token.IsPaused) await token.WaitAsync();

                var ed = new ExcelExportData();
                ed.Id = row.Id;

                ed.Code = Encrypt.DesEncrypt(row.Data, "ACF8C7ADC7C0D54F71809B5441EF8DD0");
                ed.Md5 = Encrypt.GetMd5FromString(row.Data);

                data.Add(ed);
            }

            XSSFWorkbook wk = new XSSFWorkbook();
            var sheet = wk.CreateSheet("第一个Sheet");

            for (int i = 0; i < data.Count; i++)
            {
                if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
                if (token.IsPaused) await token.WaitAsync();
                var row = sheet.CreateRow(i); //选择第1列
                var cell = row.CreateCell(0); //选择第1行
                cell.SetCellValue(data[i].Id); //把0写进这个位置

                var cell2 = row.CreateCell(1);
                cell2.SetCellValue(data[i].Code);

                var cell3 = row.CreateCell(2);
                cell3.SetCellValue(data[i].Md5);
            }

            string fileName = Guid.NewGuid().ToString("N");
            string path = Path.Combine(savePath, fileName + ".xls");
            //保存
            FileStream file = new FileStream(path, FileMode.Create);
            wk.Write(file);
            file.Close();
            file.Dispose();
            wk.Dispose();

            _logger.LogInformation("Excel导出完成，路径：" + path);

            return path;
        }
    }
}
