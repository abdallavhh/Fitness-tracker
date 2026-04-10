using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class HealthMetric
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Metric_ID { get; set; }

        public int User_ID { get; set; }

        public DateTime? Metric_Date { get; set; }

        public int? Heart_Rate { get; set; }

        public int? Blood_Sugar { get; set; }

        [MaxLength(20)]
        public string? Blood_Pressure { get; set; }

        public int? Total_Water { get; set; }

        // Navigation property
        [ForeignKey("User_ID")]
        public virtual User User { get; set; } = null!;
    }
}
