using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace IndividualUserAccount
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Debug.WriteLine("2nd *******Filter Config************");
            filters.Add(new HandleErrorAttribute());
        }
    }
}
