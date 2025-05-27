using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeHospitalRequestController : ControllerBase
    {
        public readonly IChangeHospitalRequestCreateUseCase _changeHospitalRequestCreateUseCase;
        public readonly IChangeHospitalRequestGetAllPaginatedUseCase _changeHospitalRequestGetAllPaginatedUseCase;
        private readonly IChangeHospitalRequestGetByIdUseCase _changeHospitalRequestGetByIdUseCase;
        private readonly IChangeHospitalRequestChangeStateUseCase _changeHospitalRequestChangeStateUseCase;

        public ChangeHospitalRequestController(
            IChangeHospitalRequestChangeStateUseCase changeHospitalRequestChangeStateUseCase,
            IChangeHospitalRequestGetByIdUseCase changeHospitalRequestGetByIdUseCase,
            IChangeHospitalRequestGetAllPaginatedUseCase changeHospitalRequestGetAllPaginatedUseCase,
            IChangeHospitalRequestCreateUseCase changeHospitalRequestCreateUseCase)
        {
            _changeHospitalRequestChangeStateUseCase = changeHospitalRequestChangeStateUseCase;
            _changeHospitalRequestGetByIdUseCase = changeHospitalRequestGetByIdUseCase;
            _changeHospitalRequestGetAllPaginatedUseCase = changeHospitalRequestGetAllPaginatedUseCase;
            _changeHospitalRequestCreateUseCase = changeHospitalRequestCreateUseCase;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateRequest([FromBody] ChangeHospitalRequestCreateRequest request)
        {
            var result = await _changeHospitalRequestCreateUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("getAllPaginated/{HospitalId}")]
        [Authorize]
        public async Task<IActionResult> GetAllPaginated(int HospitalId, [FromQuery] int page, [FromQuery] int size)
        {
            var result = await _changeHospitalRequestGetAllPaginatedUseCase.Execute(page, size, HospitalId);
            return Ok(result);
        }

        [HttpGet("getById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _changeHospitalRequestGetByIdUseCase.Execute(id);
            return Ok(result);
        }

        [HttpPut("changeState")]
        [Authorize]
        public async Task<IActionResult> ChangeState([FromBody] ChangeHospitalRequestChangeStateRequest request)
        {
            var result = await _changeHospitalRequestChangeStateUseCase.Execute(request);
            return Ok(result);
        }
    }
}
