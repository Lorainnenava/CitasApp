using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.ChangeHospitalRequests
{
    public class ChangeHospitalRequestGetAllPaginatedUseCase : IChangeHospitalRequestGetAllPaginatedUseCase
    {
        private readonly IGenericRepository<ChangeHospitalRequestsEntity> _changeHospitalRequestRepository;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;
        private readonly ILogger<ChangeHospitalRequestGetAllPaginatedUseCase> _logger;

        public ChangeHospitalRequestGetAllPaginatedUseCase(
            IGenericRepository<ChangeHospitalRequestsEntity> changeHospitalRequestRepository,
            ILogger<ChangeHospitalRequestGetAllPaginatedUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalRepository)
        {
            _logger = logger;
            _changeHospitalRequestRepository = changeHospitalRequestRepository;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<PaginationResult<ChangeHospitalRequestListResponse>> Execute(int Page, int Size, int HospitalId)
        {
            _logger.LogInformation("Iniciando la obtención de todos los usuarios del hospital con el HospitalId {HospitalId}.", HospitalId);

            var hospitalExistes = await _hospitalRepository.GetByCondition(x => x.HospitalId == HospitalId);

            if (hospitalExistes is null)
            {
                _logger.LogWarning("No se encontró el hospital con ID: {HospitalId}", HospitalId);
                throw new NotFoundException($"El hospital con el HospitalId '{HospitalId}' no existe.");
            }

            var (items, totalCount) = await _changeHospitalRequestRepository.Pagination(
                Page, Size,
                x => x.CurrentHospitalId == HospitalId,
                x => x.CurrentHospital,
                x => x.NewHospital,
                x => x.User,
                x => x.Status
            );

            var mappedItems = items.Select(x => new ChangeHospitalRequestListResponse
            {
                ChangeHospitalRequestId = x.ChangeHospitalRequestId,
                UserName = $"{x.User.FirstName} {x.User.LastName}",
                IdentificationNumber = x.User.IdentificationNumber,
                CurrentHospitalName = x.CurrentHospital.Name,
                NewHospitalName = x.NewHospital.Name,
                StatusName = x.Status.Name,
                CreatedAt = x.CreatedAt
            });

            _logger.LogInformation("Se obtuvieron {Count} usuarios.", totalCount);

            return new PaginationResult<ChangeHospitalRequestListResponse>
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
