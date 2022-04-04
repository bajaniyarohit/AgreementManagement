using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class General
	{
		public int Id { get; set; }
		public bool Active { get; set; }
	}
}
