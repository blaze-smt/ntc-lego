using NTC_Lego.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace NTC_Lego.Client
{
    public class Search
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public List<Item> Items { get; set; } = new List<Item>();

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
