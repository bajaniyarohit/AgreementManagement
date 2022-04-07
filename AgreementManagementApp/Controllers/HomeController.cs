using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Abstract;
using Services.ViewModels;
using Services.ViewModels.Agreement;

namespace AgreementManagementApp.Controllers
{
	[Authorize] // Require authenticated requests.
	public class HomeController : Controller
	{
		private readonly IAgreementService _agreementService;

		public HomeController(IAgreementService agreementService)
		{
			_agreementService = agreementService;
		}

		public IActionResult Index()
		{
			return View("Index");
		}

		public IActionResult GetAgreement(JDatatableRequest param)
		{
			return Json(_agreementService.GetAgreement(param, User.Identity.Name));
		}

		public IActionResult AddOrEdit(int id = 0)
		{
			return View(_agreementService.GetAgreementById(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddOrEdit(int id, AgreementViewModel agreementViewModel)
		{
			if (ModelState.IsValid)
			{
				_agreementService.CreateOrUpdateAgreement(id, agreementViewModel, User);
				return Json(new { isValid = true });
			}
			return Json(new { isValid = false });
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			_agreementService.DeleteAgreement(id);
			return Json(new { isValid = true });
		}

		public IActionResult GetProducts(int id)
		{

			return Json(_agreementService.GetProductsByProductGroup(id));
		}
	}
}
