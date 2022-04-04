using AgreementManagementApp.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace AgreementManagementApp.Controllers
{
	[Authorize] // Require authenticated requests.
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IAgreementRepository _agreementRepository;
		private IProductGroupRepository _productGroupRepository;
		private IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public HomeController(ILogger<HomeController> logger, IAgreementRepository agreementRepository, IProductGroupRepository productGroupRepository, IProductRepository productRepository, IMapper mapper)
		{
			_logger = logger;
			_agreementRepository = agreementRepository;
			_productGroupRepository = productGroupRepository;
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			return View("Index");
		}

		[HttpPost]
		public IActionResult GetAgreement()
		{
			List<Agreement> agreements = _agreementRepository.GetAgreementAll().ToList();
			var model = _mapper.Map<List<AgreementViewModel>>(agreements);
			model.ForEach(f => f.UserName = User.Identity.Name);
			return Json(JsonConvert.SerializeObject(model));
		}

		public IActionResult AddOrEdit(int id = 0)
		{
			List<ProductGroup> productGroups = _productGroupRepository.GetAll().ToList();
			@ViewBag.ProductGroup = _mapper.Map<List<ProductGroupViewModel>>(productGroups);

			if (id == 0)
			{
				@ViewBag.Product = new List<ProductViewModel>();
				return View(new AgreementViewModel());
			}
			else
			{
				var agreement = _agreementRepository.GetById(id);
				var model = _mapper.Map<AgreementViewModel>(agreement);
				if (agreement == null)
				{
					return NotFound();
				}
				List<Product> product = _productRepository.Find(f=> f.ProductGroupId == model.ProductGroupId).ToList();
				@ViewBag.Product = _mapper.Map<List<ProductViewModel>>(product);
				return View(model);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddOrEdit(int id, AgreementViewModel agreementViewModel)
		{
			if (ModelState.IsValid)
			{
				//Insert
				if (id == 0)
				{
					var model = _mapper.Map<Agreement>(agreementViewModel);
					model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
					model.ProductPrice = _productRepository.GetById(model.ProductId).Price;
					_agreementRepository.Add(model);
				}
				//Update
				else
				{
					try
					{
						var model = _mapper.Map<Agreement>(agreementViewModel);
						_agreementRepository.Update(model);
						
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!AgreementModelExists(agreementViewModel.Id))
						{ return NotFound(); }
						else
						{ throw; }
					}
				}
				return Json(new { isValid = true });
			}
			return Json(new { isValid = false });
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var agreement = _agreementRepository.GetById(id);
			_agreementRepository.Remove(agreement);
			return Json(new { isValid = true });
		}

		public IActionResult GetProducts(int id) {
			List<Product> product = _productRepository.Find(f => f.ProductGroupId == id).ToList();
			var model = _mapper.Map<List<ProductViewModel>>(product);
			return Json(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private bool AgreementModelExists(int id)
		{
			return _agreementRepository.Any(e => e.Id == id);
		}
	}
}
