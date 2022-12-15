using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.AdminPortal
{
    public partial class Purchases
    {
        private IEnumerable<PurchaseOrderVM>? purchaseOrders;
        private IEnumerable<SupplierVM>? suppliers;
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
            purchaseOrders = await Http.GetFromJsonAsync<List<PurchaseOrderVM>>($"/Purchase/purchases?page={curPage}");
            suppliers = await Http.GetFromJsonAsync<List<SupplierVM>>($"/Purchase/suppliers?");
            locations = await Http.GetFromJsonAsync<List<LocationVM>>($"/Inventory/location?");
            ItemVM? item = await Http.GetFromJsonAsync<ItemVM>($"/Inventory/item?itemId={purchaseDetailAdd.ItemId}");
            colors = await Http.GetFromJsonAsync<List<ColorVM>>($"/Admin/itemcolors?itemId={item!.ItemId}&&itemType={item.ItemTypeId}");
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Purchases");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");

        /*Add Modal*/
        private bool status = false;
        private string display = "display: none";

        private void toggleModal()
        {
            status = !status;

            if (status)
            {
                display = "display: block";
            }
            else
            {
                display = "display: none";
            }
        }

        /*  ==================================
         *      Purchase Order Transactions
            ================================== */
        private bool selected = false;
        private bool canCancel = false;
        private bool alreadyCanceled = false;
        private PurchaseOrderVM purchaseOrderCancel = new PurchaseOrderVM();

        private async void HandleSelect(ChangeEventArgs args)
        {
            alreadyCanceled = false;
            canCancel = false;
            var id = Int32.Parse(args.Value.ToString());
            PurchaseOrderVM purchaseOrder = await Http.GetFromJsonAsync<PurchaseOrderVM>($"/Purchase/purchase-id?id={id}");
            if (purchaseOrder != null)
            {
                if (purchaseOrder.OrderStatus == OrderStatus.Canceled)
                {
                    selected = true;
                    alreadyCanceled = true;
                }
                else
                {
                    selected = true;
                    purchaseOrderCancel = purchaseOrder;

                    DateTime orderDate = purchaseOrderCancel.PurchaseOrderDate;
                    orderDate = orderDate.AddDays(30);
                    if (orderDate > DateTime.Now)
                    {
                        canCancel = true;
                    }
                }
                StateHasChanged();
            }
        }

        private async Task HandlePurchaseCancel()
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync($"/Purchase/purchase-cancel?", purchaseOrderCancel.PurchaseOrderId);
            var messageContent = await response.Content.ReadAsStringAsync();
            var messageList = JsonConvert.DeserializeObject<List<string>>(messageContent);
            var message = string.Join("\n", messageList);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StateHasChanged();
                await JsRuntime.InvokeVoidAsync("alert", message);
                await OnInitializedAsync();
            }
            else
            {
                await JsRuntime.InvokeVoidAsync("alert", message);
            }
        }

        /*  ==================================
         *      Purchase Order Transactions
            ================================== */
        private PurchaseOrderAddVM purchaseAdd = new PurchaseOrderAddVM();
        private PurchaseOrderDetailAddVM purchaseDetailAdd = new PurchaseOrderDetailAddVM();
        private List<PurchaseOrderDetailAddVM> purchaseDetailsAdd = new List<PurchaseOrderDetailAddVM>()
    {
         new PurchaseOrderDetailAddVM() { },
    };


        private async Task HandlePurchaseAdd()
        {
            purchaseAdd.PurchaseOrderDetails = purchaseDetailsAdd;

            bool confirmed = true;//await JsRuntime.InvokeAsync<bool>("confirm", "Confirm Purchase Order");
            if (confirmed)
            {
                HttpResponseMessage response = await Http.PostAsJsonAsync($"/Purchase/add-purchase?", purchaseAdd);
                var messageContent = await response.Content.ReadAsStringAsync();
                var messageList = JsonConvert.DeserializeObject<List<string>>(messageContent);
                var message = string.Join("\n", messageList);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await JsRuntime.InvokeVoidAsync("alert", message);
                    NavigationManager.NavigateTo("/purchases");
                }
                else
                {
                    await JsRuntime.InvokeVoidAsync("alert", message);
                }
            }
        }

        private async void HandleDetailCancel(PurchaseOrderDetailAddVM detail)
        {
            bool confirmed = true;//await JsRuntime.InvokeAsync<bool>("confirm", "Confirm Delete");
            if (confirmed)
            {
                purchaseDetailsAdd.Remove(detail);
                StateHasChanged();
            }
        }

        private async Task HandleDetailAdd()
        {
            PurchaseOrderDetailAddVM newDetail = new PurchaseOrderDetailAddVM()
            {
                ItemId = purchaseDetailAdd.ItemId,
                ColorId = purchaseDetailAdd.ColorId,
                LocationId = purchaseDetailAdd.LocationId,
                Quantity = purchaseDetailAdd.Quantity,
                Price = purchaseDetailAdd.Price,
            };

            PurchaseOrderDetailAddVM match = purchaseDetailsAdd.FirstOrDefault(x => x.ItemId == newDetail.ItemId && x.ColorId == newDetail.ColorId && x.LocationId == newDetail.LocationId);
            if (match != null)
            {
                match.Quantity += newDetail.Quantity;
                match.Price = newDetail.Price;
            }
            else
            {
                purchaseDetailsAdd.Add(newDetail);
            }
            StateHasChanged();
        }

        private IEnumerable<ColorVM>? colors;
        private IEnumerable<LocationVM>? locations;
        private string? errorMessage = null;
        private bool hideRest = false;

        private async Task HandleItemChange(string id)
        {
            purchaseDetailAdd.ItemId = id;
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
    }
}
