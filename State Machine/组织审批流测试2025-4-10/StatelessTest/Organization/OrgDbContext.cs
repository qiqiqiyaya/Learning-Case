using Microsoft.EntityFrameworkCore;
using StatelessTest.Organization.Entities;

namespace StatelessTest.Organization
{
    public class OrgDbContext : DbContext
    {
        public OrgDbContext(DbContextOptions<OrgDbContext> options)
            : base(options)
        {

        }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<ManagerEntity> Managers { get; set; }

        public DbSet<OrganizationEntity> Organizations { get; set; }

        public DbSet<PeEntity> Pes { get; set; }

        public DbSet<WorkflowEntity> Workflows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<ManagerEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<EmployeeEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<PeEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<WorkflowEntity>().HasKey(s => s.Id);

            modelBuilder.Entity<OrganizationEntity>()
                .HasMany<ManagerEntity>(s => s.Managers)
                .WithOne(s => s.Organization)
                .HasForeignKey(s => s.OrganizationId)
                .IsRequired();
        }
    }
}
