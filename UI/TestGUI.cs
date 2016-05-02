using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework_Lib;

namespace UI
{
    public class TestGUI
    {
        static void Main(string[] args)
        {
            using (var db = new NSCC_DBContext())
            {
                Applicant nApp = new Applicant();
                nApp.FirstName = "TestBil4356ly";
                nApp.MiddleName = "TestBilly";
                nApp.LastName = "Billy";
                nApp.DOB = DateTime.Now;
                nApp.Gender = "M";
                nApp.StreetAddress1 = "Fakestreet 123";
                nApp.City = "Here";
                nApp.CountryCode = "CA";
                nApp.HomePhone = "1234567890";
                nApp.Email = "123@123456.789";
                nApp.Citizenship = 1;
                nApp.Password = "Fuckyourface";
                nApp.CellPhone = "Fkin Cell";
                nApp.WorkPhone = "fkin phones";


                db.SaveChanges();
                //var query = from a in db.Applicants
                //            select a;
                //foreach (var item in query)
                //{
                //    Console.WriteLine(item.CountryCode);
                //}
            }

            Console.WriteLine("Complete.");
            Console.ReadKey();

            
        }
    }
}
