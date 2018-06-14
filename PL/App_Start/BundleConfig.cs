using System.Web;
using System.Web.Optimization;

namespace TP
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/DefaultStyles").Include(
                "~/Resources/CSS/System/Default.min.css",
                "~/Resources/CSS/Fonts.css",
                "~/Content/bootstrap.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/DefaultScripts").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/jcanvas.min.js",
                "~/Scripts/jquery.signalR-2.2.3.min.js",
                "~/Resources/JS/System/Default.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/DefaultValidate").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));
        }
    }
}
