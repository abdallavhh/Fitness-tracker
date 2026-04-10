using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Meal_ID { get; set; }

        public int Log_ID { get; set; }

        [MaxLength(50)]
        public string? Meal_Name { get; set; }

        [MaxLength(50)]
        public string? Meal_Type { get; set; }

        // Navigation properties
        [ForeignKey("Log_ID")]
        public virtual DailyLog DailyLog { get; set; } = null!;

        public virtual ICollection<MealItem> MealItems { get; set; } = new List<MealItem>();
    }
}
