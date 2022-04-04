using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Mappers
{
	public class AgreementMap
	{
		public AgreementMap(EntityTypeBuilder<Agreement> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.UserId).IsRequired();
			entityBuilder.Property(t => t.UserId).HasMaxLength(450);
			entityBuilder.Property(t => t.EffectiveDate).HasColumnType("date");
			entityBuilder.Property(t => t.ExpirationDate).HasColumnType("date");
			entityBuilder.Property(t => t.Active).IsRequired();
			entityBuilder.HasOne(p => p.Product).WithMany(p=> p.Agreements).HasForeignKey(p=>p.ProductId);
			entityBuilder.HasOne(p => p.ProductGroup).WithMany(p => p.Agreements).HasForeignKey(p => p.ProductGroupId);
		}
	}
}
