using Pastelaria.Web.Application.Teste;
using Pastelaria.Web.Application.Teste.Model;
using Pastelaria.Web.Infra;
using System.Web.Mvc;

namespace Pastelaria.Web.Controllers
{
    public class Home2Controller : BaseController
    {
        private readonly TesteApplication _testeApplication;

        public Home2Controller(TesteApplication testeApplication)
        {
            _testeApplication = testeApplication;
        }

        public ActionResult Index()
        {
            return View(_testeApplication.Get());
        }

        public ActionResult GetGrid()
        {
            var response = _testeApplication.GetTestes();
            if (!response.Ok)
                return Error(response.Messages);

            return View("_GridTestes", response.Content);
        }

        public ActionResult Post(TesteModel teste)
        {
            var response = _testeApplication.Post(teste);
            if (!response.Ok)
                return Error(response.Messages);

            return Success();
        }

        public ActionResult Put(TesteModel teste)
        {
            return Success();
        }

        public ActionResult Delete(int identificador)
        {
            return Success();
        }
    }
}