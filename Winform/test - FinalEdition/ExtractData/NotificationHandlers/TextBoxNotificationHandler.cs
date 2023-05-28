using Extract.Domain.Share;
using MediatR;

namespace ExtractData.NotificationHandlers
{
    public class TextBoxNotificationHandler : INotificationHandler<OutputMessage>
    {
        private readonly Main _mainForm;

        public TextBoxNotificationHandler(Main mainForm)
        {
            _mainForm = mainForm;
        }

        public Task Handle(OutputMessage notification, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return Task.CompletedTask;

            _mainForm.SetPropertyThreadSafe(@this =>
            {
                @this.Txt_OutputMsg.AppendText(notification.Text + "\r\n");
            });
            return Task.CompletedTask;
        }
    }
}
