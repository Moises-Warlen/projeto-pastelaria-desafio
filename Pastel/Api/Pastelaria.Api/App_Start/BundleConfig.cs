using System.Web;
using System.Web.Optimization; // Bibliotecas para manipular o bundling e a otimização de scripts e CSS no ASP.NET.

namespace Pastelaria.Api // Define o namespace da aplicação.
{
    public class BundleConfig
    {
        // Método responsável por registrar os pacotes (bundles) de scripts e estilos.
        // O bundling ajuda a reduzir o número de solicitações HTTP, combinando arquivos de script e CSS em um único pacote.
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Adiciona um pacote de scripts contendo o jQuery. O {version} é um curinga para garantir que a versão correta do jQuery seja incluída.
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Adiciona um pacote de scripts para validação do jQuery. O * indica que serão incluídos todos os arquivos que correspondem ao padrão 'jquery.validate'.
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Adiciona um pacote de scripts para o Modernizr. Este script é útil para detectar recursos HTML5 e CSS3 em navegadores.
            // No desenvolvimento, é recomendável usar a versão completa, e no ambiente de produção, selecionar apenas os testes necessários usando a ferramenta do Modernizr.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Adiciona um pacote de scripts para o Bootstrap, que é uma biblioteca de design responsivo popular.
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // Adiciona um pacote de estilos CSS, que inclui o arquivo CSS do Bootstrap e um arquivo CSS personalizado (site.css).
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
