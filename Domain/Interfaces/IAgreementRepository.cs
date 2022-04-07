using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
	public interface IAgreementRepository : IGenericRepository<Agreement> 
	{
		IEnumerable<Agreement> GetAgreementAll();
	}
}
