using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.AdminPortal
{
    public partial class AdminDashboard
    {
        private IEnumerable<InventoryVM>? inventories;
        private IEnumerable<SaleOrderVM>? saleOrders;
        private IEnumerable<PurchaseOrderVM>? purchaseOrders;
        private decimal allSaleOrders;
        private decimal allPurchaseOrders;

        protected override async Task OnInitializedAsync()
        {
            await LoadPage();
        }

        protected async Task LoadPage()
        {
            inventories = await Http.GetFromJsonAsync<List<InventoryVM>>($"/Inventory/inventory-recent?");
            saleOrders = await Http.GetFromJsonAsync<List<SaleOrderVM>>($"/Admin/sales-recent?");
            purchaseOrders = await Http.GetFromJsonAsync<List<PurchaseOrderVM>>($"/Purchase/purchases-recent?");
        }

        protected async Task LoadAllPages()
        {
            allSaleOrders = await Http.GetFromJsonAsync<decimal>($"/Admin/allsales?");
            allPurchaseOrders = await Http.GetFromJsonAsync<decimal>($"/Admin/allpurchases?");
        }

        protected async override void OnInitialized()
        {
            await LoadAllPages();
            currentPage.SetCurrentPageName("Dashboard");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");
    }
}
