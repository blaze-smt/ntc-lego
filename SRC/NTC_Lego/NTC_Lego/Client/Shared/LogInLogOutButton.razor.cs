namespace NTC_Lego.Client.Shared
{
    public partial class LogInLogOutButton
    {
        void Login()
        {
            NavigationManager.NavigateTo("login");
        }

        async Task Logout()
        {
            // Remove JWT token and userID from local storage
            await LocalStorage.RemoveItemAsync("token");
            await LocalStorage.RemoveItemAsync("userID");

            // Change current AuthenticationState of the user
            await CustomAuthStateProvider.GetAuthenticationStateAsync();

            NavigationManager.NavigateTo("/");
        }
    }
}
