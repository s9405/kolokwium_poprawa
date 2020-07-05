using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium_poprawa.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace kolokwium_poprawa.Controllers
{
    [ApiController]
    [Route("api/firefigthers/1/actions")]
    public class FireFigthersController : ControllerBase
    {

        private readonly IDataBaseService _service;
        public IConfiguration Configuration;

        public FireFigthersController(IDataBaseService service, IConfiguration configuration)
        {
            Configuration = configuration;
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetFireFigthersActions(int id)
        {

            var response = _service.GetFireFigthersActions(id);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);


        }
    }
}