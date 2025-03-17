using Elsa.Http;
using Elsa.Workflows;

namespace ElsaServer.Activities
{
    public class HttpEndPointTest : HttpEndpoint
    {
        protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
        {
            var request = context;
            await base.ExecuteAsync(context);
        }
    }
}
