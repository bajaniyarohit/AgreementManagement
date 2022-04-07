using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Agreement
{
	public class AgreementViewModel
	{

		public int Id { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		[Required(ErrorMessage = "This Field is required.")]
		public int ProductGroupId { get; set; }
		[Required(ErrorMessage = "This Field is required.")]
		public int ProductId { get; set; }
		[Required(ErrorMessage = "This Field is required.")]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public Nullable<DateTime> EffectiveDate { get; set; }
		[Required(ErrorMessage = "This Field is required.")]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public Nullable<DateTime> ExpirationDate { get; set; }
		public decimal ProductPrice { get; set; }
		[Range(1, Double.PositiveInfinity)]
		[Required(ErrorMessage = "This Field is required.")]
		public decimal NewPrice { get; set; }
		public bool Active { get; set; }
		public string ProductNumber { get; set; }
		public string ProductDescription { get; set; }
		public string GroupCode { get; set; }
		public string GroupDescription { get; set; }
		public List<ProductViewModel> Products { get; set; }
		public List<ProductGroupViewModel> ProductGroups { get; set; }

	}
}
