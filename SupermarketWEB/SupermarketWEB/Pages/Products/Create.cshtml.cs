using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using SupermarketWEB.Models.ViewModels;

namespace SupermarketWEB.Pages.Products
{
    public class CreateModel : PageModel
    {
		private readonly SupermarketContext _context;
        public CreateModel(SupermarketContext context)
		{
			_context = context;
		}

        [BindProperty]
        public CreateProduct CreateProduct { get; set; }
        public async Task<IActionResult> OnGet() {
            CreateProduct = new CreateProduct()
            {
                CategoryList = await _context.Categories.ToListAsync(),
                Product = new Product() 
            };            
            return Page();
        }
		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await _context.Products.AddAsync(CreateProduct.Product);
				await _context.SaveChangesAsync();
				return RedirectToPage("./Index");
			}
			else {
				return Page();
			}
		}
	}
}
