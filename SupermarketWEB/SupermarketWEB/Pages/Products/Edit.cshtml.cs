using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using SupermarketWEB.Models.ViewModels;

namespace SupermarketWEB.Pages.Products
{
    public class EditModel : PageModel
    {
		private readonly SupermarketContext _context;
		public EditModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public CreateProduct CreateProduct { get; set; }
		public async Task<IActionResult> OnGet(int id)
		{
			CreateProduct = new CreateProduct()
			{
				CategoryList = await _context.Categories.ToListAsync(),
				Product = await _context.Products.FindAsync(id)
			};
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_context.Attach(CreateProduct.Product).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(CreateProduct.Product.Id))
				{
					return NotFound();
				}
				else {
					throw;
				}
			}
			return RedirectToPage("./Index");
		}
		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
