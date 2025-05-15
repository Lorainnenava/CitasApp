using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorUpdateUseCase
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorUpdateUseCase> _logger;

        public DoctorUpdateUseCase(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<DoctorUpdateUseCase> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorResponse> Execute(int DoctorId, DoctorRequest request)
        {
            _logger.LogInformation("Iniciando la actualización de un doctor con el ID:{DoctorId}", DoctorId);

            var doctors = await _doctorRepository.GetAll();
            var searchDoctor = doctors.FirstOrDefault(x => x.DoctorId == DoctorId);

            if (searchDoctor is null)
            {
                _logger.LogWarning("No se encontró ningún doctor con el ID: {DoctorId}", DoctorId);
                throw new NotFoundException("No se encontro el doctor solicitado");
            }

            var search = doctors.FirstOrDefault(x => x.LicenseNumber == request.LicenseNumber && x.DoctorId != DoctorId);

            if (search is not null)
            {
                _logger.LogWarning("Ya existe un doctor con este n° de licencia {LicenseNumber}.", request.LicenseNumber);
                throw new AlreadyExistsException("Ya existe un doctor con este mismo n° de licencia.");
            }

            var entityMapped = _mapper.Map<DoctorsEntity>(request);

            var doctorUpdated = await _doctorRepository.Update(x => x.DoctorId == DoctorId, searchDoctor, entityMapped);

            var response = _mapper.Map<DoctorResponse>(doctorUpdated);

            _logger.LogInformation("Doctor actualizado exitosamente con el ID: {DoctorId}", response.DoctorId);

            return response;
        }
    }
}
