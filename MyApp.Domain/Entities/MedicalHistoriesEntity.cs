namespace MyApp.Domain.Entities
{
    public class MedicalHistoriesEntity
    {
        public int MedicalHistoryId { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int BloodTypeId { get; set; }
        public int MaritalStatusId { get; set; }
        public string Occupation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public UserAddressDetailsEntity UserAddressDetail { get; set; } = new();
        public EmergencyContactsEntity EmergencyContact { get; set; } = new();
        public MaritalStatusesEntity MaritalStatus { get; set; } = new();
        public MedicalConditionsEntity MedicalCondition { get; set; } = new();
        public BloodTypesEntity BloodType { get; set; } = new();
    }
}
