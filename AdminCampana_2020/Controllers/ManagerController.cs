using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AdminCampana_2020.Controllers
{
    public class ManagerController : Controller
    {

        private IRolBusiness rolBusiness;
        private IUsuarioBusiness usuarioBusiness;
        private IMovilizadoBusiness movilizadoBusiness;
        private IMetaBusiness metaBusiness;

        public ManagerController(IRolBusiness _rolBusiness, IUsuarioBusiness _usuarioBusiness, IMovilizadoBusiness _movilizadoBusiness, IMetaBusiness _metaBusiness)
        {
            this.rolBusiness = _rolBusiness;
            this.usuarioBusiness = _usuarioBusiness;
            this.movilizadoBusiness = _movilizadoBusiness;
            this.metaBusiness = _metaBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "Super Administrador,Administrador")]
        public ActionResult Visualizacion()
        {

            #region Consultamos los claims de la aplicacion
            ClaimsPrincipal principal = this.User as ClaimsPrincipal;
            var rol = principal.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value).SingleOrDefault();
            ViewBag.Role = rol;
            ViewBag.IdRol = new SelectList(rolBusiness.GetRoles(), "Id", "Nombre");
            #endregion

            AreasDomainModel areasDM = GetDatosMovilizadosByAreas();
            int totalMovilizados = this.movilizadoBusiness.CountMovilizadosTotal();
            int totalCoordinadores = this.usuarioBusiness.CountUsuariosCoordinadoresTotal();
            int metaTotal = this.metaBusiness.CountMetaTotal();
            this.FillViewBagView(areasDM, totalMovilizados, totalCoordinadores, metaTotal);
            return View();
        }

        [HttpGet]
        public JsonResult GetDatosUsuarioCoordinador(int idArea)
        {
           
            var usuarios = usuarioBusiness.GetAllCoordinadores(idArea);
            return Json(usuarios.ToList(), JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// Metodo que se encarga de consultar el total de registro por areas
        /// </summary>
        private AreasDomainModel GetDatosMovilizadosByAreas()
        {
            string[] areas = new string[] { "MultiNivel", "Planilla Ganadora", "Campaña", "En Campaña", "Redes Sociales" };
            AreasDomainModel areasDM = new AreasDomainModel();
            foreach (string item in areas)
            {
                if (item.Equals("MultiNivel"))
                {
                    areasDM.StrValorMultinivel = item;
                    areasDM.TotalMultinvel = movilizadoBusiness.TotalByAreaCoordinadores(item);
                }
                if (item.Equals("Planilla Ganadora"))
                {
                    areasDM.StrValorPlanillaGanadora = item;//"Planilla Ganadora";
                    areasDM.TotalPlanillaGanadora = movilizadoBusiness.TotalByAreaCoordinadores(item);
                }
                if (item.Equals("Campaña"))
                {
                    areasDM.StrValorCampania = item;//"Campaña";
                    areasDM.TotalCampania = movilizadoBusiness.TotalByAreaCoordinadores(item);
                }
                if (item.Equals("En Campaña"))
                {
                    areasDM.StrValorEnCampania = item;//"En Campaña";
                    areasDM.TotalEnCampania = movilizadoBusiness.TotalByAreaCoordinadores(item);
                }
                if (item.Equals("Redes Sociales"))
                {
                    areasDM.StrValorRedesSociales = item;
                    areasDM.TotalRedesSociales = movilizadoBusiness.TotalByAreaCoordinadores(item);
                }
            }
            return areasDM;
        }


        private void FillViewBagView(AreasDomainModel areasDM, int totalMovilizados, int totalCoordinadores, int metaTotal)
        {
            ViewBag.totalMultinivel = areasDM.TotalMultinvel;
            ViewBag.totalPlanillaGanadora = areasDM.TotalPlanillaGanadora;
            ViewBag.totalCampania = areasDM.TotalCampania;
            ViewBag.totalEnCampania = areasDM.TotalEnCampania;
            ViewBag.RedesSociales = areasDM.TotalRedesSociales;
            ViewBag.TotalMovilizados = totalMovilizados;
            ViewBag.TotalCoordinadores = totalCoordinadores;
            ViewBag.MetaTotal = metaTotal;
        }


        /// <summary>
        /// Este metodo se encarga de traer a todos los movilizadores con sus movilizados  pertenecientes a un coordinador
        /// </summary>
        /// <param name="idCoordinador">el identificador del coordinador</param>
        /// <returns>un json con los elementos de la consulta compleja</returns>
        [HttpGet]
        public JsonResult GetAllDataByCoordinador(int idCoordinador)
        {
            List<CoordinadorDomainModel> coordinadorDM = null;
            var usuarios = usuarioBusiness.GetMovilizadoresByCoordinador(idCoordinador);
            if (usuarios != null)
            {
                coordinadorDM = new List<CoordinadorDomainModel>();

                foreach (var u in usuarios)
                {

                    foreach (var m in u.Movilizados)
                    {
                        CoordinadorDomainModel coordinador = new CoordinadorDomainModel();
                        coordinador.Nombres = u.Nombres;
                        coordinador.Apellidos = u.Apellidos;
                        coordinador.NombreMovilizado = m.StrNombre;
                        coordinador.ApellidoPaternoMovilizado = m.StrApellidoPaterno;
                        coordinador.ApellidoMaternoMovilizado = m.StrApellidoMaterno;
                        coordinador.CalleMovilizado = m.DireccionDomainModel.StrCalle;
                        coordinador.NumeroInteriorMovilizado = m.DireccionDomainModel.StrNumeroInterior;
                        coordinador.CodigoPostalMovilizado = m.DireccionDomainModel.ColoniaDomainModel.StrCodigoPostal;
                        coordinador.TipoAsentamientoMovilizado = m.DireccionDomainModel.ColoniaDomainModel.StrTipoDeAsentamiento;
                        coordinador.AsentamientoMovilizado = m.DireccionDomainModel.ColoniaDomainModel.StrAsentamiento;
                        coordinador.TelefonoCelular = m.TelefonoDomainModel.StrNumeroCelular;
                        coordinadorDM.Add(coordinador);
                    }

                }
            }

            return Json(coordinadorDM, JsonRequestBehavior.AllowGet);
        }





    }
}