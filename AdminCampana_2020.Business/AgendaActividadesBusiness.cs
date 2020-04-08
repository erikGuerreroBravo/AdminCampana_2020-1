using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.Repository;
using AdminCampana_2020.Repository.Infraestructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Business
{
    public class AgendaActividadesBusiness : IAgendaActividadesBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AgendaActividadesRepository agendaActividadesRepository;

        public AgendaActividadesBusiness(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
            agendaActividadesRepository = new AgendaActividadesRepository(_unitOfWork);
        }

        public List<AgendaActividadesDomainModel> ObtenerActividades()
        {

            List<AgendaActividadesDomainModel> activdades = new List<AgendaActividadesDomainModel>();
            List<AgendaActividades> activdad = new List<AgendaActividades>();

            activdad = agendaActividadesRepository.GetAll().OrderBy(p=> p.dteFecha).ToList();

            foreach (AgendaActividades item in activdad)
            {
                AgendaActividadesDomainModel agendaActividadesDomainModel = new AgendaActividadesDomainModel();

                agendaActividadesDomainModel.id = item.id;
                agendaActividadesDomainModel.strActividad = item.strActividad;
                agendaActividadesDomainModel.strDescripcion = item.strDescripcion;
                agendaActividadesDomainModel.strHoraInicio = item.strHoraInicio.ToString();
                agendaActividadesDomainModel.strHoraTermino = item.strHoraTermino.ToString();
                agendaActividadesDomainModel.strLugar = item.strLugar;
                agendaActividadesDomainModel.dteFecha = item.dteFecha.Value;
                agendaActividadesDomainModel.Fecha = item.dteFecha.Value.ToShortDateString();

                activdades.Add(agendaActividadesDomainModel);
            }


            return activdades;
        }

        public bool RegistrarEvento(AgendaActividadesDomainModel agendaActividadesDomainModel)
        {
            bool respuesta = false;

            if (agendaActividadesDomainModel != null)
            {
                AgendaActividades agendaActividades = new AgendaActividades();

                agendaActividades.strActividad = agendaActividadesDomainModel.strActividad;
                agendaActividades.strDescripcion = agendaActividadesDomainModel.strDescripcion;
                agendaActividades.strHoraInicio = agendaActividadesDomainModel.strHoraInicio;
                agendaActividades.strHoraTermino = agendaActividadesDomainModel.strHoraTermino;
                agendaActividades.strLugar = agendaActividadesDomainModel.strLugar;
                agendaActividades.dteFecha = agendaActividadesDomainModel.dteFecha;

                agendaActividadesRepository.Insert(agendaActividades);
                respuesta = true;
            }

            return respuesta;
        }

        public List<AgendaActividadesDomainModel> ObtenerEventosPorFecha(DateTime fecha)
        {
            List<AgendaActividadesDomainModel> agendaActividadesDomainModels = new List<AgendaActividadesDomainModel>();
            List<AgendaActividades> agendaActividades = agendaActividadesRepository.GetAll().Where(p => p.dteFecha.Equals(fecha)).ToList();

            foreach (AgendaActividades item in agendaActividades)
            {
                AgendaActividadesDomainModel agendaActividadesDomainModel = new AgendaActividadesDomainModel();

                agendaActividadesDomainModel.id = item.id;
                agendaActividadesDomainModel.strActividad = item.strActividad;
                agendaActividadesDomainModel.strLugar = item.strLugar;
                agendaActividadesDomainModel.strDescripcion = item.strDescripcion;
                agendaActividadesDomainModel.strHoraInicio = item.strHoraInicio;
                agendaActividadesDomainModel.strHoraTermino = item.strHoraTermino;
                agendaActividadesDomainModel.dteFecha = item.dteFecha.Value;
                agendaActividadesDomainModel.Fecha = item.dteFecha.Value.ToString("yyyy-MM-dd");

                agendaActividadesDomainModels.Add(agendaActividadesDomainModel);

            }

            return agendaActividadesDomainModels;
        }

        public bool ActualizarEvento(AgendaActividadesDomainModel agendaActividadesDomainModel)
        {
            bool respuesta = false;

            if (agendaActividadesDomainModel != null)
            {
                AgendaActividades agendaActividades = agendaActividadesRepository.SingleOrDefault(p => p.id == agendaActividadesDomainModel.id);

                if (agendaActividades != null)
                {
                    agendaActividades.strActividad = agendaActividadesDomainModel.strActividad;
                    agendaActividades.strDescripcion = agendaActividadesDomainModel.strDescripcion;
                    agendaActividades.strHoraInicio = agendaActividadesDomainModel.strHoraInicio;
                    agendaActividades.strHoraTermino = agendaActividadesDomainModel.strHoraTermino;
                    agendaActividades.strLugar = agendaActividadesDomainModel.strLugar;
                    agendaActividades.dteFecha = agendaActividadesDomainModel.dteFecha;

                    agendaActividadesRepository.Update(agendaActividades);
                    respuesta = true;
                }
         
            }

            return respuesta;
        }
    }
}
