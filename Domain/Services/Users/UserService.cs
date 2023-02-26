using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;


        public UserService(ApplicationContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db= db;
        }

        public async Task RegistrationAndUpdateAsync(UserDto user,CancellationToken token)
        {
            //Create
            if (user.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            {
                User newUser = _mapper.Map<User>(user);
                newUser.Id = Guid.NewGuid();
                await _db.Users.AddAsync(newUser, token);
            }
            //Update
            else 
            {
                User updateUser = _mapper.Map<User>(user);
                _db.Users.Update(updateUser);
            }
            await _db.SaveChangesAsync(token);
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken token)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync(token);
        }

        public async Task<UserDto> GetAsync(Guid userId, CancellationToken token)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId, token);
            var response = _mapper.Map<UserDto>(user);
            return(response);
        }

        public async Task<UserEntryDto> GetAllAsync(CancellationToken token)
        {
            var users = await _db.Users
                .Include(u => u.Role)
                .ToListAsync(token);
            return new UserEntryDto
            {
                Users = users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password,
                    PasswordHash = u.PasswordHash,
                    IsActive = u.IsActive,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    RoleId = u.RoleId,
                    Role = u.Role

                }).ToList()
            };
        }
    }
}
