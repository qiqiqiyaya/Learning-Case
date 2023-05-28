namespace ExtractionData
{
    public class ConvertResult
    {
        public bool Successed { get; set; }

        public string FailedMessage { get; set; }

        public FileDataTest FileData { get; set; }

        public static ConvertResult Success(FileDataTest data)
        {
            return new ConvertResult() { Successed = true, FileData = data };
        }

        public static ConvertResult Failed(string msg)
        {
            return new ConvertResult() { Successed = false, FailedMessage = msg };
        }
    }
}
