using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Shared.DTOs;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        public readonly IHospitalCreateUseCase _hospitalCreateUseCase;
        public readonly IHospitalUpdateUseCase _hospitalUpdateUseCase;
        public readonly IHospitalGetByIdUseCase _hospitalGetByIdUseCase;
        public readonly IHospitalToogleIsActiveUseCase _hospitalToogleIsActiveUseCase;
        public readonly IHospitalGetAllPaginatedUseCase _hospitalGetAllPaginatedUseCase;

        public HospitalController(
            IHospitalCreateUseCase hospitalCreateUseCase,
            IHospitalUpdateUseCase hospitalUpdateUseCase,
            IHospitalGetByIdUseCase hospitalGetByIdUseCase,
            IHospitalToogleIsActiveUseCase hospitalToogleIsActiveUseCase,
            IHospitalGetAllPaginatedUseCase hospitalGetAllPaginatedUseCase)
        {
            _hospitalCreateUseCase = hospitalCreateUseCase;
            _hospitalUpdateUseCase = hospitalUpdateUseCase;
            _hospitalGetByIdUseCase = hospitalGetByIdUseCase;
            _hospitalToogleIsActiveUseCase = hospitalToogleIsActiveUseCase;
            _hospitalGetAllPaginatedUseCase = hospitalGetAllPaginatedUseCase;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] HospitalCreateRequest request)
        {
            var result = await _hospitalCreateUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("getAllPaginated")]
        [Authorize]
        public async Task<ActionResult<PaginationResult<HospitalResponse>>> GetAllPaginated(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _hospitalGetAllPaginatedUseCase.Execute(page, size);
            return Ok(result);
        }

        [HttpPut("toggleIsActive/{id}")]
        [Authorize]
        public async Task<IActionResult> ToggleIsActive(int Id)
        {
            var result = await _hospitalToogleIsActiveUseCase.Execute(Id);
            return Ok(result);
        }

        [HttpGet("getById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _hospitalGetByIdUseCase.Execute(Id);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int Id, [FromBody] HospitalUpdateRequest request)
        {
            var result = await _hospitalUpdateUseCase.Execute(Id, request);
            return Ok(result);
        }
    }
}
