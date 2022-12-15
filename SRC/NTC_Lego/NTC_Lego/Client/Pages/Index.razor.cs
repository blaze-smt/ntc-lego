namespace NTC_Lego.Client.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {
            currentPage.SetCurrentPageName("Home");
            base.OnInitialized();
        }

        void ChangeName() => currentPage.SetCurrentPageName("Name changed");
    }
}
