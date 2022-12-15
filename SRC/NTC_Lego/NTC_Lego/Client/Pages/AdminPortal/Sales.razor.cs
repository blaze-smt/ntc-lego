using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.AdminPortal
{
    public partial class Sales
    {
        private IEnumerable<SaleOrderVM>? saleOrders;
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
            saleOrders = await Http.GetFromJsonAsync<List<SaleOrderVM>>($"/Admin/sales?page={curPage}");
        }

        protected override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Sales");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");

        private bool selected = false;
        private bool canCancel = false;
        private bool alreadyCanceled = false;
        private SaleOrderVM saleOrderCancel = new SaleOrderVM();

        private async void HandleSelect(ChangeEventArgs args)
        {
            alreadyCanceled = false;
            canCancel = false;
            var id = Int32.Parse(args.Value.ToString());
            SaleOrderVM saleOrder = await Http.GetFromJsonAsync<SaleOrderVM>($"/Sale/sale-id?id={id}");
            if (saleOrder != null)
            {
                if (saleOrder.OrderStatus == OrderStatus.Canceled)
                {
                    selected = true;
                    alreadyCanceled = true;
                }
                else
                {
                    selected = true;
                    saleOrderCancel = saleOrder;

                    DateTime orderDate = saleOrderCancel.SaleOrderDate;
                    orderDate = orderDate.AddDays(30);
                    if (orderDate > DateTime.Now)
                    {
                        canCancel = true;
                    }
                }
                StateHasChanged();
            }
        }

        private async Task HandleSaleCancel()
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync($"/Sale/sale-cancel?", saleOrderCancel.SaleOrderId);
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
    }
}
