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
        string UpdatePersonal(MovilizadoDomainModel personaDM);

        List<MovilizadoDomainModel> GetAllPersonas();
        MovilizadoDomainModel GetPersonaById(int id);

        string UpdateMovilizado(MovilizadoDomainModel personaDM);
        bool MigrarMovilizados(UsuarioDomainModel usuarioDomainModel);

        bool BajaMovilizado(MovilizadoDomainModel usuarioDM);
        List<MovilizadoDomainModel> GetAllMovilizados(int idUsuario);
        List<MovilizadoDomainModel> GetMovilizadosByCoordinador(int idCoordinador);
        /// <summary>
        /// Este metodo se encarga de consultar todos los roles de un movilizador sin incluir el rol de administrador o super administrador
        /// </summary>
        /// <returns>una lista de roles sin incluier el rol de administrador y super administrador</returns>
        List<RolDomainModel> ObtenerRolesMovilizador();
        /// <summary>
        /// Este metodo se encarga de consultar el total de registros pertenecientes a una area en especifico
        /// </summary>
        /// <returns>una cantidad total de registros</returns>
        int TotalByAreaCoordinadores(string area);

        /// <summary>
        /// Este metodo se encarga de verificar que no exista un registro duplicado
        /// </summary>
        /// <param name="movilizadoDM">la entidad a evalaur en bd</param>
        /// <returns>true/false</returns>
        bool ValidarExisteMovilizado(MovilizadoDomainModel movilizadoDM);
        /// <summary>
        /// Este metodo se encarga de contar cuantos movilizados se tienen hasta el momento
        /// </summary>
        /// <returns>
        /// regresa el total de movilizados activos
        /// </returns>
        int CountMovilizadosTotal();
    }
}
