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

    }
}
