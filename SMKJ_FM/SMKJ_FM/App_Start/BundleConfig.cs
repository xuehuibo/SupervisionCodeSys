using System.Web;
using System.Web.Optimization;

namespace SMKJ_FM
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js")
                        );
            bundles.Add(new ScriptBundle("~/bundles/jqueryjson").Include(
                    "~/Scripts/easyui/jquery.json.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));



            bundles.Add(new ScriptBundle("~/bundles/easyui/locale").Include(
                        "~/Scripts/easyui/locale/easyui-lang-zh_CN.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/easyui/jquery-{version}.js",
                        "~/Scripts/easyui/jquery.easyui.js"
                            ));
            bundles.Add(new StyleBundle("~/bundles/easyui/css").Include(
                        "~/Scripts/easyui/themes/icon.css",
                        "~/Scripts/easyui/themes/default/easyui.css"
                            ));

            bundles.Add(new ScriptBundle("~/bundles/tools").Include(
                        "~/Scripts/Tools.js"
                             ));
            bundles.Add(new ScriptBundle("~/bundles/MyJs").Include(
                          "~/Scripts/MyJs.js"
                             ));
            bundles.Add(new ScriptBundle("~/bundles/uploadifyJs").Include(
                           "~/Scripts/uploadify/jquery.uploadify.js"
                           ));
            bundles.Add(new StyleBundle("~/bundles/uploadifyCss").Include(
                            "~/Scripts/uploadify/uploadify.css"
                            ));

        }
    }
}