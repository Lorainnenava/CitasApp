using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.HospitalSchedules;
using MyApp.Application.Interfaces.UseCases.HospitalSchedules;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalScheduleController : ControllerBase
    {
        public readonly IHospitalScheduleGetByIdUseCase _hospitalScheduleGetByIdUseCase;
        public readonly IHospitalUpdateUseCase _hospitalScheduleUpdateUseCase;

        public HospitalScheduleController(
            IHospitalScheduleGetByIdUseCase hospitalScheduleGetByIdUseCase,
            IHospitalUpdateUseCase hospitalScheduleUpdateUseCase)
        {
            _hospitalScheduleGetByIdUseCase = hospitalScheduleGetByIdUseCase;
            _hospitalScheduleUpdateUseCase = hospitalScheduleUpdateUseCase;
        }

        [HttpGet("getById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _hospitalScheduleGetByIdUseCase.Execute(Id);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int Id, [FromBody] HospitalScheduleRequest request)
        {
            var result = await _hospitalScheduleUpdateUseCase.Execute(Id, request);
            return Ok(result);
        }
    }
}
