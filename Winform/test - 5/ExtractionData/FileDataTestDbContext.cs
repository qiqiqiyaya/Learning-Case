using Microsoft.EntityFrameworkCore;

namespace ExtractionData
{
    public class FileDataTestDbContext : DbContext
    {
        public DbSet<FileDataTest> FileDataTest { get; set; }

        public string DbPath { get; }

        public FileDataTestDbContext()
        {
            DbPath = Path.Combine(Application.StartupPath, "EFCore.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
