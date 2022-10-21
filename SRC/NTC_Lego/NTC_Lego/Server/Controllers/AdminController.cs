using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataService _dataService;

        public AdminController(DataContext dataContext)
        {
            _dataService = new DataService(dataContext);
        }

        [HttpGet]
        public IEnumerable<Item> Get(int page)
        {
            int pageSize = 10;

            var items = _dataService.GetItems().Skip((page - 1) * pageSize).Take(pageSize);
            return items;

        }
    }
}
