using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ApiLoginController : ApiController
    {

        private fypmobileEntities db = new fypmobileEntities();
        public Clogins Login(Clogins c)
        {
            var r = db.accounts.Where(s => s.email == c.Email && s.password == c.Password).FirstOrDefault();
            if (r == null) return null;
            Clogins g = new Clogins();
            g.Email = r.email;
            g.Password = r.password;
            g.UserID = r.uid;
            g.Name = r.name;
            g.AccountType = r.accountType;
            return g;
        }
    }
}
