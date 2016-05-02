using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using Framework_Lib;

namespace NSCC_DB_Service.Controllers
{
    public class AcademicYearsController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool AcademicYearExists(int key)
        {
            return db.AcademicYears.Any(c => c.AcademicYearID == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //----------------------------------------------//
        //CRUD
        //GET
        [EnableQuery]
        public IQueryable<AcademicYear> Get()
        {
            return db.AcademicYears;
        }
        [EnableQuery]
        public SingleResult<AcademicYear> Get([FromODataUri] int key)
        {
            IQueryable<AcademicYear> result = db.AcademicYears.Where(c => c.AcademicYearID == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(AcademicYear AcademicYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.AcademicYears.Add(AcademicYear);
            await db.SaveChangesAsync();
            return Created(AcademicYear);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<AcademicYear> AcademicYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.AcademicYears.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            AcademicYear.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicYearExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }
        //----------------------------------------------//
        //DELETE
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var AcademicYear = await db.AcademicYears.FindAsync(key);
            if (AcademicYear == null)
            {
                return NotFound();
            }
            db.AcademicYears.Remove(AcademicYear);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}