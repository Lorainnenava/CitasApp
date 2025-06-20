using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;

namespace MyApp.Application.UseCases.Hospitals
{
    public class HospitalGetAllPaginatedUseCase : IHospitalGetAllPaginatedUseCase
    {
        private readonly ILogger<HospitalGetAllPaginatedUseCase> _logger;
        public readonly IGenericRepository<HospitalsEntity> _hospitalRepository;

        public HospitalGetAllPaginatedUseCase(
            ILogger<HospitalGetAllPaginatedUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalRepository)
        {
            _logger = logger;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<PaginationResult<HospitalListResponse>> Execute(int Page, int Size)
        {
            _logger.LogInformation("Iniciando la obtención de todos los hospitales.");

            var (items, totalCount) = await _hospitalRepository.Pagination(
                Page, Size,
                null,
                x => x.Municipality
            );

            var mappedItems = items.Select(x => new HospitalListResponse
            {
                HospitalId = x.HospitalId,
                MunicipalityName = x.Municipality.Name,
                Name = x.Name,
                Address = x.Address,
                IsActive = x.IsActive,
            });

            _logger.LogInformation("Se obtuvieron {Count} hospitales.", totalCount);

            return new PaginationResult<HospitalListResponse>
            {
                RowsCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / Size),
                PageSize = Size,
                CurrentPage = Page,
                Results = mappedItems
            };
        }
    }
}
