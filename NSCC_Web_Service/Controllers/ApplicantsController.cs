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
    public class ApplicantsController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool ApplicantExists(int key)
        {
            return db.Applicants.Any(c => c.ApplicantId == key);
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
        public IQueryable<Applicant> Get()
        {
            return db.Applicants;
        }
        [EnableQuery]
        public SingleResult<Applicant> Get([FromODataUri] int key)
        {
            IQueryable<Applicant> result = db.Applicants.Where(c => c.ApplicantId == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(Applicant Applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Applicants.Add(Applicant);
            await db.SaveChangesAsync();
            return Created(Applicant);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Applicant> Applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Applicants.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Applicant.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(key))
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
            var Applicant = await db.Applicants.FindAsync(key);
            if (Applicant == null)
            {
                return NotFound();
            }
            db.Applicants.Remove(Applicant);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}