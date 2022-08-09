namespace Producer
{
    public class Message
    {
        public string ApplicationName { get; set; }

        public string ContentRootPath { get; set; }

        public string EnvironmentName { get; set; }

        public DateTime DateTime { get; set; }

        public Message(string applicationName, string contentRootPath, string environmentName)
        {
            ApplicationName = applicationName;
            ContentRootPath = contentRootPath;
            EnvironmentName = environmentName;
            DateTime = DateTime.Now;
        }
    }
}
