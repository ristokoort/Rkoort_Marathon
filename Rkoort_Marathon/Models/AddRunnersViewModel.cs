using System.Collections;
using System.Collections.Generic;

namespace Rkoort_Marathon.Models
{
    public class AddRunnersViewModel
    {
        public List<RunnersMaster> RegisteredRunners{ get; set; }

        public RunnersMaster NewRunner { get; set; }
    }
}
