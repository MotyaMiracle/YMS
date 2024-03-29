﻿using Domain.Entity;

namespace Domain.Entity
{
    public class HistoryEntry
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public Guid EntityId { get; set; }
    }
}
