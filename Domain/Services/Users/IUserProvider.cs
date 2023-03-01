using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Users
{
    public interface IUserProvider
    {
        Guid? GetCurrentUserId();
        Guid GetCurrentUser();
        Task<Guid> GetCurrentUserAsync(CancellationToken token);
        string GetCurrentUserLanguage();

        /// <summary>
        /// Хеш ключевых полей из токена, для проверки актуальности токена авторизации
        /// </summary>
        public string GetCurrentUserAuthHashFromToken();
    }
}
