namespace AspNetCoreAsWindowsServiceTesting
{
    public class HostSettingConfiguration
    {
        public int Port { get; set; }

        public string Host { get; set; }

        public string Url => Host + ":" + Port;
    }
}
