using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace JobSearch.App.Services
{
    public class Service
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://xamarinforms2020api.azurewebsites.net/";

        protected Service()
        {
            _client = new HttpClient();
        }
    }
}
