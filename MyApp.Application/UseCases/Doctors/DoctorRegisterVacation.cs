using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorRegisterVacation
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorRegisterVacation> _logger;

        public DoctorRegisterVacation(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<DoctorRegisterVacation> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task<DoctorResponse> Execute(int DoctorId, DoctorRegisterVacationRequest request)
        {
            _logger.LogInformation("Iniciando el registro de vacaciones de un doctor con el ID: {DoctorId}", DoctorId);

            var registerVaction = await _doctorRepository.GetByCondition(x => x.DoctorId == DoctorId);

            if (registerVaction is null)
            {
                _logger.LogWarning("No se encontró ningún doctor con el ID: {DoctorId}", DoctorId);
                throw new NotFoundException("No se encontro el doctor solicitado");
            }

            var today = DateTime.Today;
            var minStartDate = today.AddDays(15);

            if (request.StartDateVacation < minStartDate)
            {
                _logger.LogWarning("La fecha de inicio de vacaciones debe ser al menos 15 días después de hoy. Fecha enviada: {Fecha}", request.StartDateVacation);
                throw new ValidationException("La fecha de inicio de vacaciones se debe registrar con al menos 15 dìas de anticipación.");
            }

            var entityMapped = _mapper.Map<DoctorsEntity>(request);

            var updateDoctor = await _doctorRepository.Update(x => x.DoctorId == DoctorId, registerVaction, entityMapped);

            var response = _mapper.Map<DoctorResponse>(updateDoctor);

            return response;
        }
    }
}
