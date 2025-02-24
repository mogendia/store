using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> _repo) : BaseApiController
{
 
    [HttpGet]
    //public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    //{
    //    var query = new ProductSpecification(brand,type,sort);
    //    var products = await _repo.GetAllAsyncWithSpec(query);
    //    return Ok(products);
    //}
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var query = new ProductSpecification(specParams);

        return await CreatePagination(_repo, query, specParams.PageIndex,specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _repo.Add(product);
        if (await _repo.SaveAll())
        {
            return CreatedAtAction("GetProduct",new {id=product.Id},product);
        }
        return BadRequest("Problem in creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id)) 
            return BadRequest("Cannot Update This Product");

       _repo.Update(product);
       if (await _repo.SaveAll())
       {
           return NoContent();
       }
        return BadRequest("Problem updating the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandSpecification();
        var brands = await _repo.GetAllAsyncWithSpec(spec);
        return Ok(brands);
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeSpecification();
        return Ok(await _repo.GetAllAsyncWithSpec(spec));
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return NotFound();
        _repo.Remove(product);
        if (await _repo.SaveAll())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting the product");
    }

    private bool ProductExists(int id)
    {
        return _repo.IsExists(id);
    }
    
}

