using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TripStatus
    {
        /// <summary>
        /// Создана
        /// </summary>
        Create = 0, 

        /// <summary>
        /// В пути
        /// </summary>
        Arrived = 1, 

        /// <summary>
        /// В архиве
        /// </summary>
        InArchive = 2
    }
}
