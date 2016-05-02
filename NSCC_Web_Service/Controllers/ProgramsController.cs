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
    public class ProgramsController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool ProgramExists(int key)
        {
            return db.Programs.Any(c => c.ProgramID == key);
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
        public IQueryable<Program> Get()
        {
            return db.Programs;
        }
        [EnableQuery]
        public SingleResult<Program> Get([FromODataUri] int key)
        {
            IQueryable<Program> result = db.Programs.Where(c => c.ProgramID == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public ICollection<Campus> GetCampuses([FromODataUri] int key)
        {
            var program = db.Programs.Find(key);
            return program.Campuses;
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(Program Program)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Programs.Add(Program);
            await db.SaveChangesAsync();
            return Created(Program);
        }
        //----------------------------------------------//
        //UPDATE



        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Program> Program)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Programs.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Program.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExists(key))
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
            var Program = await db.Programs.FindAsync(key);
            if (Program == null)
            {
                return NotFound();
            }
            db.Programs.Remove(Program);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}