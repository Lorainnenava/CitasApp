using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalHistoriesEntity
    {
        [Key]
        public int MedicalHistoryId { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int BloodTypeId { get; set; }
        public int MaritalStatusId { get; set; }
        public string Occupation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = null!;
        public UserAddressDetailsEntity? UserAddressDetail { get; set; } = null!;
        public EmergencyContactsEntity? EmergencyContact { get; set; } = null!;
        public MaritalStatusesEntity? MaritalStatus { get; set; } = null!;
        public MedicalConditionsEntity? MedicalCondition { get; set; } = null!;
        public BloodTypesEntity? BloodType { get; set; } = null!;
    }
}
