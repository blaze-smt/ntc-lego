using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Services
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;

        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Item> GetItem(string itemId)
        {
            return await _httpClient.GetFromJsonAsync<Item>($"item/{itemId}");
        }
    }
}
