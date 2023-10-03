using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Basketball_Shoes.Models;

namespace Basketball_Shoes.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Basketball_Shoes.Models.User> User { get; set; } = default!;
		public DbSet<Basketball_Shoes.Models.Product> Product { get; set; } = default!;
	}
}