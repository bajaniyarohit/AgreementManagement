using Services.ViewModels;
using Services.ViewModels.Agreement;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Services.Abstract
{
	public interface IAgreementService
	{
		JDataTableResponse<AgreementViewModel> GetAgreement(JDatatableRequest Request, String useName);
		AgreementViewModel GetAgreementById(int id);
		void CreateOrUpdateAgreement(int id,AgreementViewModel agreementViewModel, ClaimsPrincipal User);
		void DeleteAgreement(int id);
		List<ProductViewModel> GetProductsByProductGroup(int groupid);

	}
}
