using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var Product = _dbContext.Products.Find(100);
            if (Product==null)
            
                return NotFound(new ApiResponse(404));
            
            else
                return Ok(Product);

        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var Product = _dbContext.Products.Find(100);
            var ProductDto = Product.ToString();
 
                return Ok(ProductDto);

        }

        [HttpGet("unathorized")]
        public ActionResult GetUnAthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
                return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        { 
                return Ok();
        }
    }
}
