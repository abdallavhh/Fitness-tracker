using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class MealItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Meal_Item_ID { get; set; }

        public int Meal_ID { get; set; }

        [MaxLength(50)]
        public string? Food_Name { get; set; }

        public int? Quantity { get; set; }

        public int? Calories { get; set; }

        public int? Protein { get; set; }

        public int? Fat { get; set; }

        public int? Carbs { get; set; }

        // Navigation property
        [ForeignKey("Meal_ID")]
        public virtual Meal Meal { get; set; } = null!;
    }
}
