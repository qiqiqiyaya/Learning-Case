using ChainOfResponsibility.Core;

namespace ChainOfResponsibility
{
    public class SubjectHandler : IHandler
    {
        private IHandler? _next;
        private readonly Subject _subject;

        public SubjectHandler(Subject subject)
        {
            _subject = subject;
        }

        public void SetNext(IHandler? next)
        {
            _next = next;
        }

        public async Task ExecuteAsync(PipelineContext context)
        {
            Console.WriteLine(_subject.Name);

            if (_next != null)
            {
                await _next.ExecuteAsync(context);
            }
        }
    }
}
