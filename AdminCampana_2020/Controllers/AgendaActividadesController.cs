using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminCampana_2020.Controllers
{
    public class AgendaActividadesController : Controller
    {
        IAgendaActividadesBusiness agendaActividadesBusiness;

        public AgendaActividadesController(IAgendaActividadesBusiness _agendaActividadesBusiness)
        {
            this.agendaActividadesBusiness = _agendaActividadesBusiness;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AgendaActividades()
        {
            return PartialView("_Agenda");
        }

        [HttpGet]
        public ActionResult ObtenerActividades()
        {
            List<AgendaActividadesVM> agendaActividades = new List<AgendaActividadesVM>();
            List<AgendaActividadesDomainModel> agenda = agendaActividadesBusiness.ObtenerActividades();

            AutoMapper.Mapper.Map(agenda, agendaActividades);

            return Json(agenda, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AgregarActividades()
        {
            return PartialView("_Agregar");
        }

        [HttpPost]
        public void AgregarActividades(AgendaActividadesVM agendaActividades)
        {

            if (agendaActividades != null)
            {
                AgendaActividadesDomainModel agendaActividadesDomainModel = new AgendaActividadesDomainModel();

                AutoMapper.Mapper.Map(agendaActividades, agendaActividadesDomainModel);

                agendaActividadesBusiness.RegistrarEvento(agendaActividadesDomainModel);
            }

        }

        [HttpGet]
        public JsonResult ObtenerEventosPorFecha(string fecha)
        {

            List<AgendaActividadesDomainModel> actividadesDM = agendaActividadesBusiness.ObtenerEventosPorFecha(Convert.ToDateTime(fecha));
            List<AgendaActividadesVM> agendaActividades = new List<AgendaActividadesVM>();

            return Json(actividadesDM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerProximosEventos()
        {
            List<AgendaActividadesDomainModel> actividadesDM = agendaActividadesBusiness.ObtenerActividades().Where(p => p.dteFecha > DateTime.Now).OrderBy(p => p.dteFecha)
                .ToList();

            return Json(actividadesDM, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ActualizarActividades(AgendaActividadesVM agendaActividades)
        {

            if (agendaActividades != null)
            {
                AgendaActividadesDomainModel agendaActividadesDomainModel = new AgendaActividadesDomainModel();

                AutoMapper.Mapper.Map(agendaActividades, agendaActividadesDomainModel);

                agendaActividadesBusiness.ActualizarEvento(agendaActividadesDomainModel);
            }

        }
    }
}