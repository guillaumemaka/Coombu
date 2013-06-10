using Coombu.Filters;
using Coombu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Coombu.Controllers
{
    [InitializeSimpleMembership]
    public class BaseController : Controller
    {
        protected readonly CoombuContext db = new CoombuContext();

        [NonAction]
        protected int GetCurrentUserID()
        {
            MembershipUser user = Membership.GetUser();

            if (user == null)
                return -1;

            return int.Parse(user.ProviderUserKey.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
