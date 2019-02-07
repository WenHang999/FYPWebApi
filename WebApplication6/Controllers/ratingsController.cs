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
    public class ratingsController : ApiController
    {
        private fypmobileEntities db = new fypmobileEntities();

        // GET: api/ratings
        public IQueryable<rating> Getratings()
        {
            return db.ratings;
        }

        // GET: api/ratings/5
        [ResponseType(typeof(rating))]
        public async Task<IHttpActionResult> Getrating(int id)
        {
            rating rating = await db.ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/ratings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putrating(int id, rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.ratingID)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ratingExists(id))
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

        // POST: api/ratings
        [ResponseType(typeof(rating))]
        public async Task<IHttpActionResult> Postrating(rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ratings.Add(rating);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rating.ratingID }, rating);
        }

        // DELETE: api/ratings/5
        [ResponseType(typeof(rating))]
        public async Task<IHttpActionResult> Deleterating(int id)
        {
            rating rating = await db.ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.ratings.Remove(rating);
            await db.SaveChangesAsync();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ratingExists(int id)
        {
            return db.ratings.Count(e => e.ratingID == id) > 0;
        }
    }
}