using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalHistoriesEntity
    {
        [Key]
        public int MedicalHistoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public int BloodTypeId { get; set; }
        [Required]
        public int MaritalStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Occupation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; }
        public MaritalStatusesEntity MaritalStatus { get; set; }
        public MedicalConditionsEntity MedicalCondition { get; set; }
        public BloodTypesEntity BloodType { get; set; }
    }
}
