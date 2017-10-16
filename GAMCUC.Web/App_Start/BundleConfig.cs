using System.Web;
using System.Web.Optimization;

namespace GAMCUC.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/IEScript").Include(
                  "~/Content/Dashboard/assets/global/plugins/respond.min.js",
                  "~/Content/Dashboard/assets/global/plugins/excanvas.min.js",
                  "~/Content/Dashboard/assets/global/plugins/ie8.fix.min.js"
          ));

            bundles.Add(new ScriptBundle("~/bundles/core/js").Include(
                     "~/Content/assets/global/plugins/jquery.min.js",
                     "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                     "~/Content/assets/global/plugins/js.cookie.min.js",
                     "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                     "~/Content/assets/global/plugins/jquery.blockui.min.js",
                     "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/global/js").Include(
                    "~/Content/assets/global/scripts/app.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout/js").Include(
                     "~/Content/assets/layouts/layout/scripts/layout.min.js",
                     "~/Content/assets/layouts/layout/scripts/demo.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/page-plugin/js").Include(
                     "~/Content/assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js",
                    "~/Content/assets/global/plugins/select2/js/select2.full.min.js",
                    "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                    "~/Content/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                    "~/Content/assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js",

                     "~/Content/assets/global/scripts/datatable.js",
                     "~/Content/assets/global/plugins/datatables/datatables.min.js",
                     "~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                     "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                     "~/Content/assets/global/plugins/bootbox/bootbox.min.js",
                      "~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.js",
                      "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/page-scripts/js").Include(
                    
                    //"~/Content/assets/pages/scripts/form-wizard.js",
                    "~/Content/assets/pages/scripts/table-datatables-buttons.min.js",
                    "~/Content/assets/pages/scripts/form-samples.min.js",
                    "~/Content/assets/pages/scripts/ui-bootbox.min.js",
                    "~/Content/assets/pages/scripts/ui-modals.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/login-plugins/js").Include(
                   "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                   "~/Content/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                   "~/Content/assets/global/plugins/select2/js/select2.full.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/login/js").Include(
                     "~/Content/assets/pages/scripts/login.min.js"));

            //css

            bundles.Add(new StyleBundle("~/Content/mandatory/css").Include(
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/Content/global/css").Include(
                      "~/Content/assets/global/css/components.css",
                      "~/Content/assets/global/css/plugins.css"));

            bundles.Add(new StyleBundle("~/Content/page-style/css").Include(
                     "~/Content/assets/pages/css/invoice.min.css"));

            bundles.Add(new StyleBundle("~/Content/layout/css").Include(
                     "~/Content/assets/layouts/layout/css/layout.min.css",
                     "~/Content/assets/layouts/layout/css/themes/darkblue.min.css",
                     "~/Content/assets/layouts/layout/css/custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/page-plugin/css").Include(
                    "~/Content/assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css",
                    "~/Content/assets/global/plugins/select2/css/select2.min.css",
                     "~/Content/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                    "~/Content/assets/global/plugins/datatables/datatables.min.css",
                    "~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css",
                    "~/Content/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                    "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.css"));



            bundles.Add(new StyleBundle("~/Content/login-plugins/css").Include(
                     "~/Content/assets/global/plugins/select2/css/select2.min.css",
                     "~/Content/assets/global/plugins/select2/css/select2-bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/login-style/css").Include(
                   "~/Content/assets/pages/css/login.min.css",
                   "~/Content/Site.css"));

            bundles.IgnoreList.Clear();
            System.Web.Optimization.BundleTable.EnableOptimizations = true;
        }
    }
}
