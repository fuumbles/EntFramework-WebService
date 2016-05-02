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
    public class CountriesController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool CountryExists(string key)
        {
            return db.Countries.Any(c => c.CountryCode == key);
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
        public IQueryable<Country> Get()
        {
            return db.Countries;
        }
        [EnableQuery]
        public SingleResult<Country> Get([FromODataUri] string key)
        {
            IQueryable<Country> result = db.Countries.Where(c => c.CountryCode == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(Country Country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Countries.Add(Country);
            await db.SaveChangesAsync();
            return Created(Country);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Country> Country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Countries.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Country.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(key))
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
            var Country = await db.Countries.FindAsync(key);
            if (Country == null)
            {
                return NotFound();
            }
            db.Countries.Remove(Country);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}