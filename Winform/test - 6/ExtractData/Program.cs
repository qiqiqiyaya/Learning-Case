using Extract.Domain;
using Extract.Domain.Share;
using ExtractData.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
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
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var main = serviceProvider.GetRequiredService<Main>();
                Application.Run(main);
            }
        }
    }
}