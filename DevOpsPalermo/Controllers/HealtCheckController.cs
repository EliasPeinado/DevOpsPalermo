﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsPalermo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealtCheckController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
