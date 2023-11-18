using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
	[Authorize]
	public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;
        public IndexModel(SupermarketContext context) { 
            _context = context;
        }

		public IEnumerable<Product> Products { get; set; }
		public async Task OnGet()
		{
			Products = await _context.Products.Include(c => c.Category).ToListAsync();
		}
	}
}
