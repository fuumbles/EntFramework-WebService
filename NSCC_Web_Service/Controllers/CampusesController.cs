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
    public class CampusesController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();
         
        private bool CampusExists(int key)
        {
            return db.Campuses.Any(c => c.CampusId == key);
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
        public IQueryable<Campus> Get()
        {
            return db.Campuses;
        }
        [EnableQuery]
        public SingleResult<Campus> Get([FromODataUri] int key)
        {
            IQueryable<Campus> result = db.Campuses.Where(c => c.CampusId == key);
            return SingleResult.Create(result);
        }
        
        public async Task<IHttpActionResult> Post(Campus campus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Campuses.Add(campus);
            await db.SaveChangesAsync();
            return Created(campus);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Campus> campus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Campuses.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            campus.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampusExists(key))
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
            var campus = await db.Campuses.FindAsync(key);
            if (campus == null)
            {
                return NotFound();
            }
            db.Campuses.Remove(campus);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}