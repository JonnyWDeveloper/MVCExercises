using Microsoft.AspNetCore.Mvc.Rendering;

namespace Storage.Services
{
    public interface ICategorySelectListService
    {
        Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
    }
}