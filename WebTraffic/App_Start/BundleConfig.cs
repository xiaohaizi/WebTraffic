using System.Web;
using System.Web.Optimization;

namespace WebTraffic
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           
           
            bundles.Add(new StyleBundle("~/bundles/scripts").Include(
                      "~/Content/Scripts/*.js"
                      ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/amazeui.min.css",
                      "~/Content/css/admin.css",
                      "~/Content/css/app.css"));
        }
    }
}
