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
    public class accountsController : ApiController
    {
        private fypmobileEntities db = new fypmobileEntities();

        // GET: api/accounts
        public IQueryable<account> Getaccounts()
        {
            return db.accounts;
        }

        // GET: api/accounts/5
        [ResponseType(typeof(account))]
        public async Task<IHttpActionResult> Getaccount(int id)
        {
            account account = await db.accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/accounts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putaccount(int id, account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.uid)
            {
                return BadRequest();
            }

            db.Entry(account).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!accountExists(id))
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

        // POST: api/accounts
        [ResponseType(typeof(account))]
        public async Task<IHttpActionResult> Postaccount(account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.accounts.Add(account);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = account.uid }, account);
        }

        // DELETE: api/accounts/5
        [ResponseType(typeof(account))]
        public async Task<IHttpActionResult> Deleteaccount(int id)
        {
            account account = await db.accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            db.accounts.Remove(account);
            await db.SaveChangesAsync();

            return Ok(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool accountExists(int id)
        {
            return db.accounts.Count(e => e.uid == id) > 0;
        }
    }
}