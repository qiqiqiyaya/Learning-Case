using Grpc.Core;
using Proto.App;

namespace gRPC
{
    public class CalculatorService : Calculator.CalculatorBase
    {
        private readonly ILogger<CalculatorService> _logger;

        public CalculatorService(ILogger<CalculatorService> logger)
        {
            _logger = logger;
        }

        public override Task<OutputMessage> Add(InputMessage request, ServerCallContext context)
        {
            return InvokeAsync((x, y) => x + y, request);
        }

        public override Task<OutputMessage> Substract(InputMessage request, ServerCallContext context)
        {
            return InvokeAsync((x, y) => x - y, request);
        }

        public override Task<OutputMessage> Divide(InputMessage request, ServerCallContext context)
        {
            return InvokeAsync((x, y) => x * y, request);
        }

        public override Task<OutputMessage> Mutiply(InputMessage request, ServerCallContext context)
        {
            return InvokeAsync((x, y) => x / y, request);
        }


        private Task<OutputMessage> InvokeAsync(Func<int, int, int> calculate, InputMessage input)
        {
            OutputMessage output;
            try
            {
                output = new OutputMessage()
                {
                    Status = 0,
                    Result = calculate(input.X, input.Y)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Calculation error.");
                output = new OutputMessage()
                {
                    Status = 1,
                    Error = ex.ToString()
                };

            }

            return Task.FromResult(output);
        }
    }
}
