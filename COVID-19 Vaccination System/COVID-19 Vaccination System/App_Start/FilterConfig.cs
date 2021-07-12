using System.Web;
using System.Web.Mvc;

namespace COVID_19_Vaccination_System {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
