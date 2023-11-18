using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Register
{
    public class CreateModel : PageModel
    {
		private readonly SupermarketContext _context;
        public CreateModel(SupermarketContext context)
		{
			_context = context;
		}
		public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User Users { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid || _context.Users == null || Users == null) { 
                return Page();
            }
            _context.Users.Add(Users);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account/Login");
        }
    }
}
