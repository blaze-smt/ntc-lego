using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

namespace NTC_Lego.Server.Controllers
{
    public class AdminPortalController : Controller
    {
        private readonly DataService _dataService;

        public AdminPortalController(DataContext dataContext)
        {
            _dataService = new DataService(dataContext);
        }

        public IActionResult ItemList()
        {
            List<Item> model = _dataService.GetItems();
            return View(model);
        }
    }
}
