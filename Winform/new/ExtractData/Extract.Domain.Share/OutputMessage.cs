using MediatR;

namespace Extract.Domain.Share
{
    public class OutputMessage : INotification
    {
        public string Text { get; set; }

        public OutputMessage(string text) 
        { 
            Text = text;
        }
    }
}
