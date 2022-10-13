using NTC_Lego.Shared;

namespace NTC_Lego.Client.Services
{
    public interface IItemService
    {
        Task<Item> GetItem(string itemId);
    }
}
