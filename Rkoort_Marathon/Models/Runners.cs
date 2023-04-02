using System.ComponentModel.DataAnnotations;
using System;

namespace Rkoort_Marathon.Models
{
    public class Runner
    {
        [Key]
        public int id { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }

        public DateTime? Break1_time1 { get; set; }
        public DateTime? Break1_time2 { get; set; }
        public DateTime? Break2_time1 { get; set; }
        public DateTime? Break2_time2 { get; set; }
        public int Breaks { get; set; }

        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
    }
}
