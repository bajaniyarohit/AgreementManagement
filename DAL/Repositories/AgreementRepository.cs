using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
	public class AgreementRepository : GenericRepository<Agreement>, IAgreementRepository
	{
		public AgreementRepository(ApplicationDbContext context) : base(context)
		{
		}

		public IEnumerable<Agreement> GetAgreementAll()
		{
			return _context.Agreements.Include(p => p.ProductGroup).Include(p => p.Product).Include(p => p.User);
		}
	}
}
