namespace MyApp.Domain.Entities
{
    public class EmergencyContactsEntity
    {
        public int EmergencyContactId { get; set; }
        public int MedicalHistoryId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int RelationshipId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; } = new();
        public RelationShipsEntity RelationShip { get; set; } = new();
    }
}
