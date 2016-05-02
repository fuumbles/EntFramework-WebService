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
    public class CitizenshipsController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool CitizenshipExists(int key)
        {
            return db.Citizenships.Any(c => c.CitizenshipId == key);
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
        public IQueryable<Citizenship> Get()
        {
            return db.Citizenships;
        }
        [EnableQuery]
        public SingleResult<Citizenship> Get([FromODataUri] int key)
        {
            IQueryable<Citizenship> result = db.Citizenships.Where(c => c.CitizenshipId == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(Citizenship Citizenship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Citizenships.Add(Citizenship);
            await db.SaveChangesAsync();
            return Created(Citizenship);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Citizenship> Citizenship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Citizenships.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Citizenship.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenshipExists(key))
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
            var Citizenship = await db.Citizenships.FindAsync(key);
            if (Citizenship == null)
            {
                return NotFound();
            }
            db.Citizenships.Remove(Citizenship);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}