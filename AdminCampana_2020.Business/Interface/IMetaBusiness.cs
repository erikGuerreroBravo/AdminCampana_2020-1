using AdminCampana_2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Business.Interface
{
    public interface IMetaBusiness
    {
        List<MetaDomainModel> GetAllMetas();
        bool UpdateMeta(MetaDomainModel _meta);
        /// <summary>
        /// Este metodo se encarga de contar cuantos movilizados se tienen hasta el momento
        /// </summary>
        /// <returns>
        /// regresa el total de movilizados activos
        /// </returns>
        int CountMetaTotal();
    }
}
