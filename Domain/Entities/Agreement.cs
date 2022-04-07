using System;
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities
{
	public class Agreement: General
	{
		public string UserId { get; set; }
		public int ProductGroupId { get; set; }
		public int ProductId{ get; set; }
		public DateTime EffectiveDate{ get; set; }
		public DateTime ExpirationDate { get; set; }
		public decimal ProductPrice { get; set; }
		public decimal NewPrice { get; set; }
		public ProductGroup ProductGroup { get; set; }
		public Product Product{ get; set; }
		public IdentityUser User { get; set; }
	}
}
