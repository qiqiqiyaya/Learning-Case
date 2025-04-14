using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stateless.Graph;
using Stateless;
using StatelessTest.Organization;
using StatelessTest.Organization.Services;
using StatelessTest.Organization.Workflows;

namespace StatelessTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            service.AddDbContext<OrgDbContext>(options =>
            {
                options.UseInMemoryDatabase("test");
                options.UseSeeding((dbContext, a) =>
                {
                    if (dbContext is OrgDbContext db)
                    {
                        SeedData.Initialize(db);
                    }
                });
            });

            service.AddTransient<IPeService, PeService>();
            service.AddTransient<IWorkflow, PeWorkflow>();

            using (var scope = service.BuildServiceProvider())
            {

            }
        }
    }
}
