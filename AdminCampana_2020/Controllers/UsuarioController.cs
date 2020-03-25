using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.Encript;
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

        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
        [HttpPost]
        public ActionResult Create(UsuarioRolVM usuarioVM)
        {

            if (usuarioVM != null)
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

                usuarioVM.Usuario.idStatus = (int)EnumStatus.ALTA;
                usuarioVM.Usuario.Clave = Funciones.Encrypt(usuarioVM.Usuario.Clave);
                var properties = ClaimsPrincipal.Current.Identities.First();
                usuarioVM.Usuario.Id = int.Parse(properties.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);             

                if (identity.IsInRole("Administrador") || identity.IsInRole("Super Administrador"))
                {
                    UsuarioRolDomainModel usuarioDM = new UsuarioRolDomainModel();
                    AutoMapper.Mapper.Map(usuarioVM, usuarioDM);
                    usuarioBusiness.AddUpdateUsuarios(usuarioDM);
                } else
                {
                    UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();

                    if (identity.IsInRole("MultiNivel"))
                    {
                        usuarioVM.Usuario.area_movilizador = "MultiNivel";
                    }
                    else if (identity.IsInRole("Planilla Ganadora"))
                    {
                        usuarioVM.Usuario.area_movilizador = "Planilla Ganadora";
                    }
                    else if (identity.IsInRole("Campaña"))
                    {
                        usuarioVM.Usuario.area_movilizador = "Campaña";
                    }
                    else if (identity.IsInRole("En Campaña"))
                    {
                        usuarioVM.Usuario.area_movilizador = "En Campaña";
                    }
                    else if (identity.IsInRole("Redes Sociales"))
                    {
                        usuarioVM.Usuario.area_movilizador = "Redes Sociales";
                    }

                    AutoMapper.Mapper.Map(usuarioVM.Usuario, usuarioDomainModel);
                    usuarioBusiness.AddUser(usuarioDomainModel);
                }
                  
            }

            return RedirectToAction("Create", "Usuario");
        }

        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
        [HttpGet]
        public ActionResult Administrar()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            List<UsuarioDomainModel> usuariosDM = null; usuarioBusiness.GetUsuarios();
            List<UsuarioVM> usuariosVM = new List<UsuarioVM>();

            int id = int.Parse(identity.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).Select(p => p.Value).FirstOrDefault());
            if (identity.IsInRole("MultiNivel") || identity.IsInRole("Planilla Ganadora") || identity.IsInRole("Campaña") || identity.IsInRole("En Campaña") || identity.IsInRole("Redes Sociales"))
            {
                usuariosDM = usuarioBusiness.GetUsuariosByCoordinador(id);
                AutoMapper.Mapper.Map(usuariosDM, usuariosVM);
            }
            else if (identity.IsInRole("Administrador") || identity.IsInRole("Super Administrador"))
            {
                usuariosDM = usuarioBusiness.GetUsuarios();
                AutoMapper.Mapper.Map(usuariosDM, usuariosVM);
            }
            
            return View(usuariosVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
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
                    usuarioDomainModel = usuarioBusiness.GetUsuario(id);
                    usuarioVM = new UsuarioVM();
                    AutoMapper.Mapper.Map(usuarioDomainModel, usuarioVM);
                    return PartialView("_Drop", usuarioVM);                   
                case 4:
                    break;
                default:
                    break;
            }

            return PartialView("");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
        public ActionResult ChangeCoordinador(UsuarioVM usuarioVM)
        {
            UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();

            AutoMapper.Mapper.Map(usuarioVM,usuarioDomainModel);
            movilizadoBusiness.MigrarMovilizados(usuarioDomainModel);
            return RedirectToAction("Administrar","Usuario");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
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
        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
        public ActionResult GetUsuariosByApellidos(string term)
        {

            List<string> usuarios = usuarioBusiness.GetUsuariosByApellidos(term);

            var filtro = usuarios.Where(apellido => apellido.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);

            return Json(filtro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Super Administrador,MultiNivel,Planilla Ganadora,Campaña,En Campaña,Redes Sociales")]
        public ActionResult Eliminar(UsuarioVM usuarioVM)
        {
            try
            {
                if (usuarioVM != null)
                {
                    UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();
                    usuarioDomainModel = usuarioBusiness.GetUsuario(usuarioVM.Id);
                    usuarioDomainModel.IdStatus = (int)EnumStatus.BAJA;
                    usuarioBusiness.BajaUsuario(usuarioDomainModel);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return RedirectToAction("Administrar", "Usuario");
        }


    }
}