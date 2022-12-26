using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("serverError")]
        public IActionResult ServerError()
        {
            ProductDto pro = null ;
            pro.Description.ToString();
            return Ok();
        }
    }
}