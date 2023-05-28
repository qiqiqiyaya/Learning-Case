using ExtractData.Repositories;

namespace Extract.Domain.Share
{
    public class ConvertResult
    {
        public bool Successed { get; set; }

        public string FailedMessage { get; set; }

        public FileDataEntity FileData { get; set; }

        public static ConvertResult Success(FileDataEntity data)
        {
            return new ConvertResult() { Successed = true, FileData = data };
        }

        public static ConvertResult Failed(string msg)
        {
            return new ConvertResult() { Successed = false, FailedMessage = msg };
        }
    }
}
