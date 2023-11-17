using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;
        public IndexModel(SupermarketContext context) { 
            _context = context;
        }
		//public IList<Product> Products { get; set; } = default!;
		//public async Task OnGetAsync()
		//{
		//    if (_context.Products != null) {
		//        Products = await _context.Products.ToListAsync();
		//    }
		//}
		public IEnumerable<Product> Products { get; set; }
		public async Task OnGet()
		{
			Products = await _context.Products.Include(c => c.Category).ToListAsync();
		}
	}
}
