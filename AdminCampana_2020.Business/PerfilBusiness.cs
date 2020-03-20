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
    public class PerfilBusiness : IPerfilBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PerfilRepository perfilRepository;

        public PerfilBusiness(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
            perfilRepository = new PerfilRepository(unitOfWork);
        }

        public List<PerfilDomainModel> GetAllPerfiles() 
        {
            List<PerfilDomainModel> perfilesDM = new List<PerfilDomainModel>();
            List<Perfil> perfils = new List<Perfil>();

            perfils = perfilRepository.GetAll().ToList();

            foreach (Perfil item in perfils)
            {
                PerfilDomainModel perfilDm = new PerfilDomainModel();

                perfilDm.Id = item.id;
                perfilDm.StrDescripcion = item.strDescripcion;
                perfilDm.StrValor = item.strValor;

                perfilesDM.Add(perfilDm);
            }

            PerfilDomainModel perfilDM = new PerfilDomainModel();

            perfilDM.Id = 0;
            perfilDM.StrDescripcion = "Seleccionar";
            perfilDM.StrValor = "Seleccionar";

            perfilesDM.Insert(0, perfilDM);

            return perfilesDM;
        }
    }
}
