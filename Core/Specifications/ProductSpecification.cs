using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) :base(p =>
        (string.IsNullOrEmpty(specParams.Search)|| p.Name.ToLower().Contains(specParams.Search))&&
        (specParams.Brands.Any()|| specParams.Brands.Contains(p.Brand))&&
        (specParams.Types.Any()|| specParams.Types.Contains(p.Type))
    )
    {
        Pagination(specParams.PageSize*(specParams.PageIndex-1),specParams.PageSize);
        switch (specParams.sort)
        {
            case "priceAsc":
                AddOrderByAsc(x=>x.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(x=>x.Price);
                break;
            default:
                AddOrderByAsc(x=>x.Name);
                break;
        }
    }
}