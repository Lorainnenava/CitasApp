﻿using MyApp.Application.DTOs.Users;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Interfaces.UseCases.Users
{
    public interface IUserGetAllUseCase
    {
        Task<PaginationResult<UserResponse>> Execute(int page, int size);
    }
}
