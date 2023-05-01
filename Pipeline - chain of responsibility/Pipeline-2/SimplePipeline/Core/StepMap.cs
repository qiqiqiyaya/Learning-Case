namespace SimplePipeline.Core
{
    public class StepMap
    {
        public IStep Step { get; set; }

        public SubjectData SubjectData { get; set; }

        public StepMap(IStep step, SubjectData subjectData)
        {
            Step = step;
            SubjectData = subjectData;
        }
    }
}
