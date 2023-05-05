namespace Domain.Enums
{
    public enum TripStatus
    {
        /// <summary>
        /// Создана
        /// </summary>
        Create = 0, 

        /// <summary>
        /// Подтверждена
        /// </summary>
        Confirmed = 1,

        /// <summary>
        /// В пути
        /// </summary>
        Arrived = 2, 

        /// <summary>
        /// Прибыл на склад
        /// </summary>
        ArriveAtStorage = 3,

        /// <summary>
        /// Убыл
        /// </summary>
        Left = 4,

        /// <summary>
        /// В архиве
        /// </summary>
        InArchive = 5
    }
}
