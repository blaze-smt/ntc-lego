using Microsoft.AspNetCore.Components;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.AdminPortal
{
    public partial class ItemView
    {
        [Parameter]
        public string? itemId { get; set; }

        private Item? item = null;

        protected override async Task OnInitializedAsync()
        {
            item = await Http.GetFromJsonAsync<Item>($"admin/getItem/{itemId}");
            await ShowImage(item); // For item image loading
        }

        // *** Methods for getting item images and associated BrickLink URL ***
        // Implement by adding a column to the table and calling the methods in each row
        protected async Task ShowImage(Item item)
        {
            Item i = item;
            var path = $"https://img.bricklink.com/ItemImage/SN/0/{i.ItemId}.png";
            i.Image = path;
            path = $"https://www.bricklink.com/v2/catalog/catalogitem.page?{i.ItemTypeId}={i.ItemId}";
            i.BrickLinkURL = path;

            if (i.ItemTypeId != "S")
            {
                var colors = await Http.GetFromJsonAsync<int[]>($"/admin/colors?id={i.ItemId}");
                foreach (var c in colors)
                {
                    path = $"https://img.bricklink.com/ItemImage/PN/{c}/{i.ItemId}.png";
                    i.Image = path;
                }
            }
        }
    }
}
