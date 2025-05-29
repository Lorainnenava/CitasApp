namespace MyApp.Domain.Entities
{
    public class HospitalsEntity
    {
        public int HospitalId { get; set; }
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relaciones
        public MunicipalitiesEntity Municipality { get; set; } = new();
        public ICollection<UsersEntity> Users { get; set; } = new List<UsersEntity>();
        public HospitalSchedulesEntity HospitalSchedule { get; set; } = new();
    }
}