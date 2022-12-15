using Microsoft.JSInterop;
using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.Account
{
    public partial class Register
    {
        UserRegister user = new UserRegister();

        void HandleRegistration()
        {
            Http.PostAsJsonAsync($"/account/register?", user);
            JSRuntime.InvokeVoidAsync("console.log", "This is the new user:", user);
            NavigationManager.NavigateTo("login");
        }
    }
}
