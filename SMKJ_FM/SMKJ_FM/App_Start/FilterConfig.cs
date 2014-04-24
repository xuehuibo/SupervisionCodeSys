using System.Web;
using System.Web.Mvc;
using SMKJ_FM.Filters;

namespace SMKJ_FM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionFilter());
        }
    }
}