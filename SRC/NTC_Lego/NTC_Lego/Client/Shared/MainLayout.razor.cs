namespace NTC_Lego.Client.Shared
{
    public partial class MainLayout
    {
        private bool collapseNavMenu = false;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override void OnInitialized()
        {
            // if the OnChange event is raised, refresh this view
            currentPage.OnChange += () => StateHasChanged();

            base.OnInitialized();
        }
    }
}
