using System.Web;
using System.Web.Optimization;

namespace BolsaTrabajoV1
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jsPlantilla").Include(
                        "~/Scripts/jsPlantilla/jquery.min.js",
                        "~/Scripts/jsPlantilla/bootstrap.min.js",
                        "~/Scripts/jsPlantilla/Char.min.js",
                        "~/Scripts/jsPlantilla/custom.min.js",
                        "~/Scripts/jsPlantilla/fastclick.js",
                        "~/Scripts/jsPlantilla/nprogress.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new StyleBundle("~/Content/cssPlantilla").Include(
                      "~/Content/cssPlantilla/bootstrap.min.css",
                      "~/Content/cssPlantilla/font-awesome.min.css",
                      "~/Content/cssPlantilla/nprogress.css",
                      "~/Content/cssPlantilla/green.css",
                      "~/Content/cssPlantilla/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/cssPlantilla/jqvmap.min.css",
                      "~/Content/cssPlantilla/daterangepicker.css",
                      "~/Content/cssPlantilla/custom.min.css"));

        }
    }
}
