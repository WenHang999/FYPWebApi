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
    public class eventTicketsController : ApiController
    {
        private fypmobileEntities db = new fypmobileEntities();

        // GET: api/eventTickets
        public IQueryable<eventTicket> GeteventTickets()
        {
            return db.eventTickets;
        }

        // GET: api/eventTickets/5
        [ResponseType(typeof(eventTicket))]
        public async Task<IHttpActionResult> GeteventTicket(int id)
        {
            eventTicket eventTicket = await db.eventTickets.FindAsync(id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            return Ok(eventTicket);
        }

        // PUT: api/eventTickets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuteventTicket(int id, eventTicket eventTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventTicket.eventCode)
            {
                return BadRequest();
            }

            db.Entry(eventTicket).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eventTicketExists(id))
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

        // POST: api/eventTickets
        [ResponseType(typeof(eventTicket))]
        public async Task<IHttpActionResult> PosteventTicket(eventTicket eventTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.eventTickets.Add(eventTicket);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = eventTicket.eventCode }, eventTicket);
        }

        // DELETE: api/eventTickets/5
        [ResponseType(typeof(eventTicket))]
        public async Task<IHttpActionResult> DeleteeventTicket(int id)
        {
            eventTicket eventTicket = await db.eventTickets.FindAsync(id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            db.eventTickets.Remove(eventTicket);
            await db.SaveChangesAsync();

            return Ok(eventTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool eventTicketExists(int id)
        {
            return db.eventTickets.Count(e => e.eventCode == id) > 0;
        }
    }
}