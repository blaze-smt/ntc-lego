using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

namespace NTC_Lego.Server.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly DataService _dataService;

        public AdminController(DataContext dataContext)
        {
            _dataService = new DataService(dataContext);
        }

        public IActionResult ItemList()
        {
            List<Item> model = _dataService.GetItems();
            return Ok(model);
        }
    }
}
