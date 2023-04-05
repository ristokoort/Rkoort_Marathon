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

        public DateTime? Break1 { get; set; }
        public DateTime? Break2 { get; set; }

        public int Breaks { get; set; }

        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
    }
}
