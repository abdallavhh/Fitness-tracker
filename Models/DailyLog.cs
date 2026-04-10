using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class DailyLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Log_ID { get; set; }

        public int User_ID { get; set; }

        public DateTime? Log_Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Weight { get; set; }

        // Navigation properties
        [ForeignKey("User_ID")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<ExerciseLog> Exercises { get; set; } = new List<ExerciseLog>();
        public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
    }
}
