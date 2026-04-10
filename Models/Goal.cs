using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class Goal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Goal_ID { get; set; }

        public int User_ID { get; set; }

        [MaxLength(50)]
        public string? Goal_Type { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal? Target_Value { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal? Current_Value { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? Target_Date { get; set; }

        public bool Achieved { get; set; }

        // Navigation property
        [ForeignKey("User_ID")]
        public virtual User User { get; set; } = null!;
    }
}
