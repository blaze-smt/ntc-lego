using NTC_Lego.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace NTC_Lego.Client
{
    /// <summary>
    /// Class used to search the database - searchbar functionality
    /// </summary>
    public class Search
    {
        private readonly HttpClient _http;

        public Search(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Item>> SearchItems(string searchText)
        {
            return await _http.GetFromJsonAsync<List<Item>>($"admin/Search/{searchText}");
        }
    }
}
