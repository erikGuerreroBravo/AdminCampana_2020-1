using AdminCampana_2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Business.Interface
{
    public interface IAgendaActividadesBusiness
    {
        List<AgendaActividadesDomainModel> ObtenerActividades();
        bool RegistrarEvento(AgendaActividadesDomainModel agendaActividadesDomainModel);
        List<AgendaActividadesDomainModel> ObtenerEventosPorFecha(DateTime fecha);
        bool ActualizarEvento(AgendaActividadesDomainModel agendaActividadesDomainModel);
    }
}
