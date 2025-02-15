using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagination<T>(IGenericRepository<T>_repo,ISpecification<T>spec,
            int pageIndex,int pageSize)where T:BaseEntity
        {
            var items = await _repo.GetAllAsyncWithSpec(spec);
            var count = await _repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex,pageSize,count,items);
            return Ok(pagination);
        }
    }
}
