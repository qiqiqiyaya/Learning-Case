using Microsoft.EntityFrameworkCore;

namespace ExtractData.Repositories
{
    public class FileDataDbContext : DbContext
    {
        public DbSet<FileDataEntity> FileData { get; set; }

        public FileDataDbContext()
        {

        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source=FileData.db");
        }
    }
}
