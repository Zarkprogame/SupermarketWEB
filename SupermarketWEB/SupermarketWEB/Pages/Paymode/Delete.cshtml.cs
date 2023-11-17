using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
    public class DeleteModel : PageModel
    {
		private readonly SupermarketContext _context;
		public DeleteModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public PayMode PayModes { get; set; } = default!;
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.PayModes == null)
			{
				return NotFound();
			}
			var paymode = await _context.PayModes.FirstOrDefaultAsync(m => m.Id == id);

			if (paymode == null)
			{
				return NotFound();
			}
			else {
				PayModes = paymode;
			}
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.PayModes == null)
			{
				return NotFound();
			}
			var paymode = await _context.PayModes.FindAsync(id);
			if (paymode != null)
			{
				PayModes = paymode;
				_context.PayModes.Remove(PayModes);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage("./Index");
		}
	}
}
