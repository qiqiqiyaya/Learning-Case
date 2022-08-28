using Microsoft.AspNetCore.Http.Features;
using System.IO.Pipelines;

namespace Customize_Server
{
    public class StreamBodyFeature : IHttpResponseBodyFeature
    {
        public Stream Stream { get; }

        public PipeWriter Writer { get; }

        public StreamBodyFeature(Stream stream)
        {
            Stream = stream;
            Writer = PipeWriter.Create(Stream);
        }

        public void DisableBuffering()
        {

        }

        public Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task SendFileAsync(string path, long offset, long? count,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CompleteAsync()
        {
            return Task.CompletedTask;
        }
    }
}
