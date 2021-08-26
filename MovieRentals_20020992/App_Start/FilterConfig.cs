using System.Web;
using System.Web.Mvc;

namespace MovieRentals_20020992
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
