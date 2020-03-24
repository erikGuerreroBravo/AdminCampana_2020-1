using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.Enums;
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
        [Authorize]
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
                    movilizadoDomainModel = ImovilizadoBusiness.GetMovilizadoById(id);
                    movilizadoVM = new MovilizadoVM();
                    AutoMapper.Mapper.Map(movilizadoDomainModel, movilizadoVM);
                    return PartialView("_Drop", movilizadoVM);
                default:
                    break;
            }
            return PartialView("");
        }
        [HttpGet]
        public JsonResult GetDatosMovilizado(int id)
        {

            SeccionVM seccionVM = new SeccionVM();
            SeccionDomainModel seccionDM = new SeccionDomainModel();

            seccionDM = IseccionBusiness.GetSeccionById(id);

            if (seccionDM != null)
            {
                seccionVM = new SeccionVM();
                AutoMapper.Mapper.Map(seccionDM, seccionVM);
            }
            ZonaVM zonaVM = new ZonaVM();


            return Json(seccionVM, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Eliminar(MovilizadoVM movilizadoVM)
        {
            try
            {
                if (movilizadoVM != null)
                {

                    MovilizadoDomainModel movilizadoDomainModel = new MovilizadoDomainModel();
                    movilizadoDomainModel = ImovilizadoBusiness.GetMovilizadoById(movilizadoVM.Id);
                    movilizadoDomainModel.idStatus = (int)EnumStatus.BAJA;
                    ImovilizadoBusiness.BajaMovilizado(movilizadoDomainModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Registros", "Movilizado");
        }
        
    }
}