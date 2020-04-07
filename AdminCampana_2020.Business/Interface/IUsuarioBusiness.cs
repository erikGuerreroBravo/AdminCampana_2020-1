using AdminCampana_2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Business.Interface
{
    public interface IUsuarioBusiness
    {
        UsuarioDomainModel ValidarLogin(string email, string password);
        bool AddUpdateUsuarios(UsuarioRolDomainModel usuarioDM);
        List<UsuarioDomainModel> GetUsuarios();
        UsuarioDomainModel GetUsuario(int id);
        List<string> GetUsuariosByApellidos(string apellidos);

        bool BajaUsuario(UsuarioDomainModel usuarioDM);
        bool UpdateUsuario(UsuarioDomainModel usuarioDomainModel);

        bool AddUser(UsuarioDomainModel usuarioDomainModel);

        List<UsuarioDomainModel> GetUsuariosByCoordinador(int id);
        
        /// <summary>
        /// este metodo se encarga de validar el correo del usuario dentro de la plataforma para recuperar una contraseña
        /// </summary>
        /// <param name="email">el email el usuario a buscar dentro de la plataforma</param>
        /// <returns>una entidad del tipo usuarioDomainModel</returns>
        UsuarioDomainModel ValidarEmailPasswordrecovery(string email);
        /// <summary>
        /// Este metodo se encarga de almacenar un movilizador dentro del sistema
        /// </summary>
        /// <param name="usuarioDM">el movilizador del sistema</param>
        /// <param name="perfilDM">el perfil del movilizador</param>
        /// <returns>una respuesta true/false</returns>
        bool AddUpdateUsuarioMovilizador(UsuarioDomainModel usuarioDM, PerfilDomainModel perfilDM);
        
        /// <summary>
        /// Se encarga de validar un perfil dentro del sistema
        /// </summary>
        /// <param name="idUsuario">el identificador del usuario</param>
        /// <returns>regresa un perfil</returns>
        PerfilDomainModel ValidarPerfilMovilizador(int idUsuario);

        /// <summary>
        /// Este metodo se encarga de consultar a todos los coordinadores de la campaña por el area especifica
        /// </summary>
        /// <returns></returns>
        List<UsuarioDomainModel> GetCoordinadores(int idRol);
        
        List<UsuarioDomainModel> GetAllCoordinadores(int idRol);

        /// <summary>
        /// Este metodo se encarga de contar cuantos coordinadores se tienen hasta el momento
        /// </summary>
        /// <returns>
        /// regresa el total de coordinadores activos
        /// </returns>
        int CountUsuariosCoordinadoresTotal();

        /// <summary>
        /// Este metodo se encarga de traer a todos los movilizados de  cada uno de los movilizadores existentes
        /// se utiliza un stroreprocedure para la llamada a la consulta compleja
        /// </summary>
        /// <param name="idCoordinador">el identificador del coordinador</param>
        /// <returns>una lista de usuarios domain model</returns>
        List<UsuarioDomainModel> GetMovilizadoresByCoordinador(int idCoordinador);


    }
}
