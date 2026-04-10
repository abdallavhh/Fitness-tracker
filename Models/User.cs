using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string User_Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        // Navigation properties
        public virtual UserProfile? Profile { get; set; }
        public virtual ICollection<DailyLog> DailyLogs { get; set; } = new List<DailyLog>();
        public virtual ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();
        public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}
