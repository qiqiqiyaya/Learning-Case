using Extract.Domain;
using Extract.Domain.Share;
using ExtractData.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ExtractData
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("应用程序启动.");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddSingleton(Log.Logger);
            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });
            services.AddScoped<Main>();
            services.AddTransient<IReadFileService, ReadFileService>();
            services.AddTransient<IFileDataChecker, FileDataChecker>();
            services.AddTransient<IDbInitService, DbInitService>();
            services.AddTransient<IFileDataExportService, FileDataExportService>();
            services.AddDbContext<FileDataDbContext>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
                .RegisterServicesFromAssembly(typeof(ReadFileService).Assembly)
                .RegisterServicesFromAssembly(typeof(PauseToken).Assembly)
                .RegisterServicesFromAssembly(typeof(FileDataDbContext).Assembly));

            RootContainer.SetRoot(services);
            try
            {
                using (ServiceProvider serviceProvider = services.BuildServiceProvider())
                {
                    var main = serviceProvider.GetRequiredService<Main>();
                    Application.Run(main);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "系统性错误");
            }
            finally
            {
                Log.Information("应用程序关闭");
                Log.CloseAndFlush();
            }
        }
    }
}