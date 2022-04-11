using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EzHttpClient.Demo
{
    public class EzHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly MediaTypeHeaderValue DEFAULT_MEDIATYPE = new MediaTypeHeaderValue("application/json");
        private static readonly string[] DEFAULT_ACCEPT =
        {
            "application/json",
            "text/plain",
            "*/*"
        };
        private static readonly string[] DEFAULT_ACCEPT_ENCODING = { "gzip", "deflate" };
        private string _url;
        private HttpContent _httpContent;
        private NameValueCollection _headers;
        private MediaTypeHeaderValue _mediaType;
        private string[] _accept;
        private AuthenticationHeaderValue _authenticationHeaderValue;
        
        
        public EzHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 启用gizp压缩
        /// 自动使用gzip压缩body并设置Content-Encoding为gzip
        /// </summary>
        public bool EnableCompress { get; set; }

        public static void Ajax()
        {
            
        }

        protected EzHttpClient Authentication(AuthenticationHeaderValue authentication)
        {
            _authenticationHeaderValue = authentication;
            return this;
        }

        public EzHttpClient Authentication(string scheme, string parameter)
        {
            _authenticationHeaderValue = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        /// <summary>
        /// 默认为 application/json, text/plain, */*
        /// </summary>
        /// <param name="accept"></param>
        /// <returns></returns>
        public EzHttpClient Accept(string[] accept)
        {
            _accept = accept;
            return this;
        }

        public EzHttpClient ContentType(string mediaType, string charSet = null)
        {
            _mediaType = new MediaTypeHeaderValue(mediaType);
            if (!string.IsNullOrEmpty(charSet))
            {
                _mediaType.CharSet = charSet;
            }

            return this;
        }

        public EzHttpClient Url(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            _url = url;
            return this;
        }

        public EzHttpClient Body(object body)
        {
            return Body(Newtonsoft.Json.JsonConvert.SerializeObject(body));
        }

        public EzHttpClient Body(string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                return this;
            }

            var sc = new StringContent(body);
            if (EnableCompress)
            {
                _httpContent = new CompressedContent(sc, CompressedContent.CompressionMethod.GZip);
                sc.Headers.ContentEncoding.Add("gzip");
            }
            else
            {
                _httpContent = sc;
            }

            //sc.Headers.ContentLength = sc..Length;
            sc.Headers.ContentType = _mediaType;

            return this;
        }

        /// <summary>
        /// 仅支持POST和PUT
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public EzHttpClient File(string path, string name, string filename)
        {
            if (_httpContent == null)
            {
                _httpContent = new MultipartFormDataContent();
            }

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            var bac = new StreamContent(stream);
            ((MultipartFormDataContent)_httpContent).Add(bac, name, filename);

            return this;
        }

        /// <summary>
        /// 仅支持POST和PUT
        /// </summary>
        /// <param name="content"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public EzHttpClient File(string content, string name)
        {
            if (_httpContent == null)
            {
                _httpContent = new MultipartFormDataContent();
            }

            ((MultipartFormDataContent)_httpContent).Add(new StringContent(content), name);
            return this;
        }

        public EzHttpClient Form(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (_httpContent == null)
            {
                _httpContent = new FormUrlEncodedContent(nameValueCollection);
            }

            return this;
        }

        public EzHttpClient Headers(NameValueCollection headers)
        {
            CheckHeaderIsNull();

            foreach (string key in headers.Keys)
            {
                _headers.Add(key, headers.Get(key));
            }

            return this;
        }

        private void CheckHeaderIsNull()
        {
            if (_headers == null)
            {
                _headers = new NameValueCollection();
            }
        }

        public EzHttpClient Header(string key, string value)
        {
            CheckHeaderIsNull();
            _headers.Add(key, value);
            return this;
        }
    }
}
