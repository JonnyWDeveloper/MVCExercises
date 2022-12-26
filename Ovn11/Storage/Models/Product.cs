using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class Product
    {
        [NotMapped]
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        [NotMapped]
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public int Id
        {
            get; set;
        }
        [Required]
        public string? Name
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Orderdate
        {

            get; set;
        }
        [Required]
        public string? Category
        {
            get; set;
        }

        [Required]
        public string? Shelf
        {
            get; set;
        }

        [Required]
        public int Count
        {
            get; set;
        }
        [Required]
        public string? Description
        {
            get; set;
        }
        [NotMapped]
        public float Rating
        {
            get; set;
        }


    }
}
