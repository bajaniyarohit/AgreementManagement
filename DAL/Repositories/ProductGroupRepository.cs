using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace DAL.Repositories
{
	public class ProductGroupRepository : GenericRepository<ProductGroup>, IProductGroupRepository
	{
		public ProductGroupRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
