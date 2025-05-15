using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorChangeStatusUseCase
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorChangeStatusUseCase> _logger;

        public DoctorChangeStatusUseCase(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<DoctorChangeStatusUseCase> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorResponse> Execute(int DoctorId)
        {
            _logger.LogInformation("Iniciando el cambio de estado de un doctor con el ID: {DoctorId}", DoctorId);

            var doctorExisted = await _doctorRepository.GetByCondition(x => x.DoctorId == DoctorId);

            if (doctorExisted is null)
            {
                _logger.LogWarning("No se encontró ningún doctor con el ID: {DoctorId}", DoctorId);
                throw new NotFoundException("No se encontro el doctor solicitado");
            }

            doctorExisted.IsActive = !doctorExisted.IsActive;

            var doctorCreated = await _doctorRepository.Update(x => x.DoctorId == DoctorId, doctorExisted, doctorExisted);

            var response = _mapper.Map<DoctorResponse>(doctorCreated);

            _logger.LogInformation("Doctor actualizado exitosamente con el ID: {DoctorId}", doctorCreated.DoctorId);

            return response;
        }
    }
}
