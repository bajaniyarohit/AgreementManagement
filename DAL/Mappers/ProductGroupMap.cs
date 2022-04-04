using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Mappers
{
	public class ProductGroupMap
	{
		public ProductGroupMap(EntityTypeBuilder<ProductGroup> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.HasIndex(u => u.GroupCode).IsUnique();
			entityBuilder.Property(t => t.GroupCode).IsRequired();
			entityBuilder.Property(t => t.GroupCode).HasMaxLength(5);
			entityBuilder.Property(t => t.GroupDescription).IsRequired();
			entityBuilder.Property(t => t.Active).IsRequired();
			entityBuilder.HasData(
					   new ProductGroup
					   {
						   Id = 1,
						   GroupCode = "PG001",
						   GroupDescription = "Product Group 1",
						   Active = true
					   },
					   new ProductGroup
					   {
						   Id = 2,
						   GroupCode = "PG002",
						   GroupDescription = "Product Group 2",
						   Active = true
					   });
		}
	}
}
