using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class ProductGroup: General
	{
		public string GroupCode { get; set; }
		public string GroupDescription { get; set; }
		public ICollection<Product> Products { get; set; }

		public ICollection<Agreement> Agreements { get; set; }

	}
}
