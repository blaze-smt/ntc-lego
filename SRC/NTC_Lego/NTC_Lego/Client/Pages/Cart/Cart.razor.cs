using NTC_Lego.Shared;
using System.Net.Http.Json;

namespace NTC_Lego.Client.Pages.Cart
{
    public partial class Cart
    {
        private IEnumerable<CartItemVM>? cartItems;
        private User? userObj = null;
        string stringID;
        int UserId;

        void Browse()
        {
            NavigationManager.NavigateTo("itemscatalog");
        }

        protected override async Task OnInitializedAsync()
        {
            // Gets the UserId as a string
            stringID = await LocalStorage.GetItemAsync<string>("userID");

            // converts the UserID to int
            UserId = Int32.Parse(stringID);

            // Gets the list of cart items based on the user's UserId
            cartItems = await Http.GetFromJsonAsync<List<CartItemVM>>($"/Cart/cartitem?userId={UserId}");
        }
    }
}
