using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Services.Abstract;
using Services.ViewModels.Agreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Services.ViewModels;

namespace Services.Concrete
{
	public class AgreementService : IAgreementService
	{
		private readonly IAgreementRepository _agreementRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IProductRepository _productRepository;
		private readonly IProductGroupRepository _productGroupRepository;
		public AgreementService(IProductGroupRepository productGroupRepository, IAgreementRepository agreementRepository, IMapper mapper, UserManager<IdentityUser> userManager, IProductRepository productRepository)
		{
			_userManager = userManager;
			_agreementRepository = agreementRepository;
			_mapper = mapper;
			_productRepository = productRepository;
			_productGroupRepository = productGroupRepository;
		}
		public void CreateOrUpdateAgreement(int id, AgreementViewModel agreementViewModel, ClaimsPrincipal User)
		{
			if (id == 0)
			{
				var model = _mapper.Map<Agreement>(agreementViewModel);
				model.UserId = _userManager.GetUserId(User);
				model.ProductPrice = _productRepository.GetById(model.ProductId).Price;
				_agreementRepository.Add(model);
			}
			else
			{
				var model = _mapper.Map<Agreement>(agreementViewModel);
				_agreementRepository.Update(model);
			}
			_agreementRepository.SaveChanges();
		}

		public void DeleteAgreement(int id)
		{
			var agreement = _agreementRepository.GetById(id);
			_agreementRepository.Remove(agreement);
			_agreementRepository.SaveChanges();
		}

		public JDataTableResponse<AgreementViewModel> GetAgreement(JDatatableRequest Request, string useName)
		{
			IEnumerable<Agreement> agreements = _agreementRepository.GetAgreementAll();
			// Filter based on search
			if (!string.IsNullOrEmpty(Request.sSearch))
			{	
				agreements = agreements.Where(f =>
					(f.ProductGroup.GroupCode.ToLower().Contains(Request.sSearch.ToLower()) ||
					f.Product.ProductNumber.ToLower().Contains(Request.sSearch.ToLower()) ||
					f.EffectiveDate.ToString("dd'/'MM'/'yyyy").ToLower().Contains(Request.sSearch.ToLower()) ||
					f.ExpirationDate.ToString("dd'/'MM'/'yyyy").ToLower().Contains(Request.sSearch.ToLower()) ||
					f.ProductPrice.ToString().Contains(Request.sSearch) ||
					f.NewPrice.ToString().Contains(Request.sSearch) ||
					f.User.UserName.ToString().Contains(Request.sSearch.ToString())));
			}

			switch (Request.iSortCol_0)
			{
				case 1:
					agreements = Request.sSortDir_0 == "asc" ? agreements.OrderBy(o => o.ProductGroup.GroupCode) : agreements.OrderByDescending(o => o.ProductGroup.GroupCode);
					break;
				case 2:
					agreements = Request.sSortDir_0 == "asc" ? agreements.OrderBy(o => o.Product.ProductNumber) : agreements.OrderByDescending(o => o.Product.ProductNumber);
					break;
				case 3:
					agreements = Request.sSortDir_0 == "asc" ? agreements.OrderBy(o => o.EffectiveDate) : agreements.OrderByDescending(o => o.EffectiveDate);
					break;
				case 4:
					agreements = Request.sSortDir_0 == "asc" ? agreements.OrderBy(o => o.ExpirationDate) : agreements.OrderByDescending(o => o.ExpirationDate);
					break;
				case 5:
					agreements = Request.sSortDir_0 == "asc" ? agreements.OrderBy(o => o.ProductPrice) : agreements.OrderByDescending(o => o.ProductPrice);
					break;
				case 6:
					agreements = Request.sSortDir_0 == "asc" ? agreements.OrderBy(o => o.NewPrice) : agreements.OrderByDescending(o => o.NewPrice);
					break;
			}

			
			// Filter based page
			agreements = agreements.Skip(Request.iDisplayStart)
				.Take(Request.iDisplayLength).ToList();

			var response = _mapper.Map<List<AgreementViewModel>>(agreements);
			
			// Get total 
			int Total = _agreementRepository.Count();
			return new JDataTableResponse<AgreementViewModel>
			{
				aaData = response,
				sEcho = Request.sEcho,
				iTotalRecords = Total,
				iTotalDisplayRecords = Total,
			};
		}

		public AgreementViewModel GetAgreementById(int id)
		{
			AgreementViewModel response = new AgreementViewModel();
			if (id == 0)
			{
				response.Products = new List<ProductViewModel>();
			}
			else
			{
				var agreement = _agreementRepository.GetById(id);
				response = _mapper.Map<AgreementViewModel>(agreement);

				List<Product> products = _productRepository.Find(f => f.ProductGroupId == agreement.ProductGroupId).ToList();
				response.Products = _mapper.Map<List<ProductViewModel>>(products);
			}
			List<ProductGroup> productGroups = _productGroupRepository.GetAll().ToList();
			response.ProductGroups = _mapper.Map<List<ProductGroupViewModel>>(productGroups);
			return response;
		}

		public List<ProductViewModel> GetProductsByProductGroup(int groupid)
		{
			List<Product> products = _productRepository.Find(f => f.ProductGroupId == groupid).ToList();
			return _mapper.Map<List<ProductViewModel>>(products);
		}
	}
}
