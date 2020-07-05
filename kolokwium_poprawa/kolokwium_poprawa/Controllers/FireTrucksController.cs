using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium_poprawa.Requests;
using kolokwium_poprawa.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace kolokwium_poprawa.Controllers
{
    [ApiController]
    [Route("api/actions/1/fire-trucks")]
    public class FireTrucksController : ControllerBase
    {
        private readonly IDataBaseService _service;
        public IConfiguration Configuration;

        public FireTrucksController(IDataBaseService service, IConfiguration configuration)
        {
            Configuration = configuration;
            _service = service;
        }

        [HttpPost]
        public IActionResult FireTruckToAction(FireTrucksRequest request)
        {
            var response = _service.FireTruckToAction(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}