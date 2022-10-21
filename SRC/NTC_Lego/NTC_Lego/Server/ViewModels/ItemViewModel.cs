using NTC_Lego.Shared;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NTC_Lego.Server.ViewModels
{
    public class ItemViewModel
    {
        public Item ItemId { get; set; }

        public Item ItemName { get; set; }

        public List<decimal> ItemWeight { get; set; }

        public List<ItemType> ItemType { get; set; }

        public List<Category> Category { get; set; }
    }
}
