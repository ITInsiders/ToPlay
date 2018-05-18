using System.Web;
using System.Web.Optimization;

namespace TP
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/SystemStyles").Include(
                "~/Resources/CSS/System/Default.css",
                "~/Resources/CSS/Fonts.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/SystemScripts").Include(
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/jcanvas.min.js",
                "~/Scripts/jquery.signalR-2.2.3.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/SystemValidate").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"
                        ));
        }
    }
}
