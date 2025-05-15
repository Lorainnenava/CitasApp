namespace MyApp.Application.Interfaces.UseCases.Users
{
    public interface IUserDeleteUseCase
    {
        Task<bool> Execute(int Id);
    }
}
