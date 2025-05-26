namespace MyApp.Domain.Entities
{
    public class HospitalSchedulesEntity
    {
        public int HospitalScheduleId { get; set; }
        public int HospitalId { get; set; }
        public int AppointmentDurationMinutes { get; set; } = 20;

        // Relaciones
        public HospitalsEntity Hospital { get; set; } = new();
        public List<HospitalScheduleDetailsEntity> ScheduleDetails { get; set; } = [];
    }
}
