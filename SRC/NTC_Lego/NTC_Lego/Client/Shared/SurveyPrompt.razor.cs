using Microsoft.AspNetCore.Components;

namespace NTC_Lego.Client.Shared
{
    public partial class SurveyPrompt
    {
        // TODO: We can remove this page, correct? It's a default page from the start?
        // Demonstrates how a parent component can supply parameters
        [Parameter]
        public string? Title { get; set; }
    }
}
