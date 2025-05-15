using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorGetByIdUseCase
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorGetByIdUseCase> _logger;

        public DoctorGetByIdUseCase(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<DoctorGetByIdUseCase> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorResponse> Execute(int DoctorId)
        {
            _logger.LogInformation("Iniciando la busqueda de un doctor con el ID: {DoctorId}", DoctorId);

            var doctorExisted = await _doctorRepository.GetByCondition(x => x.DoctorId == DoctorId);

            if (doctorExisted is null)
            {
                _logger.LogWarning("No se encontró ningún doctor con el ID: {DoctorId}", DoctorId);
                throw new NotFoundException("No se encontro el doctor solicitado");
            }

            var response = _mapper.Map<DoctorResponse>(doctorExisted);

            _logger.LogInformation("Doctor con el ID: {DoctorId} encontrado exitosamente", response.DoctorId);

            return response;
        }
    }
}
