namespace Domain.Services.Users
{
    public interface IUserService
    {
        Task RegistrationAndUpdateAsync(UserDto user, CancellationToken token);
        Task DeleteUserAsync(Guid userId, CancellationToken token);
        Task<UserDto> GetAsync (Guid userId, CancellationToken token);
        Task<UserEntryDto> GetAllAsync(CancellationToken token);
    }
}
