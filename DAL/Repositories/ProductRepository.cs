using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace DAL.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
