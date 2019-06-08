using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace IndividualUserAccount.Controllers
{
    public class EmployeeController : ApiController
    {
        [Authorize]
        public IEnumerable<MyHoney> Get()
        {
            using (Entities entity = new Entities())
            {
                Debug.WriteLine("ONE MORE TIME___________________________________________________________");
                return entity.MyHoneys.ToList();
            }
        }
    }
}
