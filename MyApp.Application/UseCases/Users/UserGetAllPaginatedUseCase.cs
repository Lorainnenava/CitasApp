using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserGetAllPaginatedUseCase : IUserGetAllPaginatedUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;
        private readonly ILogger<UserGetAllPaginatedUseCase> _logger;

        public UserGetAllPaginatedUseCase(
            IGenericRepository<UsersEntity> userRepository,
            ILogger<UserGetAllPaginatedUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<PaginationResult<UserListResponse>> Execute(int Page, int Size, int HospitalId)
        {
            _logger.LogInformation("Iniciando la obtención de todos los usuarios del hospital con el ID: {HospitalId}.", HospitalId);

            var hospitalExistes = await _hospitalRepository.GetByCondition(x => x.HospitalId == HospitalId);

            if (hospitalExistes is null)
            {
                _logger.LogWarning("No se encontró el hospital con ID: {HospitalId}", HospitalId);
                throw new NotFoundException("No se encontró un hospital registrado con ese identificador.");
            }

            var (items, totalCount) = await _userRepository.Pagination(
                Page, Size,
                x => x.HospitalId == HospitalId,
                x => x.IdentificationNumber,
                x => x.Hospital,
                x => x.Gender);

            var mappedItems = items.Select(x => new UserListResponse
            {
                UserId = x.UserId,
                HospitalName = x.Hospital.Name,
                FullName = $"{x.FirstName} {x.LastName}",
                IdentificationNumber = x.IdentificationNumber,
                IdentificationTypeName = x.IdentificationType.Name,
                GenderName = x.Gender.Name,
                RoleName = x.Role.Name
            });

            _logger.LogInformation("Se obtuvieron {Count} usuarios.", totalCount);

            return new PaginationResult<UserListResponse>
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
