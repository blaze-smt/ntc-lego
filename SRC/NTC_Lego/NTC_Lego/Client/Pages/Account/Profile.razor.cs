using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.Account
{
    public partial class Profile
    {
        private UserVM? user;

        protected override async Task OnInitializedAsync()
        {
            string stringID = await LocalStorage.GetItemAsync<string>("userID");

            int UserId = Int32.Parse(stringID);

            user = await Http.GetFromJsonAsync<UserVM>($"/account/user?userId={UserId}");
        }

        protected async override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Profile");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");
    }
}
