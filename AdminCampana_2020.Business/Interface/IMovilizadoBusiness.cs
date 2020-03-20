using AdminCampana_2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Business.Interface
{
    public interface IMovilizadoBusiness
    {
        bool AddUpdateMovilizado(MovilizadoDomainModel personaDM);
        List<MovilizadoDomainModel> GetAllMovilizados();
        MovilizadoDomainModel GetMovilizadoById(int id);
        string UpdateMovilizado(MovilizadoDomainModel personaDM);
    }
}
