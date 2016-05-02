using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFramework_Library
{
    public class DB_Context : DbContext
    {
        public DB_Context() : base("DB_str")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DB_Context>());
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<ProgramChoice> ProgramChoices { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProvinceState> ProvinceStates { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
    }
}
