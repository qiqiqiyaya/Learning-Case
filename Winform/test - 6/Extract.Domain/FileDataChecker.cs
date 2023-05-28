using Extract.Domain.Share;
using ExtractData.Repositories;
using System.Text.RegularExpressions;

namespace Extract.Domain
{
    public class FileDataChecker : IFileDataChecker
    {
        public ConvertResult ConvertAndCheck(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            var array = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length >= 2)
            {
                //记录标记检查
                if (array[0].Length > 2) return ConvertResult.Failed("记录标记长度超出 2 ");
                if (!Is16Base(array[0])) return ConvertResult.Failed("记录标记不是 16 进制 ");

                if (array[1].Length > 1) return ConvertResult.Failed("记录长度超出 1 ");
                if (!Is16Base(array[1])) return ConvertResult.Failed("记录长度不是 16 进制 ");

                var length = Convert.ToInt32(array[1], 16);
                if (array[2].Length != length) return ConvertResult.Failed($"记录数据长度错误 {length} ，实践数据长度 {array[2].Length} ，跳过此行");
                if (!Is16Base(array[2])) return ConvertResult.Failed("记录数据不是 16 进制 ");

                return ConvertResult.Success(new FileDataEntity()
                {
                    Id = Convert.ToInt32(array[0], 16),
                    Length = Convert.ToInt32(array[1], 16),
                    Data = array[2]
                });
            }

            return ConvertResult.Failed($"数据记录格式出错，数据：{str} ，跳过此行");
        }

        /// <summary>
        /// 是否是16进制
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static bool Is16Base(string hex)
        {
            return Regex.IsMatch(hex, @"[A-Fa-f0-9]+$");
        }
    }
}
