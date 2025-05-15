using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class NotificationsEntity
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Message { get; set; } = string.Empty;
        [Required]
        public bool Read { get; set; } = false;
        [Required]
        public DateTime SentAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
