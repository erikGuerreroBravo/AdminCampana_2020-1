using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.IdColonia = new SelectList(IcoloniaBusiness.GetColonias(), "id", "strAsentamiento");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registro([Bind(Include = "StrNombre,StrApellidoPaterno,StrApellidoMaterno,TelefonoVM,StrNumeroCelular,DireccionVM")]MovilizadoVM movilizadoVM, string IdColonia)
        {
            var coloniaId = int.Parse(IdColonia);


            movilizadoVM.DireccionVM.ColoniaVM = new ColoniaVM();
            movilizadoVM.DireccionVM.ColoniaVM.Id = coloniaId;


            if (ModelState.IsValid)
            {

                DireccionDomainModel direccionDM = new DireccionDomainModel();

                //SeccionDomainModel seccionDM = new SeccionDomainModel();
                ColoniaDomainModel coloniaDM = new ColoniaDomainModel();
                //ZonaDomainModel zonaDM = new ZonaDomainModel();
                MovilizadoDomainModel movilizadoDM = new MovilizadoDomainModel();
                TelefonoDomainModel telefonoDM = new TelefonoDomainModel();


                AutoMapper.Mapper.Map(movilizadoVM.DireccionVM, direccionDM);

                //AutoMapper.Mapper.Map(movilizadoVM.DireccionVM.SeccionVM, seccionDM);
                AutoMapper.Mapper.Map(movilizadoVM.DireccionVM.ColoniaVM, coloniaDM);
                //AutoMapper.Mapper.Map(movilizadoVM.DireccionVM.ZonaVM, zonaDM);
                AutoMapper.Mapper.Map(movilizadoVM.TelefonoVM, telefonoDM);

                AutoMapper.Mapper.Map(movilizadoVM, movilizadoDM);

                movilizadoDM.DireccionDomainModel = direccionDM;
                //movilizadoDM.DireccionDomainModel.SeccionDomainModel = seccionDM;
                movilizadoDM.DireccionDomainModel.ColoniaDomainModel = coloniaDM;
                //movilizadoDM.DireccionDomainModel.ZonaDomainModel = zonaDM;
                movilizadoDM.TelefonoDomainModel = telefonoDM;

                ImovilizadoBusiness.AddUpdateMovilizado(movilizadoDM);
            }

            ViewBag.IdColonia = new SelectList(IcoloniaBusiness.GetColonias(), "id", "strAsentamiento");
            return View("Registro");
        }

        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public JsonResult Consultar()
        {
            return Json(ImovilizadoBusiness.GetAllMovilizados(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize]
        public ActionResult Editar(int _id)
        {


            if (User.Identity.IsAuthenticated)
            {
                MovilizadoDomainModel movilizadoDM = ImovilizadoBusiness.GetMovilizadoById(_id);
                if (movilizadoDM != null)
                {
                    MovilizadoVM movilizadoVM = new MovilizadoVM();
                    AutoMapper.Mapper.Map(movilizadoDM, movilizadoVM);
                    TelefonoVM telefonoVM = new TelefonoVM();
                    AutoMapper.Mapper.Map(movilizadoDM.TelefonoDomainModel, telefonoVM);
                    movilizadoVM.TelefonoVM = telefonoVM;
                    return View("Editar", movilizadoVM);
                }
                else
                {
                    return RedirectToAction("InternalServerError", "Error");
                }
            }
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize]
        public ActionResult Editar([Bind(Include = "Id,StrNombre,StrApellidoPaterno,StrApellidoMaterno,TelefonoVM")]MovilizadoVM movilizadoVM)
        {
            if (movilizadoVM != null && ModelState.IsValid)
            {
                MovilizadoDomainModel movilizadoDM = new MovilizadoDomainModel();
                TelefonoDomainModel telefonoDM = new TelefonoDomainModel();
                AutoMapper.Mapper.Map(movilizadoVM.TelefonoVM,telefonoDM);
                AutoMapper.Mapper.Map(movilizadoVM,movilizadoDM);
                movilizadoDM.TelefonoDomainModel = telefonoDM;
                ImovilizadoBusiness.UpdateMovilizado(movilizadoDM);

            }
            return RedirectToAction("Registros", "Movilizado");
        }

        [HttpGet]
        public JsonResult GetDatosDireccion()
        {
            SeccionVM seccionVM = new SeccionVM();
            SeccionDomainModel seccionDM = new SeccionDomainModel();

            seccionDM = IseccionBusiness.GetSeccionById(1);
           
            if (seccionDM != null)
            {
                seccionVM = new SeccionVM();
                AutoMapper.Mapper.Map(seccionDM, seccionVM);
            }
           
            return Json(seccionVM, JsonRequestBehavior.AllowGet);
        }


    }
}