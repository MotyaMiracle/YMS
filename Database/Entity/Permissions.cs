using Microsoft.EntityFrameworkCore;

namespace Yard_Management_System.Entity
{
    public enum Permissions
    {
        /// <summary>
        /// Просмотр пользователей
        /// </summary>
        Read = 0,

        /// <summary>
        /// Создание пользователей
        /// </summary>
        Create = 1,

        /// <summary>
        /// Удаление пользователей
        /// </summary>
        Delete = 2,

        /// <summary>
        /// Редактирование информации о пользователях
        /// </summary>
        Update = 3
    }
}