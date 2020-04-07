using System.Web.Mvc;

namespace AdminCampana_2020.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult InternalServerError()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NoScript()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DuplicateData()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NoEmailRecovery()
        {
            return View("NoEmailRecovery");
        }
    }
}
