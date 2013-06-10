using System.Web;
using System.Web.Optimization;

namespace Coombu
{
    public class BundleConfig
    {
        // Pour plus d’informations sur le Bundling, accédez à l’adresse http://go.microsoft.com/fwlink/?LinkId=254725 (en anglais)
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-migrate-1.1.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.10.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include(
                        "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(                        
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/infinitescroll").Include(
                        "~/Scripts/jquery.infinitescroll*"));

            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l’outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/coombu").Include(
                        "~/Scripts/coombu.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui-input-file").Include(
                        "~/Scripts/enhance.min.js",
                        "~/Scripts/fileinput.jquery.js"
                ));
            
            bundles.Add(new StyleBundle("~/bundles/jquery-ui-input-file/css").Include(
                        "~/Content/themes/third-party/css/enhanced.css"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/normalize.css",                        
                        "~/Content/Site.css",
                        "~/Content/bootstrap*"));

            bundles.Add(new StyleBundle("~/Content/themes/custom/css").Include(
                        "~/Content/themes/custom/jquery-ui-1.10.0.custom.css" // <--- Twitter Boostrap Theme
                        //"~/Content/themes/base/jquery.ui.core.css",
                        //"~/Content/themes/base/jquery.ui.resizable.css",
                        //"~/Content/themes/base/jquery.ui.selectable.css",
                        //"~/Content/themes/base/jquery.ui.accordion.css",
                        //"~/Content/themes/base/jquery.ui.autocomplete.css",
                        //"~/Content/themes/base/jquery.ui.button.css",
                        //"~/Content/themes/base/jquery.ui.dialog.css",
                        //"~/Content/themes/base/jquery.ui.slider.css",
                        //"~/Content/themes/base/jquery.ui.tabs.css",
                        //"~/Content/themes/base/jquery.ui.datepicker.css",
                        //"~/Content/themes/base/jquery.ui.progressbar.css",
                        //"~/Content/themes/base/jquery.ui.theme.css"
                        ));
        }
    }
}