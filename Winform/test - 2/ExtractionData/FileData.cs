using SQLite;

namespace ExtractionData
{
    public class FileData
    {
        /// <summary>
        /// 数据记录标记
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }

    }
}
