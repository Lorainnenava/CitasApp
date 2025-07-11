﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.Users;
using MyApp.Application.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.Users
{
    public class UserUpdateUseCaseTests
    {
        public readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        public readonly IMapper _mapper;
        private readonly UserUpdateUseCase _useCase;
        private readonly Mock<ILogger<UserUpdateUseCase>> _loggerMock;

        public UserUpdateUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserUpdateUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateRequest, UsersEntity>();
                cfg.CreateMap<UsersEntity, UserResponse>();
                cfg.CreateMap<UserUpdateRequest, UsersEntity>();
            });
            _mapper = config.CreateMapper();
            _useCase = new UserUpdateUseCase(_userRepositoryMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnUserUpdatedSuccessfully()
        {
            var userToUpdate = MockUser.MockOneUserEntityToUpdateRequest();
            var expectedUser = MockUser.MockOneUserEntityUpdated();
            var expectedUserEntity = MockUser.MockOneUserEntity();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(expectedUserEntity);

            _userRepositoryMock.Setup(x => x.Update(It.IsAny<UsersEntity>(), It.IsAny<UsersEntity>()))
                .ReturnsAsync(expectedUser);

            var result = await _useCase.Execute(1, userToUpdate);

            Assert.NotNull(result);
            Assert.Equal(expectedUser.FirstName, result.FirstName);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenUpdateFails()
        {
            var userToUpdate = MockUser.MockOneUserEntityToUpdateRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(1, userToUpdate));

            Assert.Contains("El usuario que intentas actualizar no existe o ha sido eliminado.", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            var userRequest = new UserUpdateRequest
            {
                Email = ""
            };

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _useCase.Execute(1, userRequest));
        }
    }
}
