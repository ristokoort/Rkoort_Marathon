using System.ComponentModel.DataAnnotations;

namespace Rkoort_Marathon.Models
{
    public class RunnersMaster
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
