using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces.UseCases.Modules;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        public readonly IGetModulesWithEverythingUseCase _getModulesWithEverythingUseCase;

        public ModuleController(IGetModulesWithEverythingUseCase getModulesWithEverythingUseCase)
        {
            _getModulesWithEverythingUseCase = getModulesWithEverythingUseCase;
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetModules()
        {
            var result = await _getModulesWithEverythingUseCase.Execute();
            return Ok(result);
        }
    }
}
