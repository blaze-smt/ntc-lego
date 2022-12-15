using Microsoft.AspNetCore.Components;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.AdminPortal
{
    public partial class Inventory
    {
        private IEnumerable<ColorVM>? colors;
        private IEnumerable<LocationVM>? locations;
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
            inventories = await Http.GetFromJsonAsync<List<InventoryVM>>($"/Inventory/inventory?page={curPage}");
            await ShowImage(inventories);
            locations = await Http.GetFromJsonAsync<List<LocationVM>>($"/Inventory/location?");
            ItemVM? item = await Http.GetFromJsonAsync<ItemVM>($"/Inventory/item?itemId={inventoryAdd.ItemId}");
            colors = await Http.GetFromJsonAsync<List<ColorVM>>($"/Admin/itemcolors?itemId={item!.ItemId}&&itemType={item.ItemTypeId}");
        }

        protected override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Inventory");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");

        // Set Inventory item image
        protected async Task ShowImage(IEnumerable<InventoryVM> inventories)
        {
            foreach (var i in inventories)
            {
                var path = $"https://img.bricklink.com/ItemImage/SN/0/{i.ItemId}.png";
                i.Image = path;

                if (i.Item.ItemTypeId != "S")
                {
                    path = $"https://img.bricklink.com/ItemImage/PN/{i.ColorId}/{i.ItemId}.png";
                    i.Image = path;
                }
            }
        }

        // Inventory Add transaction
        private string? errorMessage = null;
        private InventoryAddVM inventoryAdd = new InventoryAddVM();
        private bool hideRest = false;
        private bool selected = false;

        private async Task HandleInventoryAdd()
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync($"/Inventory/add-inventory?", inventoryAdd);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StateHasChanged();
                await OnInitializedAsync();
            }
        }

        private async Task HandleItemChange(string id)
        {
            inventoryAdd.ItemId = id;
            try
            {
                ItemVM? item = await Http.GetFromJsonAsync<ItemVM>($"/Inventory/item?itemId={id}");
                if (item != null)
                {
                    colors = await Http.GetFromJsonAsync<List<ColorVM>>($"/Admin/itemcolors?itemId={item.ItemId}&&itemType={item.ItemTypeId}");
                    this.errorMessage = null;
                    this.hideRest = false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                colors = null;
                this.errorMessage = "Invalid item id. Please try again!!";
                this.hideRest = true;
            }
            StateHasChanged();
        }

        // Inventory Edit transaction
        private InventoryVM inventoryEdit = new InventoryVM();
        private async void HandleSelect(ChangeEventArgs args)
        {
            var InventoryId = Int32.Parse(args.Value.ToString());
            InventoryVM inventory = await Http.GetFromJsonAsync<InventoryVM>($"/Inventory/inventory-id?inventoryId={InventoryId}");
            if (inventory != null)
            {
                selected = true;
                inventoryEdit = inventory;
                StateHasChanged();
            }
        }
        private async Task HandleInventoryEdit()
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync($"/Inventory/edit-inventory?", inventoryEdit);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StateHasChanged();
                await OnInitializedAsync();
            }
        }
    }
}
