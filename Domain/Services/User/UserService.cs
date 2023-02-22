using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.User
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
                UserEntity newUser = _mapper.Map<UserEntity>(user);
                newUser.Id = Guid.NewGuid();
                await _db.Users.AddAsync(newUser, token);
            }
            //Update
            else 
            {
                UserEntity updateUser = _mapper.Map<UserEntity>(user);
                _db.Users.Update(updateUser);
            }
            await _db.SaveChangesAsync(token);
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken token)
        {
            UserEntity user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync(token);
        }
    }
}
