using System.Web;
using System.Web.Mvc;

namespace Asp.net_Web_API_torutial_for_begginers
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
