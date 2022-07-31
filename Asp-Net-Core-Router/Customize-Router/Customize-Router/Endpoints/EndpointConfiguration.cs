using System.Reflection;

namespace Customize_Router.Endpoints
{
    public class EndpointConfiguration
    {
        private readonly List<KeyValuePair<string, Type>> _endpoints;

        public EndpointConfiguration()
        {
            _endpoints = new List<KeyValuePair<string, Type>>();
        }

        public EndpointConfiguration AddEndpoint(string path, Type info)
        {
            _endpoints.Add(new KeyValuePair<string, Type>(path, info));
            return this;
        }

        public Type GetEndpointType(string path)
        {
            var point = _endpoints.Find(x => x.Key == path);
            return point.Value;
        }

        public List<KeyValuePair<string, Type>> Endpoints => _endpoints;

        public const string ApiGet = "/ApiGet";
        public const string ApiSet = "/ApiSet";
        public const string ApiGetList = "/ApiGetList";
    }
}
