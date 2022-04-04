using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repositories
{
	public class AgreementRepository : GenericRepository<Agreement>, IAgreementRepository
	{
		public AgreementRepository(ApplicationDbContext context) : base(context)
		{
		}

		public IEnumerable<Agreement> GetAgreementAll()
		{
			return _context.Agreements.Include(p => p.ProductGroup).ThenInclude(p => p.Products);
		}
	}
}
