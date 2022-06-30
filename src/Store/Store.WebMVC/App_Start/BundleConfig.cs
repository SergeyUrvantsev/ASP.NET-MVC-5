﻿using System.Web;
using System.Web.Optimization;

namespace Store.WebMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/developerScripts/Scripts.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/developerScripts/script.js",
                        "~/Scripts/jquery-ui-1.13.1.min.js",
                        "~/Scripts/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/Site.css",
                        "~/Content/themes/base/all.css",
                        "~/Content/themes/base/jquery-ui.css"));
        }
    }
}
