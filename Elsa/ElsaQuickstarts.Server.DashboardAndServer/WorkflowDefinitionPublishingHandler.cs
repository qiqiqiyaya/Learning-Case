using Elsa.Events;
using MediatR;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    public class WorkflowDefinitionPublishingHandler: INotificationHandler<WorkflowDefinitionPublishing>
    {
        public Task Handle(WorkflowDefinitionPublishing notification, CancellationToken cancellationToken)
        {


            return Task.CompletedTask;
        }
    }
}
