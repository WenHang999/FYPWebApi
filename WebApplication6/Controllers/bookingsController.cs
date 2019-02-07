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
    public class bookingsController : ApiController
    {
        private fypmobileEntities db = new fypmobileEntities();

        // GET: api/bookings
        public IQueryable<booking> Getbookings()
        {
            return db.bookings;
        }

        // GET: api/bookings/5
        [ResponseType(typeof(booking))]
        public async Task<IHttpActionResult> Getbooking(int id)
        {
            booking booking = await db.bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/bookings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putbooking(int id, booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.bookingID)
            {
                return BadRequest();
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookingExists(id))
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

        // POST: api/bookings
        [ResponseType(typeof(booking))]
        public async Task<IHttpActionResult> Postbooking(booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bookings.Add(booking);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = booking.bookingID }, booking);
        }

        // DELETE: api/bookings/5
        [ResponseType(typeof(booking))]
        public async Task<IHttpActionResult> Deletebooking(int id)
        {
            booking booking = await db.bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.bookings.Remove(booking);
            await db.SaveChangesAsync();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookingExists(int id)
        {
            return db.bookings.Count(e => e.bookingID == id) > 0;
        }
    }
}