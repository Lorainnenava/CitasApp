using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorGetAllPaginatedUseCase
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorGetAllPaginatedUseCase> _logger;

        public DoctorGetAllPaginatedUseCase(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<DoctorGetAllPaginatedUseCase> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<DoctorResponse>> Execute(int page, int size)
        {
            _logger.LogInformation("Iniciando la obtención de todos los doctores paginados.");

            var doctors = await _doctorRepository.GetAll(x => x.IsActive == true);

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

            var (items, totalCount) = await _doctorRepository.Pagination(page, size, x => x.IsOnVacation == false);

            var mappedItems = _mapper.Map<IEnumerable<DoctorResponse>>(items);

            _logger.LogInformation("Se obtuvieron {Count} doctores.", totalCount);

            return new PaginationResult<DoctorResponse>
            {
                RowsCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / size),
                PageSize = size,
                CurrentPage = page,
                Results = mappedItems
            };
        }
    }
}
