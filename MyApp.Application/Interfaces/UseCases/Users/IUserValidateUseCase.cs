namespace MyApp.Application.Interfaces.UseCases.Users
{
    public interface IUserValidateUseCase
    {
        Task<bool> Execute(string code);
    }
}
