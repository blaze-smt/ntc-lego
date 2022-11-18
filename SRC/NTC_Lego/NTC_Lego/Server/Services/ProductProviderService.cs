using NTC_Lego.Client.Pages.AdminPortal;
using System.Collections.Immutable;

namespace NTC_Lego.Shared
{
    public class ProductProviderService
    {
        public static readonly ImmutableList<Item> Items = ImmutableList.CreateRange(new List<Item>()
        {
            new()
        {
            ItemId ="200",
            ItemName = "Hoodie"
        },

            new()
        {
            ItemId = "201",
            ItemName = "Sweatpants"
        }
        });
    }
}
