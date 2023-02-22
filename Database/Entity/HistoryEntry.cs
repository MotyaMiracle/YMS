using Yard_Management_System.Entity;

namespace Database.Entity
{
    public class HistoryEntry
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime Date { get; set; }
        public Guid EntityId { get; set; }
    }
}
