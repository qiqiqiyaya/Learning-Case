using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatelessTest.Organization.Entities;
using StatelessTest.Organization.Workflows;

namespace StatelessTest.Organization.Services
{
    public class PeService : IPeService
    {
        private readonly PeWorkflow _peWorkflow;
        private readonly OrgDbContext _dbContext;

        public PeService(PeWorkflow peWorkflow,
            OrgDbContext dbContext)
        {
            _peWorkflow = peWorkflow;
            _dbContext = dbContext;
        }

        public async ValueTask<PeEntity> CreateAsync()
        {
            var pe = new PeEntity();
            var flow = await _peWorkflow.CreateAsync();
            pe.WorkflowId = flow.Id;

            return pe;
        }


    }
}
