using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Profile_ID { get; set; }

        public int User_ID { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Height { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Weight { get; set; }

        [Required]
        public DateTime Date_of_Birth { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Activity_Level { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Medical_Condition { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Target_Weight { get; set; }

        // Navigation property
        [ForeignKey("User_ID")]
        public virtual User User { get; set; } = null!;
    }
}
