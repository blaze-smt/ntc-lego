using NTC_Lego.Client.Pages.AdminPortal;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.ItemsCatalog
{
    public partial class ItemsCatalog
    {
        private IEnumerable<InventoryVM>? inventories;
        private int pageSize = 15;
        private int curPage = 1;

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
            inventories = await Http.GetFromJsonAsync<List<InventoryVM>>($"/Inventory/inventory2?page={curPage}");
            await ShowImage(inventories);
        }

        protected override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Catalog");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");

        protected async Task ShowImage(IEnumerable<InventoryVM> inventories)
        {
        //Loops through the items in the Inventory and gets the images
            foreach (var i in inventories)
            {
                var path = $"https://img.bricklink.com/ItemImage/SN/0/{i.ItemId}.png";
                i.Image = path;
                path = $"https://www.bricklink.com/v2/catalog/catalogitem.page?{i.Item.ItemTypeId}={i.ItemId}";
                i.BrickLinkURL = path;

                if (i.Item.ItemTypeId != "S")
                {
                    path = $"https://img.bricklink.com/ItemImage/PN/{i.ColorId}/{i.ItemId}.png";
                    i.Image = path;
                }
            }
        }
    }
}
