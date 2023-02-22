namespace Domain.Services.User
{
    public interface IUserService
    {
        Task RegistrationAndUpdateAsync(UserDto user, CancellationToken token);
        Task DeleteUserAsync(Guid userId, CancellationToken token);
    }
}
