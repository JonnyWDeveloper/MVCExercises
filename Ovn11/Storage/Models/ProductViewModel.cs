using System.Collections;

namespace Storage.Models
{
    public class ProductViewModel
    {
        //public int Id
        //{
        //    get => Id;
        //    set => Id = value;
        //}
        public string Name
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
