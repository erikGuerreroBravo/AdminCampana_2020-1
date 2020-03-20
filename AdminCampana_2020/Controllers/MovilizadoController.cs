using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AdminCampana_2020.Controllers
{
    public class MovilizadoController : Controller
    {

        IMovilizadoBusiness ImovilizadoBusiness;
        IColoniaBusiness IcoloniaBusiness;
        IZonaBusiness IzonaBusiness;
        ISeccionBusiness IseccionBusiness;
       

        public MovilizadoController(IMovilizadoBusiness _ImovilizadoBusiness, IColoniaBusiness _IcoloniaBusiness, IZonaBusiness _IzonaBusiness, ISeccionBusiness _IseccionBusiness
            )
        {
            ImovilizadoBusiness = _ImovilizadoBusiness;
            IcoloniaBusiness = _IcoloniaBusiness;
            IzonaBusiness = _IzonaBusiness;
            IseccionBusiness = _IseccionBusiness;
        }

        // GET: Movilizado
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registro()
        {
            ViewData["Direccion.idColonia"] = new SelectList(IcoloniaBusiness.GetColonias(), "id", "strAsentamiento");
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Registro(MovilizadoVM movilizadoVM)
        {
            try
            {
                if (movilizadoVM != null)
                {
                     var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

                    movilizadoVM.idUsuario = int.Parse(identity.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());

                    MovilizadoDomainModel movilizadoDomainModel = new MovilizadoDomainModel();
                    AutoMapper.Mapper.Map(movilizadoVM, movilizadoDomainModel);
                    ImovilizadoBusiness.AddUpdateMovilizado(movilizadoDomainModel);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return RedirectToAction("Registro","Movilizado");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Registros()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<MovilizadoDomainModel> movilizadosDM = ImovilizadoBusiness.GetAllMovilizados();
                List<MovilizadoVM> movilizadosVM = new List<MovilizadoVM>();
                AutoMapper.Mapper.Map(movilizadosDM, movilizadosVM);
                return View(movilizadosVM);
            }
            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetMovilizado(int id, int type)
        {
            MovilizadoVM movilizadoVM = null;
            MovilizadoDomainModel movilizadoDomainModel = null;
            switch (type)
            {
                case 1:
                  movilizadoDomainModel = ImovilizadoBusiness.GetMovilizadoById(id);
                  movilizadoVM = new MovilizadoVM();
                    AutoMapper.Mapper.Map(movilizadoDomainModel, movilizadoVM);

                    return PartialView("_Display", movilizadoVM);
                case 2:
                    movilizadoDomainModel = ImovilizadoBusiness.GetMovilizadoById(id);
                    movilizadoVM = new MovilizadoVM();
                    AutoMapper.Mapper.Map(movilizadoDomainModel, movilizadoVM);
                    ViewData["Direccion.idColonia"] = new SelectList(IcoloniaBusiness.GetColonias(), "id", "strAsentamiento");
                    return PartialView("_Update", movilizadoVM);
                case 3:
                    break;
                default:
                    break;
            }
            return PartialView("");
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public JsonResult Consultar()
        //{
        //    return Json(ImovilizadoBusiness.GetAllMovilizados(), JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //[Authorize]
        //public ActionResult Editar(int _id)
        //{


        //    if (User.Identity.IsAuthenticated)
        //    {
        //        MovilizadoDomainModel movilizadoDM = ImovilizadoBusiness.GetMovilizadoById(_id);
        //        if (movilizadoDM != null)
        //        {
        //            MovilizadoVM movilizadoVM = new MovilizadoVM();
        //            AutoMapper.Mapper.Map(movilizadoDM, movilizadoVM);
        //            TelefonoVM telefonoVM = new TelefonoVM();
        //            AutoMapper.Mapper.Map(movilizadoDM.TelefonoDomainModel, telefonoVM);
        //            movilizadoVM.TelefonoVM = telefonoVM;
        //            return View("Editar", movilizadoVM);
        //        }
        //        else
        //        {
        //            return RedirectToAction("InternalServerError", "Error");
        //        }
        //    }
        //    return RedirectToAction("Login","Account");
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Authorize]
        //public ActionResult Editar([Bind(Include = "Id,StrNombre,StrApellidoPaterno,StrApellidoMaterno,TelefonoVM")]MovilizadoVM movilizadoVM)
        //{
        //    if (movilizadoVM != null && ModelState.IsValid)
        //    {
        //        MovilizadoDomainModel movilizadoDM = new MovilizadoDomainModel();
        //        TelefonoDomainModel telefonoDM = new TelefonoDomainModel();
        //        AutoMapper.Mapper.Map(movilizadoVM.TelefonoVM,telefonoDM);
        //        AutoMapper.Mapper.Map(movilizadoVM,movilizadoDM);
        //        movilizadoDM.TelefonoDomainModel = telefonoDM;
        //        ImovilizadoBusiness.UpdateMovilizado(movilizadoDM);

        //    }
        //    return RedirectToAction("Registros", "Movilizado");
        //}

        //[HttpGet]
        //public JsonResult GetDatosDireccion()
        //{
        //    SeccionVM seccionVM = new SeccionVM();
        //    SeccionDomainModel seccionDM = new SeccionDomainModel();

        //    seccionDM = IseccionBusiness.GetSeccionById(1);

        //    if (seccionDM != null)
        //    {
        //        seccionVM = new SeccionVM();
        //        AutoMapper.Mapper.Map(seccionDM, seccionVM);
        //    }

        //    return Json(seccionVM, JsonRequestBehavior.AllowGet);
        //}


    }
}