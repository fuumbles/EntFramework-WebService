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
    public class ProvinceStatesController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool ProvinceStateExists(string key)
        {
            return db.ProvinceStates.Any(c => c.ProvinceStateCode == key);
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
        public IQueryable<ProvinceState> Get()
        {
            return db.ProvinceStates;
        }
        [EnableQuery]
        public SingleResult<ProvinceState> Get([FromODataUri] string key)
        {
            IQueryable<ProvinceState> result = db.ProvinceStates.Where(c => c.ProvinceStateCode == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(ProvinceState ProvinceState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ProvinceStates.Add(ProvinceState);
            await db.SaveChangesAsync();
            return Created(ProvinceState);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<ProvinceState> ProvinceState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.ProvinceStates.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            ProvinceState.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceStateExists(key))
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
            var ProvinceState = await db.ProvinceStates.FindAsync(key);
            if (ProvinceState == null)
            {
                return NotFound();
            }
            db.ProvinceStates.Remove(ProvinceState);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}