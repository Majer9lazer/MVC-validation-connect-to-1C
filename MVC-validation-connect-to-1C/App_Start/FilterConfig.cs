using System.Web;
using System.Web.Mvc;

namespace MVC_validation_connect_to_1C
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
