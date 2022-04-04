using DAL.Mappers;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//#region "Product Group"
			//modelBuilder.Entity<ProductGroup>().HasIndex(u => u.GroupCode).IsUnique();
			//modelBuilder.Entity<ProductGroup>();
			//#endregion

			new ProductMap(modelBuilder.Entity<Product>());
			new ProductGroupMap(modelBuilder.Entity<ProductGroup>());
			new AgreementMap(modelBuilder.Entity<Agreement>());

			#region "Agreement"

			// modelBuilder.Entity<Agreement>().HasOne(p => p.Product);
			#endregion

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductGroup> ProductGroups { get; set; }
		public DbSet<Agreement> Agreements { get; set; }
	}
}
