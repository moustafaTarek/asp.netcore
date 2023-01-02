
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            : base( x=> 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandID.HasValue || x.ProductBrandId == productParams.BrandID)  &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
            AddOrderBy(x=>x.Name);
            AddPaging(productParams.PageSize*(productParams.PageIndex-1), productParams.PageSize);

            if(productParams.Sort !=null)
            {
                switch(productParams.Sort)
                {
                    case "priceAsc" :
                        AddOrderBy(p=>p.Price);
                        break;
                        
                    case "priceDesc" :
                        AddOrderByDescending(p=>p.Price);
                        break;
                    
                    default :
                        AddOrderBy(p=>p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id == id)
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}