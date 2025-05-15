using MyApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class EmergencyContactsEntity
    {
        [Key]
        public int EmergencyContactId { get; set; }
        [Required]
        public int MedicalHistoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public RelationshipType Relationship { get; set; }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; }
    }
}
