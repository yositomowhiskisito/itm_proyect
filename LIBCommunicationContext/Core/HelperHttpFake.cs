using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LIBCommunicationContext.Core
{
    public class HelperHttpFake : HttpClient
    {
        private string response = EncryptHelper.EncryptKey("{'Response':'OK'}");

        public HelperHttpFake(Dictionary<string, object> data)
        {
            if (data != null && data.ContainsKey("Response"))
                this.response = EncryptHelper.EncryptKey(data["Response"].ToString());
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            var response = new HttpResponseMessage();
            response.Content = new HelperFakeContent(
                requestUri.Contains("/Token/Authenticate") ? true : false, this.response);
            return response;
        }
    }

    internal class HelperFakeContent : HttpContent
    {
        private bool value;
        private string response;

        public HelperFakeContent(bool value, string response)
        {
            this.value = value;
            this.response = response;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context) { return null; }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return true;
        }

        public new Task<string> ReadAsStringAsync()
        {
            if (value)
                return Task.Run(() => { return "{\"Token\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InJiSFhxb3lJNlFNd2dHNWlzTXh5OUJtc2lYVXRneHA0OUpsTXJwR2RzckRDUE0xYUdOYmRNVmpWcDlYSXZ4Z0EiLCJuYmYiOjE2MzkxNjM2OTEsImV4cCI6MTYzOTE2MzgxMSwiaWF0IjoxNjM5MTYzNjkxfQ.qW9nSgXUht_iV9Hir_EFhPfti8TZ6ruukc-cOzuC_cM\",\"Expires\":\"2021-12-10T19:16:51.7378299Z\"}"; });
            else
                return Task.Run(() => { return response; });
        }
    }
}