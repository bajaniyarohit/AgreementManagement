using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
	public interface IAgreementRepository : IGenericRepository<Agreement> 
	{
		IEnumerable<Agreement> GetAgreementAll();
	}
}
