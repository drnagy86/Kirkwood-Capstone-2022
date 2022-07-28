using System.Web;
using System.Web.Optimization;

namespace MVCPresentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(

                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"

                        ));
                       

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            ////Add java script files here
            //bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
            //            "~/Scripts/sortEventTable.js"
                        

            //            ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme-lumen.css",                                            
                      "~/Content/site.css",
                      "~/Content/media.css"));
            //Add java script files here
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/index.js",
                        "~/Scripts/sortEventTable.js",
                        "~/Scripts/sortTable.js",
                        "~/Scripts/addLocation.js",
                        "~/Scripts/addDate.js"
                        ));

        }
    }
}
