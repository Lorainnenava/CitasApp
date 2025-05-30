using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class ConsumptionHabitsEntity
    {
        [Key]
        public int ConsumptionHabitId { get; set; }
        public int MedicalHistoryId { get; set; }
        public bool ConsumesAlcohol { get; set; }
        public bool ConsumesTobacco { get; set; }
        public bool ConsumesDrugs { get; set; }
        public bool ConsumesCaffeine { get; set; }
        public bool ConsumesOtherSubstances { get; set; }
        public bool Exercises { get; set; }
        public int ExerciseDaysPerWeek { get; set; }
        public int AverageSleepHours { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; } = null!;
    }
}
