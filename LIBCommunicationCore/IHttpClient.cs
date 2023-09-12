using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LIBCommunicationCore
{
    public interface IHttpClient : IDisposable
    {
        TimeSpan Timeout { get; set; }
        HttpRequestHeaders DefaultRequestHeaders { get; }

        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}
