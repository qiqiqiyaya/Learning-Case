using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;

namespace ExtractionData
{
    public class ExcelExport
    {
        public static string Export(List<ExcelData> data)
        {
            //创建文件  
            XSSFWorkbook wk = new XSSFWorkbook();

            //创建Excel工作表  
            var sheet = wk.CreateSheet("第一个Sheet");

            for (int i = 0; i < data.Count; i++)
            {
                //创建单元格  
                var row = sheet.CreateRow(i); //选择第1列
                var cell = row.CreateCell(0); //选择第1行
                cell.SetCellValue(data[i].Id); //把0写进这个位置

                var cell2 = row.CreateCell(1); //选择第1行
                cell2.SetCellValue(data[i].Code); //把0写进这个位置

                var cell3 = row.CreateCell(2); //选择第1行
                cell3.SetCellValue(data[i].Md5); //把0写进这个位置
            }

            string fileName = Guid.NewGuid().ToString("N");
            string path = Path.Combine(Application.StartupPath, fileName + ".xls");
            //保存
            FileStream file = new FileStream(path, FileMode.Create); //保存在这个路径，模式是创建
            wk.Write(file);
            file.Close();
            file.Dispose();
            wk.Dispose();

            return path;
        }

    }
}
