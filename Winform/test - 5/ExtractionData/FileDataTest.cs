using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtractionData
{
    [PrimaryKey(nameof(Id)), Index(nameof(Id), IsUnique = true,AllDescending = false)]
    public class FileDataTest
    {
        /// <summary>
        /// 数据记录标记
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
