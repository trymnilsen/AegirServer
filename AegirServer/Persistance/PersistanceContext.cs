using AegirDataTypes.Vessel;
using AegirDataTypes.Workspace;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Persistence
{
    class PersistanceContext : DbContext
    {
        public DbSet<VesselConfiguration> Vessels { get; set; }
        public DbSet<ProjectData> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database does not pluralize table names
            modelBuilder.Conventions
                .Remove<PluralizingTableNameConvention>();
        }
    }
}
