﻿using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class NotificationsEntity
    {
        [Key]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
