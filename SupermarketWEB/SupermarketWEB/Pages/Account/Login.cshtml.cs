using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SupermarketWEB.Models;
using System.Data;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public string Email = "", Password = "";

        public async Task<IActionResult> OnPostAsync() { 
            if (!ModelState.IsValid) return Page();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SupermarketEF;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString)) { 
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT Email, Password FROM SupermarketEF.dbo.Users WHERE Email = '" + User.Email + "'; ", connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) { 
                        while (reader.Read())
                        {
                            Email = reader.GetString(0);
                            Password = reader.GetString(1);
                        }
                    }
                }
				if (User.Email == Email && User.Password == Password)
				{
					var claims = new List<Claim> {
						new Claim(ClaimTypes.Name, "admin"),
						new Claim(ClaimTypes.Email, User.Email),
					};
					var identity = new ClaimsIdentity(claims, "MyCookieAuth");
					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
					return RedirectToPage("/index");
				}
				else
				{
					ViewData["Message"] = "Email or Password Incorrent, Please try again";
					ViewData["liveToastBtn"] = true;
					return Page();
				}
			}
        }
    }
}
