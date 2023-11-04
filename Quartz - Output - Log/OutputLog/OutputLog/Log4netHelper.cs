namespace OutputLog
{
    public class Log4netHelper
    {
        public static void Register(ApplicationType process)
        {
            var binPath = "";
            switch (process)
            {
                case ApplicationType.AspNet:
                    binPath = AspNetBinPath();
                    break;
                case ApplicationType.UnitTest:
                    binPath = UnitTestBinPath();
                    break;
            }

            FileInfo info = new FileInfo(binPath + "\\" + "Log4Net.config");
            if (!info.Exists)
            {
                throw new Exception("No Log4Net.config file.");
            }


            log4net.Config.XmlConfigurator.Configure(info);
        }

        private static string AspNetBinPath()
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }

        private static string UnitTestBinPath()
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }
    }

    public enum ApplicationType
    {
        UnitTest,
        AspNet
    }
}