using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    public class NSCC_DBContext : DbContext
    {
        
        public NSCC_DBContext() : base("NSCC_DB")
        {
            SqlConnection.ClearAllPools();
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<NSCC_DBContext>());

            TestEntry();
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


        public void TestEntry()
        {
            
        }
    }
}
