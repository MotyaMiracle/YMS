using AutoMapper;
using Domain.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Application.Services.Users
{
    public class UserProvider : IUserProvider
    {
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationContext _db;


        public UserProvider(IHttpContextAccessor httpContextAccessor, ApplicationContext dbContext, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            _db = dbContext;
            this.mapper = mapper;
        }

        public Guid? GetCurrentUserId()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var userIdClaim = httpContext.User.FindFirst("Id");
            return userIdClaim != null
                ? Guid.Parse(userIdClaim.Value)
                : (Guid?)null;
        }

        public Guid GetCurrentUser()
        {
            User user = _db.Users.FirstOrDefault(x => x.Id == EnsureCurrentUserId());

            return GetCurrentUserInner(user);
        }

        public Guid EnsureCurrentUserId()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                throw new UnauthorizedAccessException("Невозможно определить текущего пользователя");

            return userId.Value;
        }

        private string GetCurrentUserLanguage()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var langClaim = httpContext.User.FindFirst("lang");
            string lang = langClaim?.Value ?? "ru";
            return lang;
        }

        /// <inheritdoc/>
        public string GetCurrentUserAuthHashFromToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var claim = httpContext.User.FindFirst("AuthHash");

            return claim?.Value;
        }

        string IUserProvider.GetCurrentUserLanguage()
        {
            return GetCurrentUserLanguage();
        }

        public async Task<Guid> GetCurrentUserAsync(CancellationToken token)
        {
            User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == EnsureCurrentUserId(), token);

            return GetCurrentUserInner(user);
        }

        private Guid GetCurrentUserInner(User user)
        {
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            return user.Id;
        }
    }
}
