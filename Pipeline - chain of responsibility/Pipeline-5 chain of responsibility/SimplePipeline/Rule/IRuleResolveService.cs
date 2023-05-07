namespace SimplePipeline.Rule
{
    public interface IRuleResolveService
    {
        List<Rule> GetRules(List<OriginalRule> originalRules);

        Rule GetRule(OriginalRule originalRule);

        T GetRule<T>(OriginalRule originalRule);

        List<Rule> ConvertTree(List<Rule> rules);

        List<Rule> ConvertTree(List<OriginalRule> originalRules);
    }
}
