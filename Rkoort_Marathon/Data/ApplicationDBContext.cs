using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rkoort_Marathon.Models;

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext (DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

    public DbSet<Runners> runners { get; set; }
    public DbSet<RunnersMaster> RunnersMaster { get; set; }
    public DbSet<Rkoort_Marathon.Models.Award> Award { get; set; }
}
