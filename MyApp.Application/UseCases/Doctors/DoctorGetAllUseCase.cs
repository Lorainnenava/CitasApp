using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorGetAllUseCase
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorGetAllUseCase> _logger;

        public DoctorGetAllUseCase(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<DoctorGetAllUseCase> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorResponse>> Execute(int? SpecialtyId)
        {
            _logger.LogInformation("Iniciando la obtención de todos los doctores con el specialtyId: {SpecialtyId}", SpecialtyId);

            var doctors = await _doctorRepository.GetAll(x => x.IsActive == true && (!SpecialtyId.HasValue || x.SpecialtyId == SpecialtyId.Value));

            var today = DateTime.Today;

            foreach (var doctor in doctors)
            {
                if (doctor.StartDateVacation.HasValue && doctor.StartDateVacation <= today && doctor.EndDateVacation.HasValue && doctor.EndDateVacation >= today)
                {
                    if (!doctor.IsOnVacation)
                    {
                        doctor.IsOnVacation = true;
                        await _doctorRepository.Update(x => x.DoctorId == doctor.DoctorId, doctor, doctor);
                    }
                }
                else
                {
                    if (doctor.IsOnVacation)
                    {
                        doctor.IsOnVacation = false;
                        await _doctorRepository.Update(x => x.DoctorId == doctor.DoctorId, doctor, doctor);
                    }
                }
            }

            var availableDoctors = doctors.Where(x => x.IsOnVacation == false);

            var response = _mapper.Map<IEnumerable<DoctorResponse>>(availableDoctors);

            _logger.LogInformation("Se obtuvieron {Count} doctores.", response.Count());

            return response;
        }
    }
}
