namespace MyApp.Domain.Entities
{
    public class UserAddressDetailsEntity
    {
        public int AddressId { get; set; }
        public int MedicalHistoryId { get; set; }
        public int MunicipalityId { get; set; }
        public string Address { get; set; } = string.Empty;
        public int ResidentsCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; } = new();
        public MunicipalitiesEntity Municipality { get; set; } = new();
    }
}
