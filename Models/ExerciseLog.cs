using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class ExerciseLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Exercise_ID { get; set; }

        public int Log_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Exercise_Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Exercise_Type { get; set; } = string.Empty;

        public int? Duration { get; set; }

        public int? Calories_Burned { get; set; }

        // Navigation property
        [ForeignKey("Log_ID")]
        public virtual DailyLog DailyLog { get; set; } = null!;
    }
}
