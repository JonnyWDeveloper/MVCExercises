using Microsoft.AspNetCore.Mvc;
using Storage.Models.ViewModels;

namespace Storage.ViewComponents
{
    public class StarViewComponent : ViewComponent
    {
        //private readonly StorageContext storageContext;

        //public StarViewComponent(StorageContext storageContext)
        //{
        //    this.storageContext = storageContext;
        //}


        public IViewComponentResult Invoke(float rating)
        {
            var doubleRating = (int)Math.Round(rating * 2);

            var model = new StarViewModel
            {
                Stars = doubleRating / 2,
                IsHalfStar = doubleRating % 2 == 1
            };

            return View(model);
        }
    }
}
