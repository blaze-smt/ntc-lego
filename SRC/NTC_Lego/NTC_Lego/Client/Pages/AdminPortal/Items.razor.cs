using Microsoft.AspNetCore.Components;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.AdminPortal
{
    public partial class Items
    {
        private IEnumerable<ItemVM>? items;
        private int pageSize = 15;
        private int curPage = 1;
        [Parameter]
        public string searchText { get; set; }
        private Item selectedItem;

        private async Task<IEnumerable<Item>> SearchItem(string searchText)
        {
            var response = await search.SearchItems(searchText);
            return response;
        }

        private void HandleSearch(Item item)
        {
            if (item == null) return;
            selectedItem = item;
            NavigationManager.NavigateTo($"itemView/{item.ItemId}");
        }
        protected override async Task OnInitializedAsync()
        {
            await ShowPage();
        }

        protected async Task NextPage()
        {
            curPage++;
            await ShowPage();
        }

        protected async Task ShowPage(int page)
        {
            curPage = page;
            await ShowPage();
        }

        protected async Task PrevPage()
        {
            if (curPage > 1)
            {
                curPage--;
                await ShowPage();
            }
        }

        protected async Task ShowPage()
        {
            items = await Http.GetFromJsonAsync<List<ItemVM>>($"/Admin?page={curPage}");
            await ShowImage(items); // For item image loading
        }

        protected override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Items");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");

        // *** Methods for getting item images and associated BrickLink URL ***
        // Implement by adding a column to the table and calling the methods in each row
        protected async Task ShowImage(IEnumerable<ItemVM> items)
        {
            foreach (var i in items)
            {
                var path = $"https://img.bricklink.com/ItemImage/SN/0/{i.ItemId}.png";
                i.Image = path;
                path = $"https://www.bricklink.com/v2/catalog/catalogitem.page?{i.ItemTypeId}={i.ItemId}";
                i.BrickLinkURL = path;

                if (i.ItemTypeId != "S")
                {
                    var colors = await Http.GetFromJsonAsync<int[]>($"/Admin/colors?id={i.ItemId}");
                    foreach (var c in colors)
                    {
                        path = $"https://img.bricklink.com/ItemImage/PN/{c}/{i.ItemId}.png";
                        i.Image = path;
                    }
                }
            }
        }
    }
}
