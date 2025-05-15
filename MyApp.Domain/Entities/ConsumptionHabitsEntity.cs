using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class ConsumptionHabitsEntity
    {
        [Key]
        public int ConsumptionHabitId { get; set; }
        [Required]
        public int MedicalHistoryId { get; set; }
        [Required]
        public bool ConsumesAlcohol { get; set; }
        [Required]
        public bool ConsumesTobacco { get; set; }
        [Required]
        public bool ConsumesDrugs { get; set; }
        [Required]
        public bool ConsumesCaffeine { get; set; }
        [Required]
        public bool ConsumesOtherSubstances { get; set; }
        [Required]
        public bool Exercises { get; set; }
        [Required]
        public int ExerciseDaysPerWeek { get; set; }
        [Required]
        public int AverageSleepHours { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; } = new();
    }
}
