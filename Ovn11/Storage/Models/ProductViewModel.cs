

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class ProductViewModel
    {
        //public int Id
        //{
        //    get => Id;
        //    set => Id = value;
        //}
        [NotMapped]
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        [NotMapped]
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public string? Name
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }
        public int Count
        {
            get; set;
        }
        public int InventoryValue
        {
            get; set;
        }
    }
}
