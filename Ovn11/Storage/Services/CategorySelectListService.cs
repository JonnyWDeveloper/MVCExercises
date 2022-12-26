using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Services;


namespace Storage.Services
{
    public class CategorySelectListService : ICategorySelectListService
    {
        private readonly StorageContext _context;

        public CategorySelectListService(StorageContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            return await _context.Product.Select(m => m.Category)
                                .Distinct()
                                .Select(g => new SelectListItem
                                {
                                    Text = g.ToString(),
                                    Value = g.ToString()
                                })
                                .ToListAsync();
        }
    }
}
