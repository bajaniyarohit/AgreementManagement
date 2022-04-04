using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
	public class Product : General
	{
		
		public int ProductGroupId { get; set; }
		public string ProductDescription { get; set; }
		public string ProductNumber { get; set; }
		public decimal Price { get; set; }
		public ProductGroup ProductGroup { get; set; }
		public ICollection<Agreement> Agreements { get; set; }
	}
}
