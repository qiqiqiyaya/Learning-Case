namespace ChainOfResponsibility.Core
{
    public class HandlerMap
    {
        public IHandler Handler { get; set; }

        public Subject Subject { get; set; }

        public HandlerMap(IHandler handler, Subject subject)
        {
            Handler = handler;
            Subject = subject;
        }
    }
}
