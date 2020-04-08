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
    public class UsuarioBusiness:IUsuarioBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UsuarioRepository usuarioRepository;
        private readonly UsuarioRolRepository usuarioRolRepository;
        private readonly MovilizadoRepository movilizadoRepository;

        public UsuarioBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            usuarioRepository = new UsuarioRepository(unitOfWork);
            usuarioRolRepository = new UsuarioRolRepository(unitOfWork);
            //inyectamos la dependencia del movilizador
            movilizadoRepository = new MovilizadoRepository(unitOfWork);
        }

        public bool AddUpdateUsuarios(UsuarioRolDomainModel usuarioDM)
        {
            bool resultado = false;
            if (usuarioDM != null)
            {

                Usuario_Rol usuario = usuarioRolRepository.SingleOrDefault(p => p.Id == usuarioDM.Id);

                if (usuario != null)
                {
                    if (usuarioDM.Id > 0)
                    {
                        usuario.Usuario.Nombres = usuarioDM.Usuario.Nombres;
                        usuario.Usuario.Apellidos = usuarioDM.Usuario.Apellidos;
                        usuario.Usuario.Email = usuarioDM.Usuario.Email;
                        usuarioRolRepository.Update(usuario);
                        resultado = true;
                    }
                }            
                else
                {
                    usuario = new Usuario_Rol();
                    usuario.Usuario = new Usuario
                    {
                        Nombres = usuarioDM.Usuario.Nombres,
                        Apellidos = usuarioDM.Usuario.Apellidos,
                        Email = usuarioDM.Usuario.Email,
                        Clave = usuarioDM.Usuario.Clave,
                        ProviderKey = usuarioDM.Usuario.ProviderKey,
                        ProviderName = usuarioDM.Usuario.ProviderName,
                        //idPerfil = usuarioDM.Usuario.IdPerfil,
                        idStatus = usuarioDM.Usuario.IdStatus,
                        idUsuario = usuarioDM.Usuario.Id
                        
                        
                    };
                    usuario.Id_rol = usuarioDM.IdRol;
                    usuarioRolRepository.Insert(usuario);
                    resultado = true;
                }

            }
            return resultado;
        }

        public bool AddUser(UsuarioDomainModel usuarioDomainModel)
        {
            bool respuesta = false;

            if (usuarioDomainModel != null)
            {
                Usuario usuario = new Usuario();

                usuario.Nombres = usuarioDomainModel.Nombres;
                usuario.Apellidos = usuarioDomainModel.Apellidos;
                usuario.Email = usuarioDomainModel.Email;
                usuario.Clave = usuarioDomainModel.Clave;
                usuario.idPerfil = usuarioDomainModel.IdPerfil;
                usuario.idStatus = usuarioDomainModel.IdStatus;
                usuario.idUsuario = usuarioDomainModel.Id;
                usuario.area_movilizador = usuarioDomainModel.area_movilizador;

                usuarioRepository.Insert(usuario);
                respuesta = true;
            }

            return respuesta;
        }

        public UsuarioDomainModel ValidarLogin(string email, string password)
        {
            UsuarioDomainModel usuarioDM = null;
            
            try
            {
                Usuario usuario = usuarioRepository.SingleOrDefault(p => p.Email == email && p.Clave == password);
                if (usuario != null)
                {
                    usuarioDM = new UsuarioDomainModel();
                    usuarioDM.Id = usuario.Id;
                    usuarioDM.Nombres = usuario.Nombres;
                    usuarioDM.Apellidos = usuario.Apellidos;
                    usuarioDM.Clave = usuario.Clave;
                    usuarioDM.Email = usuario.Email;
                    usuarioDM.ProviderKey = usuario.ProviderKey;
                    usuarioDM.ProviderName = usuario.ProviderName;
                    List<UsuarioRolDomainModel> rolesDM = new List<UsuarioRolDomainModel>();
                    foreach (Usuario_Rol user in usuario.Usuario_Rol)
                    {
                        UsuarioRolDomainModel usuarioRolDM = new UsuarioRolDomainModel();
                        RolDomainModel rolDM = new RolDomainModel();
                        usuarioRolDM.IdUsuario = user.Id_Usuario;
                        usuarioRolDM.IdRol = user.Id_rol;
                        rolDM.Id = user.Rol.Id;
                        rolDM.Nombre = user.Rol.Nombre;
                        usuarioRolDM.Rol = rolDM;
                        rolesDM.Add(usuarioRolDM);
                    }
                    usuarioDM.UsuarioRoles = rolesDM;
                }
                return usuarioDM;
            }
            catch (Exception ex)
            {                
                return usuarioDM;
            }
            
        }


        //Método para administrador y superadministrador
        public List<UsuarioDomainModel> GetUsuarios()
        {
            List<Usuario> usuarios = usuarioRepository.GetAll(p => p.idStatus == 1 && p.idPerfil == 3).ToList();
            List<UsuarioDomainModel> usuarioDomainModels = new List<UsuarioDomainModel>();

            foreach (Usuario item in usuarios)
            {
                UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();
                usuarioDomainModel.Id = item.Id;
                usuarioDomainModel.Nombres = item.Nombres;
                usuarioDomainModel.Apellidos = item.Apellidos;
                usuarioDomainModel.Email = item.Email;
                usuarioDomainModel.NombreCompleto = item.Nombres + item.Apellidos;
                usuarioDomainModel.area_movilizador = item.area_movilizador;
                foreach (var rol in item.Usuario_Rol)
                {
                    UsuarioRolDomainModel usuarioRolDomainModel = new UsuarioRolDomainModel();
                    usuarioRolDomainModel.Rol = new RolDomainModel
                    {
                        Nombre = rol.Rol.Nombre
                    };
                    usuarioDomainModel.UsuarioRoles = new List<UsuarioRolDomainModel>();
                    usuarioDomainModel.UsuarioRoles.Add(usuarioRolDomainModel);
                }
                usuarioDomainModels.Add(usuarioDomainModel);
            }

            return usuarioDomainModels;
        }

        public List<UsuarioDomainModel> GetUsuariosByCoordinador(int idCoordinador)
        {
            List<Usuario> usuarios = usuarioRepository.GetAll(p => p.idStatus == 1 && p.idUsuario == idCoordinador).ToList();
            List<UsuarioDomainModel> usuarioDomainModels = new List<UsuarioDomainModel>();

            foreach (Usuario item in usuarios)
            {
                UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();
                usuarioDomainModel.Id = item.Id;
                usuarioDomainModel.Nombres = item.Nombres;
                usuarioDomainModel.Apellidos = item.Apellidos;
                usuarioDomainModel.Email = item.Email;
                usuarioDomainModel.NombreCompleto = item.Nombres + item.Apellidos;
                foreach (var rol in item.Usuario_Rol)
                {
                    UsuarioRolDomainModel usuarioRolDomainModel = new UsuarioRolDomainModel();
                    usuarioRolDomainModel.Rol = new RolDomainModel
                    {
                        Nombre = rol.Rol.Nombre
                    };
                    usuarioDomainModel.UsuarioRoles = new List<UsuarioRolDomainModel>();
                    usuarioDomainModel.UsuarioRoles.Add(usuarioRolDomainModel);
                }
                usuarioDomainModels.Add(usuarioDomainModel);
            }

            return usuarioDomainModels;
        }

        public UsuarioDomainModel GetUsuario(int id)
        {
            UsuarioDomainModel usuarioDomainModel = new UsuarioDomainModel();
            Usuario usuario = usuarioRepository.SingleOrDefault(p => p.Id == id);

            usuarioDomainModel.Id = usuario.Id;
            usuarioDomainModel.Nombres = usuario.Nombres;
            usuarioDomainModel.Apellidos = usuario.Apellidos;
            usuarioDomainModel.Email = usuario.Email;
            foreach (var item in usuario.Usuario_Rol)
            {
                UsuarioRolDomainModel usuarioRolDomainModel = new UsuarioRolDomainModel();

                usuarioRolDomainModel.Rol = new RolDomainModel
                {
                    Nombre = item.Rol.Nombre
                };
                usuarioDomainModel.UsuarioRoles = new List<UsuarioRolDomainModel>();
                usuarioDomainModel.UsuarioRoles.Add(usuarioRolDomainModel);
            }

            return usuarioDomainModel;

        }

        public List<string> GetUsuariosByApellidos(string apellidos)
        {
            List<Usuario> usuarios = usuarioRepository.GetAll(p => p.idStatus == 1 && p.Apellidos.Contains(apellidos)).ToList();
            List<string> usuarioDomainModels = new List<string>();

            foreach (Usuario item in usuarios)
            {
                usuarioDomainModels.Add(item.Apellidos);
            }

            return usuarioDomainModels;
        }

        public bool BajaUsuario(UsuarioDomainModel usuarioDM)
        {
            bool resultado = false;
            if (usuarioDM != null)
            {
                Usuario usuario = usuarioRepository.SingleOrDefault(p => p.Id == usuarioDM.Id);

                if (usuarioDM.Id > 0)
                {
                    usuario.idStatus = usuarioDM.IdStatus;
                    usuarioRepository.Update(usuario);
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool UpdateUsuario(UsuarioDomainModel usuarioDomainModel)
        {
            bool respuesta = false;

            if (usuarioDomainModel != null)
            {
                Usuario usuario = usuarioRepository.SingleOrDefault(p => p.Id == usuarioDomainModel.Id);

                if (usuario != null)
                {
                    if (usuario.Id > 0)
                    {
                        usuario.Nombres = usuarioDomainModel.Nombres;
                        usuario.Apellidos = usuarioDomainModel.Apellidos;
                        usuario.Email = usuarioDomainModel.Email;
                        usuarioRepository.Update(usuario);
                        respuesta = true;
                    }
                }
            }

            return respuesta;
        }

        #region Metodos de Actualizacion de la plataforma
        
        /// <summary>
        /// este metodo se encarga de validar el correo del usuario dentro de la plataforma para recuperar una contraseña
        /// </summary>
        /// <param name="email">el email el usuario a buscar dentro de la plataforma</param>
        /// <returns>una entidad del tipo usuarioDomainModel</returns>
        public UsuarioDomainModel ValidarEmailPasswordrecovery(string email)
        {
            UsuarioDomainModel usuarioDM = null;
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    Usuario usuario = usuarioRepository.SingleOrDefault(p => p.Email == email && p.Status.strValor == Recursos.RecursosBusiness.USUARIO_ACTIVO);
                    if (usuario != null)
                    {
                        usuarioDM = new UsuarioDomainModel();
                        usuarioDM.Id = usuario.Id;
                        usuarioDM.Nombres = usuario.Nombres;
                        usuarioDM.Apellidos = usuario.Apellidos;
                        usuarioDM.Clave = usuario.Clave;
                        usuarioDM.Email = usuario.Email;
                    }
                    return usuarioDM;
                }
                return usuarioDM;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                usuarioDM = new UsuarioDomainModel();
                usuarioDM.Id = 0;
                return usuarioDM;
            }

        }

         /// <summary>
        /// Este metodo se encarga de almacenar un movilizador dentro del sistema
        /// </summary>
        /// <param name="usuarioDM">el movilizador del sistema</param>
        /// <param name="perfilDM">el perfil del movilizador</param>
        /// <returns>una respuesta true/false</returns>
        public bool AddUpdateUsuarioMovilizador(UsuarioDomainModel usuarioDM, PerfilDomainModel perfilDM)
        {
            bool respuesta = false;
            Usuario_Rol user = null;

            try
            {
                if (usuarioDM != null)
                {
                    Usuario_Rol usuarioRol = usuarioRolRepository.SingleOrDefault(p => p.Id == usuarioDM.Id);

                    if (usuarioDM.Id > 0)
                    {

                    }
                    else
                    {
                        ///este metodo se encarga de buscar el rol del coordiandor que agrega al movilizador
                        user = usuarioRolRepository.SingleOrDefault(p => p.Id_Usuario == usuarioDM.IdUsuario);
                        usuarioRol = new Usuario_Rol();
                        usuarioRol.Usuario = new Usuario
                        {
                            Nombres = usuarioDM.Nombres,
                            Apellidos = usuarioDM.Apellidos,
                            Email = usuarioDM.Email,
                            Clave = usuarioDM.Clave,
                            ProviderKey = usuarioDM.ProviderKey,
                            ProviderName = usuarioDM.ProviderName,
                            idPerfil = perfilDM.Id,
                            idStatus = usuarioDM.IdStatus,
                            idUsuario = usuarioDM.IdUsuario
                        };

                        usuarioRol.Id_rol = user.Id_rol;
                        usuarioRol.dteFecha = DateTime.Now;
                        usuarioRolRepository.Insert(usuarioRol);
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;

            }
            return respuesta;
        }

        /// <summary>
        /// Se encarga de validar un perfil dentro del sistema
        /// </summary>
        /// <param name="idUsuario">el identificador del usuario</param>
        /// <returns>regresa un perfil</returns>
        public PerfilDomainModel ValidarPerfilMovilizador(int idUsuario)
        {
            PerfilDomainModel perfilDM = null;
            try
            {
                var usuario = usuarioRepository.SingleOrDefault(p => p.Id == idUsuario);
                var perfil = usuario.Perfil;
                perfilDM = new PerfilDomainModel();
                perfilDM.Id = perfil.id;
                perfilDM.StrValor = perfil.strValor;
                perfilDM.StrDescripcion = perfil.strDescripcion;
                return perfilDM;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return perfilDM;
            }

        }

        /// <summary>
        /// Este metodo se encarga de consultar a todos los coordinadores de la campaña por el area especifica
        /// </summary>
        /// <returns></returns>
        public List<UsuarioDomainModel> GetCoordinadores(int idRol)
        {
            List<UsuarioDomainModel> coordinadores = new List<UsuarioDomainModel>();
            try
            {
                List<Usuario> usuarios = usuarioRepository.GetAll().Where(p => p.Perfil.strValor == Recursos.RecursosBusiness.COORDINADOR_AREA && p.idStatus == 1).ToList<Usuario>();
                foreach (Usuario usuario in usuarios)
                {
                    UsuarioDomainModel usuarioDM = new UsuarioDomainModel();
                    usuarioDM.Nombres = usuario.Nombres;
                    usuarioDM.Apellidos = usuario.Apellidos;
                    usuarioDM.Email = usuario.Email;
                    usuarioDM.Id = usuario.Id;
                    // usuarioDM.IdUsuario = usuario.IdUsuario.Value;
                    foreach (Usuario_Rol item in usuario.Usuario_Rol)
                    {
                        UsuarioRolDomainModel usuarioRolDM = new UsuarioRolDomainModel();
                        usuarioRolDM.Id = item.Id;
                        usuarioRolDM.IdRol = item.Id_rol;
                        usuarioRolDM.IdUsuario = item.Id_Usuario;
                        RolDomainModel rolDM = new RolDomainModel();
                        rolDM.Id = item.Rol.Id;
                        rolDM.Nombre = item.Rol.Nombre;

                        usuarioRolDM.Rol = rolDM;
                        usuarioDM.UsuarioRoles.Add(usuarioRolDM);
                    }
                    coordinadores.Add(usuarioDM);
                }
                return coordinadores.Where(p => p.IdRol == idRol).ToList();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return coordinadores;
            }
        }


        public List<UsuarioDomainModel> GetAllCoordinadores(int idRol)
        {
            List<UsuarioDomainModel> coordinadores = new List<UsuarioDomainModel>();
            try
            {
                List<Usuario> usuarios = usuarioRepository.GetAll().Where(p => p.idStatus == 1 && p.idPerfil == 1).ToList<Usuario>();
                List<UsuarioRolDomainModel> usuariosRoles = new List<UsuarioRolDomainModel>();
                foreach (Usuario usuario in usuarios)
                {
                    UsuarioDomainModel usuarioDM = new UsuarioDomainModel();
                    usuarioDM.Nombres = usuario.Nombres;
                    usuarioDM.Apellidos = usuario.Apellidos;
                    usuarioDM.Email = usuario.Email;
                    usuarioDM.Id = usuario.Id;
                    foreach (Usuario_Rol item in usuario.Usuario_Rol)
                    {
                        usuarioDM.IdRol = item.Id_rol;
                        UsuarioRolDomainModel usuarioRolDM = new UsuarioRolDomainModel();
                        //usuarioRolDM.Id = item.Id;
                        //usuarioRolDM.IdRol = item.Id_rol;
                        //usuarioRolDM.IdUsuario = item.Id_Usuario;
                        RolDomainModel rolDM = new RolDomainModel();
                        rolDM.Id = item.Rol.Id;
                        rolDM.Nombre = item.Rol.Nombre;
                        usuarioRolDM.Rol = rolDM;
                        usuariosRoles.Add(usuarioRolDM);

                    }
                    usuarioDM.UsuarioRoles = usuariosRoles;
                    coordinadores.Add(usuarioDM);

                }
                return coordinadores.Where(p => p.IdRol == idRol).ToList();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return coordinadores;
            }
        }


        /// <summary>
        /// Este metodo se encarga de contar cuantos coordinadores se tienen hasta el momento
        /// </summary>
        /// <returns>
        /// regresa el total de coordinadores activos
        /// </returns>
        public int CountUsuariosCoordinadoresTotal()
        {
            try
            {
                return usuarioRepository.GetAll(p => p.idStatus == 1 && p.idPerfil == 1).Count();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return 0;
            }
        }



        /// <summary>
        /// Este metodo se encarga de traer a todos los movilizados de  cada uno de los movilizadores existentes
        /// se utiliza un stroreprocedure para la llamada a la consulta compleja
        /// </summary>
        /// <param name="idCoordinador">el identificador del coordinador</param>
        /// <returns>una lista de usuarios domain model</returns>
        public List<UsuarioDomainModel> GetMovilizadoresByCoordinador(int idCoordinador)
        {

            List<UsuarioDomainModel> usersDM = new List<UsuarioDomainModel>(); //lista vacia
            try
            {
                List<Usuario> usuarios = usuarioRepository.GetAll().Where(p => p.idUsuario == idCoordinador).ToList();

                foreach (Usuario user in usuarios)
                {
                    List<MovilizadoDomainModel> movilizados = new List<MovilizadoDomainModel>();//lista vacia
                    UsuarioDomainModel usuarioDM = new UsuarioDomainModel();
                    usuarioDM.Id = user.Id;
                    usuarioDM.IdPerfil = user.idPerfil.Value;
                    usuarioDM.IdStatus = user.idStatus.Value;
                    usuarioDM.Nombres = user.Nombres;
                    usuarioDM.Apellidos = user.Apellidos;
                    int IdUsuarioMovilizador = user.Id;

                    var movilizadores = movilizadoRepository.GetAll().Where(p => p.Usuario.idPerfil == 2 && p.idStatus == 1 && p.idUsuario == IdUsuarioMovilizador).ToList();
                    foreach (var m in movilizadores)
                    {

                        if (m.Usuario.idUsuario == idCoordinador)
                        {
                            MovilizadoDomainModel movilizado = new MovilizadoDomainModel(); ///creo un objeto movilizado vacio
                            //movilizado.id = m.id;
                            movilizado.StrNombre = m.strNombre;
                            movilizado.StrApellidoPaterno = m.strApellidoPaterno;
                            movilizado.StrApellidoMaterno = m.strApellidoMaterno;
                            movilizado.IdUsuario = m.idUsuario.Value;
                            //movilizado.I = m.idDireccion;
                            movilizado.DireccionDomainModel = new DireccionDomainModel();//creamos la direccion  de forma vacia para ser llenada
                            movilizado.DireccionDomainModel.StrCalle = m.Direccion.strCalle;
                            movilizado.DireccionDomainModel.StrNumeroInterior = m.Direccion.strNumeroInterior;
                            movilizado.DireccionDomainModel.IdColonia = m.Direccion.idColonia.Value;
                            movilizado.DireccionDomainModel.ColoniaDomainModel = new ColoniaDomainModel();
                            movilizado.DireccionDomainModel.ColoniaDomainModel.StrCodigoPostal = m.Direccion.Colonia.strCodigoPostal.Value.ToString();
                            movilizado.DireccionDomainModel.ColoniaDomainModel.StrTipoDeAsentamiento = m.Direccion.Colonia.strTipoDeAsentamiento;
                            movilizado.DireccionDomainModel.ColoniaDomainModel.StrAsentamiento = m.Direccion.Colonia.strAsentamiento;
                            movilizado.TelefonoDomainModel = new TelefonoDomainModel();
                            movilizado.TelefonoDomainModel.StrNumeroCelular = m.Telefono.strNumeroCelular;
                            movilizados.Add(movilizado);//agregamos el movilizado a la lista
                        }


                    }

                    usuarioDM.Movilizados = movilizados;
                    usersDM.Add(usuarioDM);///agrego a la lista de usuarios un usuario con su movilizador

                }

                return usersDM;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return usersDM;
            }
        }




        #endregion
    }
}
