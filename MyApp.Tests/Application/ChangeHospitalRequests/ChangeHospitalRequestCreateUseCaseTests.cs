using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Application.UseCases.ChangeHospitalRequests;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.ChangeHospitalRequests
{
    public class ChangeHospitalRequestCreateUseCaseTests
    {
        public readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        public readonly IMapper _mapper;
        private readonly ChangeHospitalRequestCreateUseCase _useCase;
        private readonly Mock<ILogger<ChangeHospitalRequestCreateUseCase>> _loggerMock;
        private readonly Mock<IGenericRepository<ChangeHospitalRequestsEntity>> _changeHospitalRequestRepositoryMock;
        private readonly Mock<IGenericRepository<HospitalsEntity>> _hospitalRepositoryMock;

        public ChangeHospitalRequestCreateUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<ChangeHospitalRequestCreateUseCase>>();
            _hospitalRepositoryMock = new Mock<IGenericRepository<HospitalsEntity>>();
            _changeHospitalRequestRepositoryMock = new Mock<IGenericRepository<ChangeHospitalRequestsEntity>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ChangeHospitalRequestsEntity, ChangeHospitalRequestsEntity>();
                cfg.CreateMap<ChangeHospitalRequestsEntity, ChangeHospitalRequestResponse>();
            });
            _mapper = config.CreateMapper();

            _useCase = new ChangeHospitalRequestCreateUseCase(
                _userRepositoryMock.Object,
                _mapper,
                _loggerMock.Object,
                _hospitalRepositoryMock.Object,
                _changeHospitalRequestRepositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCreateChangeHospitalRequestSuccessfully()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestCreateRequest();
            var response = MockChangeHospitalRequest.MockChangeHospitalRequestEntity();
            var userEntity = MockUser.MockOneUserEntity();
            var currentHospital = MockHospital.MockHospitalEntity();
            var newHospital = MockHospital.MockHospitalEntityTwo();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync(currentHospital);

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync(newHospital);

            _changeHospitalRequestRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<ChangeHospitalRequestsEntity>()))
                .ReturnsAsync(response);

            var result = await _useCase.Execute(request);

            Assert.NotNull(result);
            Assert.Equal(request.UserId, result.UserId);
            Assert.Equal(request.CurrentHospitalId, result.CurrentHospitalId);
            Assert.Equal(request.NewHospitalId, result.NewHospitalId);
            Assert.Equal(request.ReasonForChange, result.ReasonForChange);
        }

        [Fact]
        public async Task Execute_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestCreateRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(request));

            Assert.Equal("No se encontró al usuario para registrar la solicitud.", ex.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowNotFoundException_WhenCurrentHospitalDoesNotExist()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestCreateRequest();
            var userEntity = MockUser.MockOneUserEntity();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync((HospitalsEntity)null!);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(request));

            Assert.Equal("No se encontró el hospital actual para registrar la solicitud.", ex.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowNotFoundException_WhenNewHospitalDoesNotExist()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestCreateRequest();
            var userEntity = MockUser.MockOneUserEntity();
            var currentHospital = MockHospital.MockHospitalEntity();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync(currentHospital);

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync((HospitalsEntity)null!);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(request));

            Assert.Equal("No se encontró el nuevo hospital para registrar la solicitud.", ex.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowAlreadyExistsException_WhenRequestAlreadyExists()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestCreateRequest();
            var existingRequest = MockChangeHospitalRequest.MockChangeHospitalRequestEntity();

            _changeHospitalRequestRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<ChangeHospitalRequestsEntity, bool>>>()))
                .ReturnsAsync(existingRequest);

            var ex = await Assert.ThrowsAsync<AlreadyExistsException>(() => _useCase.Execute(request));

            Assert.Equal("Ya existe una solicitud de cambio de hospital para el usuario.", ex.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowAlreadyExistsException_WhenTheTwoHospitalAreEquals()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestCreateRequestDouble();

            var ex = await Assert.ThrowsAsync<ConflictException>(() => _useCase.Execute(request));

            Assert.Equal("El hospital actual y el nuevo hospital no pueden ser el mismo.", ex.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            var request = MockChangeHospitalRequest.MockChangeHospitalRequestEntityInvalid();

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _useCase.Execute(request));
        }
    }
}
