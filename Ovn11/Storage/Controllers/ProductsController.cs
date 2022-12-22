using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext context;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(StorageContext context, ILogger<ProductsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await context.Product.OrderBy(p => p.Name).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Product == null)
            {
                logger.LogInformation("Details not found for id {id}", id);
                return View("Error");
            }

            var product = await context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return View("Error");
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();

        }
        // GET: Products/Inventory
        public async Task<IActionResult> Inventory()
        {
            if (context.Product == null)
            {
                return View("Error");
            }

            IEnumerable<ProductViewModel> productViewInventory =
                await context.Product.Select(p => new ProductViewModel()
                {
                    Price = p.Price,
                    Name = p.Name,
                    Count = p.Count,
                    InventoryValue = p.Count * p.Price
                })
                .OrderBy(p => p.Name)
                .ToArrayAsync();

            if (productViewInventory == null)
            {
                return View("Error");
            }

            return View(productViewInventory);
        }
        //GET: Products/SearhInventory (Displayed on Products.Inventory)
        public async Task<IActionResult> SearchInventory(string searchString)
        {
            var viewModel = await context.Product
                .Where(p => p.Name.StartsWith(searchString))
                .Select(p => new ProductViewModel()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Count = p.Count,
                })
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(nameof(Inventory), viewModel);
        }

        //GET: Products/Search (Displayed on Products.Index)
        public async Task<IActionResult> Search(string searchString)
        {
            var view = await context.Product
                .Where(p => p.Category.StartsWith(searchString))
                .Select(p => new Product()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Orderdate = p.Orderdate,
                    Category = p.Category,
                    Shelf = p.Shelf,
                    Count = p.Count,
                    Description = p.Description,
                })
                  .OrderBy(p => p.Category)
                  .ToListAsync();

            return View(nameof(Index), view);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Product == null)
            {
                logger.LogInformation("No id passed for edit");
                return View("Error");
            }

            var product = await context.Product.FindAsync(id);

            if (product == null)
            {
                logger.LogInformation("Edit details not found for id {id}", id);
                return View("Error");
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                logger.LogInformation("Id mismatch in passed information. " +
                "Id value {id} did not match model value of {productId}",
                id, product.Id);
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(product);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        logger.LogInformation("A product with the following Id: {productId} does not exist in the database", product.Id);
                        return View("Error");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Product == null)
            {
                logger.LogInformation("No id value was passed for deletion.");
                return View("Error");
            }

            var product = await context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                logger.LogInformation("Id to be deleted ({id}) does not exist in database.",
                id);
                return View("Error");
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Product == null)
            {
                return Problem("Entity set 'StorageContext.Product'  is null.");
            }
            var product = await context.Product.FindAsync(id);
            if (product != null)
            {
                context.Product.Remove(product);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return context.Product.Any(e => e.Id == id);
        }
    }
}
