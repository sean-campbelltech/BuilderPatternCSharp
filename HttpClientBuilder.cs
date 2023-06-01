using System.Text;

namespace BuilderPatternCSharp
{
    public class HttpClientBuilder
    {
        private readonly HttpClient _client = new HttpClient();

        public HttpClientBuilder AddBasicAuth(string username, string password)
        {
            checkRemoveAuthHeader();
            byte[] encodedCreds = Encoding.ASCII.GetBytes($"{username}:{password}");
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {encodedCreds}");
            return this;
        }

        public HttpClientBuilder AddJwt(string jwt)
        {
            checkRemoveAuthHeader();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            return this;
        }

        public HttpClientBuilder AddCustomHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        public HttpClientBuilder SetTimeout(int seconds)
        {
            _client.Timeout = TimeSpan.FromSeconds(seconds);
            return this;
        }

        public HttpClientBuilder SetBaseAddress(Uri baseAddress)
        {
            _client.BaseAddress = baseAddress;
            return this;
        }

        public HttpClientBuilder SetMaxResponseContentBufferSize(long maxSize)
        {
            _client.MaxResponseContentBufferSize = maxSize;
            return this;
        }

        public HttpClientBuilder SetVersionPolicy(HttpVersionPolicy versionPolicy)
        {
            _client.DefaultVersionPolicy = versionPolicy;
            return this;
        }

        public HttpClientBuilder SetRequestVersion(Version version)
        {
            _client.DefaultRequestVersion = version;
            return this;
        }

        private void checkRemoveAuthHeader()
        {
            if (_client.DefaultRequestHeaders.Contains("Authorization"))
            {
                _client.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        public HttpClient Build()
        {
            return _client;
        }
    }
}