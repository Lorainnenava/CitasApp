using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class HospitalSchedulesEntity
    {
        [Key]
        public int HospitalScheduleId { get; set; }
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public int AppointmentDurationMinutes { get; set; } = 20;

        // Relaciones
        public HospitalsEntity Hospital { get; set; } = null!;
        public List<HospitalScheduleDetailsEntity> ScheduleDetails { get; set; } = null!;
    }
}
