using System.ComponentModel.DataAnnotations;

namespace Rkoort_Marathon.Models
{
    public class RunnersMaster
    {
        [Key]
        public int id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        

    }
}
