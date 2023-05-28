using Microsoft.EntityFrameworkCore;

namespace ExtractionData.Repository
{
    [PrimaryKey(nameof(Id)), Index(nameof(Id), IsUnique = true, AllDescending = false)]
    public class RecordDataEntity
    {
        /// <summary>
        /// 数据记录标记
        /// </summary>
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
