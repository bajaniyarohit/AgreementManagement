using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mappers
{
	public class ProductMap
	{
		public ProductMap(EntityTypeBuilder<Product> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.HasIndex(u => u.ProductNumber).IsUnique();
			entityBuilder.Property(u => u.ProductNumber).IsRequired();
			entityBuilder.Property(u => u.ProductNumber).HasMaxLength(5);
			entityBuilder.Property(t => t.ProductDescription).IsRequired();
			entityBuilder.Property(t => t.Price).IsRequired();
			entityBuilder.Property(t => t.Active).IsRequired();
			entityBuilder.HasOne(p => p.ProductGroup).WithMany(p => p.Products).HasForeignKey(p => p.ProductGroupId).OnDelete(DeleteBehavior.ClientNoAction);
			entityBuilder.HasData(
						new Product
						{
							Id = 1,
							ProductGroupId = 1,
							ProductNumber = "PN001",
							ProductDescription = "Product 1",
							Price = 10,
							Active = true
						},
						new Product
						{
							Id = 2,
							ProductGroupId = 1,
							ProductNumber = "PN002",
							ProductDescription = "Product 2",
							Price = 20,
							Active = true
						},
						new Product
						{
							Id = 3,
							ProductNumber = "PN003",
							ProductGroupId = 2,
							ProductDescription = "Product 3",
							Price = 30,
							Active = true
						},
						new Product
						{
							Id = 4,
							ProductGroupId = 2,
							ProductNumber = "PN004",
							ProductDescription = "Product 4",
							Price = 40,
							Active = true
						});
		}
	}
}
