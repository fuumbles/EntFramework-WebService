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
    public class ProgramChoicesController : ODataController
    {
        NSCC_DBContext db = new NSCC_DBContext();

        private bool ProgramChoiceExists(int key)
        {
            return db.ProgramChoices.Any(c => c.ProgramChoiceId == key);
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
        public IQueryable<ProgramChoice> Get()
        {
            return db.ProgramChoices;
        }
        [EnableQuery]
        public SingleResult<ProgramChoice> Get([FromODataUri] int key)
        {
            IQueryable<ProgramChoice> result = db.ProgramChoices.Where(c => c.ProgramChoiceId == key);
            return SingleResult.Create(result);
        }
        //----------------------------------------------//
        //POST
        public async Task<IHttpActionResult> Post(ProgramChoice ProgramChoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ProgramChoices.Add(ProgramChoice);
            await db.SaveChangesAsync();
            return Created(ProgramChoice);
        }
        //----------------------------------------------//
        //UPDATE
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ProgramChoice> ProgramChoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.ProgramChoices.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            ProgramChoice.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramChoiceExists(key))
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
            var ProgramChoice = await db.ProgramChoices.FindAsync(key);
            if (ProgramChoice == null)
            {
                return NotFound();
            }
            db.ProgramChoices.Remove(ProgramChoice);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        //----------------------------------------------//

    }
}