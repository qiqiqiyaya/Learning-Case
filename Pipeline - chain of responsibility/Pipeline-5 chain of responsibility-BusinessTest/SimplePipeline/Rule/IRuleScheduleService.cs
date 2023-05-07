namespace SimplePipeline.Rule
{
    public interface IRuleScheduleService
    {
        Task CalculateAsync(List<Rule> rules, RuleCalculationContext context);

        Task CalculateAsync(RuleCalculationContext context);
    }
}
