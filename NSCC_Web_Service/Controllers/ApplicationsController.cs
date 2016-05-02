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
    public class ApplicationsController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool ApplicationExists(int key)
        {
            return db.Applications.Any(c => c.ApplicationId == key);
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
        public IQueryable<Application> Get()
        {
            return db.Applications;
        }
        [EnableQuery]
        public SingleResult<Application> Get([FromODataUri] int key)
        {
            IQueryable<Application> result = db.Applications.Where(c => c.ApplicationId == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(Application Application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Applications.Add(Application);
            await db.SaveChangesAsync();
            return Created(Application);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Application> Application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Applications.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Application.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(key))
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
            var Application = await db.Applications.FindAsync(key);
            if (Application == null)
            {
                return NotFound();
            }
            db.Applications.Remove(Application);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}