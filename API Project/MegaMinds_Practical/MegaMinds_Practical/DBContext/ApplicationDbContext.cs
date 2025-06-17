using MegaMinds_Practical.Model;
using System.Collections.Generic;
using MegaMinds_Practical.Model;
using Microsoft.EntityFrameworkCore;

namespace MegaMinds_Practical.DbContextDBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ObservationsModel> Observations { get; set; }
        public DbSet<ObservationDataModel> ObservationData { get; set; }
        public DbSet<ObservationPropertiesModel> ObservationProperties { get; set; }

    }
}
