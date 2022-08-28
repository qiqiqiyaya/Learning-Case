using System.Net;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Customize_Server
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _listener = new();

        public IFeatureCollection Features { get; } = new FeatureCollection();

        public void Dispose()
        {
            _listener.Stop();
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) where TContext : notnull
        {
            var pathBases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addressesFeatures = Features.Get<IServerAddressesFeature>();

            foreach (var address in addressesFeatures.Addresses)
            {
                _listener.Prefixes.Add(address.TrimEnd('/') + "/");
                pathBases.Add(new Uri(address).AbsolutePath.TrimEnd('/'));
            }

            _listener.Start();

            while (true)
            {
                var listenerContext = _listener.GetContext();
                _ = ProcessRequestAsync(listenerContext);
            }

            async Task ProcessRequestAsync(HttpListenerContext listenerContext)
            {
                FeatureCollection features = new();
                var requestFeature = CreateRequestFeature(pathBases, listenerContext);

                var responseFeature = new HttpResponseFeature();
                var body = new MemoryStream();
                var bodyFeature = new StreamBodyFeature(body);
                features.Set<IHttpRequestFeature>(requestFeature);
                features.Set<IHttpResponseFeature>(responseFeature);
                features.Set<IHttpResponseBodyFeature>(bodyFeature);

                var context = application.CreateContext(features);
                Exception? exception = null;
                try
                {
                    await application.ProcessRequestAsync(context);

                    var response = listenerContext.Response;
                    response.StatusCode = responseFeature.StatusCode;
                    if (responseFeature.ReasonPhrase is not null)
                    {
                        response.StatusDescription = responseFeature.ReasonPhrase;
                    }

                    foreach (var kv in responseFeature.Headers)
                    {
                        response.AddHeader(kv.Key, kv.Value);
                    }

                    body.Position = 0;

                    await body.CopyToAsync(listenerContext.Response.OutputStream);
                }
                catch (Exception e)
                {
                    exception = e;
                }
                finally
                {
                    body.Dispose();
                    application.DisposeContext(context, exception);
                    listenerContext.Response.Close();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _listener.Stop();
            return Task.CompletedTask;
        }

        private static HttpRequestFeature CreateRequestFeature(HashSet<string> pathBases, HttpListenerContext listenerContext)
        {
            var request = listenerContext.Request;
            var url = request.Url!;
            var absolutePath = url.AbsolutePath;
            var protocolVersion = request.ProtocolVersion;
            var requestHeaders = new HeaderDictionary();
            foreach (string key in request.Headers)
            {
                requestHeaders.Add(key, request.Headers.GetValues(key));
            }

            var requestFeature = new HttpRequestFeature()
            {
                Body = request.InputStream,
                Headers = requestHeaders,
                Method = request.HttpMethod,
                Scheme = url.Scheme,
                Protocol = $"{url.Scheme.ToUpper()}/{protocolVersion.Major}.{protocolVersion.Minor}"
            };

            var pathBase = pathBases.First(x => absolutePath.StartsWith(x, StringComparison.OrdinalIgnoreCase));
            requestFeature.Path = absolutePath[pathBase.Length..];
            requestFeature.PathBase = pathBase;
            return requestFeature;
        }
    }
}
