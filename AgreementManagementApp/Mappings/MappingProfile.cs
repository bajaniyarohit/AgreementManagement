using AgreementManagementApp.Models;
using AutoMapper;
using Domain.Entities;

namespace AgreementManagementApp.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			#region "Agreement"
			CreateMap<Agreement, AgreementViewModel>()
			.ForMember(dest=> dest.UserName, source => source.Ignore())
			.ForSourceMember(source => source.Product, o => o.DoNotValidate())
			.ForSourceMember(source => source.ProductGroup, o => o.DoNotValidate())
			.ForMember(dest => dest.ProductNumber, source => source.MapFrom(source => source.Product.ProductNumber))
			.ForMember(dest => dest.ProductDescription, source => source.MapFrom(source => source.Product.ProductDescription))
			.ForMember(dest => dest.GroupCode, source => source.MapFrom(source => source.ProductGroup.GroupCode))
			.ForMember(dest => dest.GroupDescription, source => source.MapFrom(source => source.ProductGroup.GroupDescription));
			


			CreateMap<AgreementViewModel, Agreement>()
			.ForMember(dest => dest.Product, source => source.Ignore())
			.ForMember(dest => dest.ProductGroup, source => source.Ignore());
			
			// .ForAllMembers(opt => opt.Ignore());
			#endregion

			#region "ProductGroup"
			CreateMap<ProductGroup, ProductGroupViewModel>()
			.ForSourceMember(source => source.Products, o => o.DoNotValidate())
			.ForSourceMember(source => source.Agreements, o => o.DoNotValidate())
			.ForSourceMember(source => source.Active, o => o.DoNotValidate());
			#endregion

			#region "Product"
			CreateMap<Product, ProductViewModel>()
			.ForSourceMember(source => source.ProductGroup, o => o.DoNotValidate())
			.ForSourceMember(source => source.Agreements, o => o.DoNotValidate())
			.ForSourceMember(source => source.Active, o => o.DoNotValidate())
			.ForSourceMember(source => source.Price, o => o.DoNotValidate());
			#endregion
		}
	}
}
