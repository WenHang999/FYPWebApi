using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ballotResultsController : ApiController
    {
        private fypmobileEntities db = new fypmobileEntities();

        // GET: api/ballotResults
        public IQueryable<ballotResult> GetballotResults()
        {
            return db.ballotResults;
        }

        // GET: api/ballotResults/5
        [ResponseType(typeof(ballotResult))]
        public async Task<IHttpActionResult> GetballotResult(int id)
        {
            ballotResult ballotResult = await db.ballotResults.FindAsync(id);
            if (ballotResult == null)
            {
                return NotFound();
            }

            return Ok(ballotResult);
        }

        // PUT: api/ballotResults/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutballotResult(int id, ballotResult ballotResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ballotResult.ballotID)
            {
                return BadRequest();
            }

            db.Entry(ballotResult).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ballotResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ballotResults
        [ResponseType(typeof(ballotResult))]
        public async Task<IHttpActionResult> PostballotResult(ballotResult ballotResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ballotResults.Add(ballotResult);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ballotResult.ballotID }, ballotResult);
        }

        // DELETE: api/ballotResults/5
        [ResponseType(typeof(ballotResult))]
        public async Task<IHttpActionResult> DeleteballotResult(int id)
        {
            ballotResult ballotResult = await db.ballotResults.FindAsync(id);
            if (ballotResult == null)
            {
                return NotFound();
            }

            db.ballotResults.Remove(ballotResult);
            await db.SaveChangesAsync();

            return Ok(ballotResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ballotResultExists(int id)
        {
            return db.ballotResults.Count(e => e.ballotID == id) > 0;
        }
    }
}