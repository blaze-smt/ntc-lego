using Newtonsoft.Json;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.Account
{
    public partial class Login
    {
        private UserLogin userForm = new UserLogin();
        private User? userObj = null;
        private string? errorMessage = null;

        private async void HandleLogin()
        {
            HttpResponseMessage loginResponse = await Http.PostAsJsonAsync($"/account/login?", userForm);

            // Checks if user login info is correct
            if (loginResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UserTokenVM? vm = JsonConvert.DeserializeObject<UserTokenVM>(await loginResponse.Content.ReadAsStringAsync());
                userObj = vm.User;
                string? token = vm.Token;

                // Store JWT in local storage
                await LocalStorage.SetItemAsync("token", token);
                await LocalStorage.SetItemAsync("userID", vm.User.UserId);
                await CustomAuthStateProvider.GetAuthenticationStateAsync();

                NavigationManager.NavigateTo("/");
            }
            else
            {
                this.errorMessage = "Email or Password is incorrect. Please try again!!";
                StateHasChanged();
            }
        }
    }
}
