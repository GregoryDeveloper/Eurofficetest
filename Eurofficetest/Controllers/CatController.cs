using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eurofficetest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eurofficetest.Controllers
{
    [ApiController]
    [Route("api/cat")]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;
        private readonly ICatService _catService;


        public CatController(ILogger<CatController> logger, ICatService catService)
        {
            _logger = logger;
            _catService = catService;
        }

        [HttpGet("limit/{limit}/page/{page}")]
        public async Task<IActionResult>Get([FromRoute] int limit, [FromRoute] int page)
        {
            var result = await _catService.GetCategoriesAsync(limit, page);
            return Ok(result);
        }

        [HttpGet("limit/{limit}/page/{page}/category/{categoryId}")]
        public async Task<IActionResult> GetImagesBtCategory([FromRoute] int limit, [FromRoute] int page, [FromRoute] int categoryId)
        {
            var result = await _catService.GetImagesAsync(limit, page, categoryId);
            return Ok(result);
        }
    }
}
