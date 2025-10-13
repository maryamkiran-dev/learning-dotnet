using DTO.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDb _pro;

        public ProductController(ProductDb repo)
        {
            _pro = repo;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductDTO dto)
        {
            var result = _pro.AddProduct(dto);
            return Ok(result);
        }
    }
}
