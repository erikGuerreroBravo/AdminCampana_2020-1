using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.Encript;
using AdminCampana_2020.Enums;
using AdminCampana_2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AdminCampana_2020.Controllers
{
    public class UsuarioController : Controller
    {

        private IUsuarioBusiness usuarioBusiness;
        private IPerfilBusiness perfilBusiness;
        private IRolBusiness rolBusiness;
        private IMovilizadoBusiness movilizadoBusiness;

        public UsuarioController(IUsuarioBusiness usuarioBusiness, IPerfilBusiness perfilBusiness, IRolBusiness rolBusiness,IMovilizadoBusiness movilizadoBusiness)
        {
            this.usuarioBusiness = usuarioBusiness;
            this.perfilBusiness = perfilBusiness;
            this.rolBusiness = rolBusiness;
            this.movilizadoBusiness = movilizadoBusiness;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.idRol = new SelectList(rolBusiness.GetRoles(), "Id", "Nombre");
            ViewData["Usuario.idPerfil"] = new SelectList(perfilBusiness.GetAllPerfiles(),"Id","StrValor");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(UsuarioRolVM usuarioVM)
        {

            if (usuarioVM != null)
            {
                usuarioVM.Usuario.idStatus = (int)EnumStatus.ALTA;
                usuarioVM.Usuario.Clave = Funciones.Encrypt(usuarioVM.Usuario.Clave);
                var properties = ClaimsPrincipal.Current.Identities.First();
                usuarioVM.Usuario.Id = int.Parse(properties.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
                UsuarioRolDomainModel usuarioDM = new UsuarioRolDomainModel();
                AutoMapper.Mapper.Map(usuarioVM, usuarioDM);
                usuarioBusiness.AddUpdateUsuarios(usuarioDM);
            }

            return RedirectToAction("Create", "Usuario");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Administrar()
        {
            List<UsuarioDomainModel> usuarioDomainModels = usuarioBusiness.GetUsuarios();
            List<UsuarioVM> usuarioVMs = new List<UsuarioVM>();
            AutoMapper.Mapper.Map(usuarioDomainModels, usuarioVMs);
            return View(usuarioVMs);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUsuario(int id, int type)
        {
            UsuarioVM usuarioVM = null;
            switch (type)
            {
                case 1:
                    UsuarioDomainModel usuarioDomainModel = usuarioBusiness.GetUsuario(id);
                    usuarioVM = new UsuarioVM();
                    AutoMapper.Mapper.Map(usuarioDomainModel, usuarioVM);
                    ViewBag.idCambio = new SelectList(usuarioBusiness.GetUsuarios(), "Id", "NombreCompleto");
                    return PartialView("_Change", usuarioVM);
                case 2:
                    UsuarioDomainModel usuarioDomain = usuarioBusiness.GetUsuario(id);
                    usuarioVM = new UsuarioVM();
                    AutoMapper.Mapper.Map(usuarioDomain, usuarioVM);
                    return PartialView("_Update", usuarioVM);
                case 3:
                    break;
                default:
                    break;
            }

            return PartialView("");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeCoordinador(UsuarioVM usuarioVM)
        {
            UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();

            AutoMapper.Mapper.Map(usuarioVM,usuarioDomainModel);
            movilizadoBusiness.MigrarMovilizados(usuarioDomainModel);
            return RedirectToAction("Administrar","Usuario");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUsuario(UsuarioVM usuarioVM)
        {
            if (usuarioVM != null)
            {
                UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();
                AutoMapper.Mapper.Map(usuarioVM,usuarioDomainModel);
                usuarioBusiness.UpdateUsuario(usuarioDomainModel);
            }

            return RedirectToAction("Administrar","Usuario");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUsuariosByApellidos(string term)
        {

            List<string> usuarios = usuarioBusiness.GetUsuariosByApellidos(term);

            var filtro = usuarios.Where(apellido => apellido.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);

            return Json(filtro, JsonRequestBehavior.AllowGet);
        }

    }
}