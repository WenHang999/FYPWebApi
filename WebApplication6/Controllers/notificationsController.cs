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
    public class notificationsController : ApiController
    {
        private fypmobileEntities db = new fypmobileEntities();

        // GET: api/notifications
        public IQueryable<notification> Getnotifications()
        {
            return db.notifications;
        }

        // GET: api/notifications/5
        [ResponseType(typeof(notification))]
        public async Task<IHttpActionResult> Getnotification(string id)
        {
            notification notification = await db.notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // PUT: api/notifications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putnotification(string id, notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notification.description)
            {
                return BadRequest();
            }

            db.Entry(notification).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!notificationExists(id))
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

        // POST: api/notifications
        [ResponseType(typeof(notification))]
        public async Task<IHttpActionResult> Postnotification(notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.notifications.Add(notification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (notificationExists(notification.description))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = notification.description }, notification);
        }

        // DELETE: api/notifications/5
        [ResponseType(typeof(notification))]
        public async Task<IHttpActionResult> Deletenotification(string id)
        {
            notification notification = await db.notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            db.notifications.Remove(notification);
            await db.SaveChangesAsync();

            return Ok(notification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool notificationExists(string id)
        {
            return db.notifications.Count(e => e.description == id) > 0;
        }
    }
}