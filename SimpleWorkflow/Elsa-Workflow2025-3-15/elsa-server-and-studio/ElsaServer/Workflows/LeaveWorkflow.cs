using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Models;
using System.Dynamic;
using Elsa.Expressions.Models;
using Elsa.Extensions;

namespace ElsaServer.Workflows
{
    public class LeaveWorkflow : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            var code = builder.WithVariable<string>();
            var userVariable = builder.WithVariable<ExpandoObject>();
            var request = builder.WithOutput<HttpRequest>("request");
            MemoryBlockReference content = new MemoryBlockReference();

            builder.Root = new Sequence()
            {
                Activities = new List<IActivity>()
                {
                    new HttpEndpoint()
                    {
                        Path = new("leavatest"),
                        SupportedMethods = new Input<ICollection<string>>(new List<string>(){HttpMethods.Post}),
                        CanStartWorkflow = true,
                        ParsedContent = new Output<object?>(content),
                        Result =new Output<HttpRequest>()
                    },
                    new SetVariable
                    {
                        Variable = code,
                        Value = new Input<object?>(context =>
                        {
                            var dsfdsf = code.Get(context);
                            var cc = request;
                            var aaaa = content.Get(context);
                            var aa = context;
                            //var routeData = routeDataVariable.Get(context)!;
                            //var userId = routeData["userid"].ToString();
                            return true;
                        })
                    },
                    new If(context =>
                    {
                        if (context.TransientProperties.TryGetValue("IsManager", out bool te))
                        {
                            return te;
                        }

                        return false;
                    })
                }
            };
        }
    }
}
