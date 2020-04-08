using AdminCampana_2020.Business.Enum;
using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.Repository;
using AdminCampana_2020.Repository.Infraestructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Business
{
    public class MovilizadoBusiness: IMovilizadoBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MovilizadoRepository movilizadoRepository;
        private readonly RolRepository rolRepository;

        public MovilizadoBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            movilizadoRepository = new MovilizadoRepository(unitOfWork);
            rolRepository = new RolRepository(unitOfWork);

        }

        /// <summary>
        /// Metodo se encarga de insertar o actualizar un registro de la entidad personaDM
        /// </summary>
        /// <param name="personaDM">Entidad PersonaDM</param>
        /// <returns>una cadena con el valor de la operación</returns>
        public bool AddUpdateMovilizado(MovilizadoDomainModel personaDM)
        {
            bool resultado = false;
            if (personaDM != null)
            {

                Movilizado movilizado = movilizadoRepository.SingleOrDefault(p => p.id == personaDM.Id);

                if (movilizado != null)
                {
                    if (movilizado.id > 0)
                    {
                        movilizado.strNombre = personaDM.StrNombre;
                        movilizado.strApellidoPaterno = personaDM.StrApellidoPaterno;
                        movilizado.strApellidoMaterno = personaDM.StrApellidoMaterno;
                        movilizado.strEmail = personaDM.StrEmail;
                        movilizado.Direccion.strCalle = personaDM.DireccionDomainModel.StrCalle;
                        movilizado.Direccion.strNumeroExterior = personaDM.DireccionDomainModel.StrNumeroExterior;
                        movilizado.Direccion.idColonia = personaDM.DireccionDomainModel.IdColonia;
                        movilizado.Telefono.strNumeroCelular = personaDM.TelefonoDomainModel.StrNumeroCelular;

                        movilizadoRepository.Update(movilizado);
                        resultado = true;

                    }
                }
                

                else
                {
                    movilizado = new Movilizado();
                    movilizado.strNombre = personaDM.StrNombre;
                    movilizado.strApellidoPaterno = personaDM.StrApellidoPaterno;
                    movilizado.strApellidoMaterno = personaDM.StrApellidoMaterno;
                    movilizado.strEmail = personaDM.StrEmail;
                    movilizado.idUsuario = personaDM.IdUsuario;
                    movilizado.idStatus = personaDM.idStatus;
                    movilizado.Direccion = new Direccion
                    {
                        strCalle = personaDM.DireccionDomainModel.StrCalle,
                        strNumeroExterior = personaDM.DireccionDomainModel.StrNumeroExterior,
                        idColonia = personaDM.DireccionDomainModel.IdColonia
                        
                    };
                    movilizado.Telefono = new Telefono
                    {
                        strNumeroCelular = personaDM.TelefonoDomainModel.StrNumeroCelular
                    };
                    movilizadoRepository.Insert(movilizado);
                    resultado = true;
                }

            }
            return resultado;
        }

        public string UpdatePersonal(MovilizadoDomainModel personaDM)
        {
            string resultado = string.Empty;
            if (personaDM != null)
            {
                Movilizado persona = movilizadoRepository.SingleOrDefault(p => p.id == personaDM.Id);

                if (persona != null)
                {
                    persona.strNombre = personaDM.StrNombre;
                    persona.strApellidoPaterno = personaDM.StrApellidoPaterno;
                    persona.strApellidoMaterno = personaDM.StrApellidoMaterno;
                    persona.strEmail = personaDM.StrEmail;
                    persona.Telefono = new Telefono();
                    persona.Telefono.strNumeroCelular = personaDM.TelefonoDomainModel.StrNumeroCelular;
                    movilizadoRepository.Update(persona);
                    resultado = "Se Actualizo correctamente";
                }
            }
            return resultado;
        }

        public string UpdateMovilizado(MovilizadoDomainModel movilizadoDM)
        {
            string resultado = string.Empty;
            if (movilizadoDM != null)
            {
                Movilizado movilizado = movilizadoRepository.SingleOrDefault(p => p.id == movilizadoDM.Id);

                if (movilizado != null)
                {
                    movilizado.strNombre = movilizadoDM.StrNombre;
                    movilizado.strApellidoPaterno = movilizadoDM.StrApellidoPaterno;
                    movilizado.strApellidoMaterno = movilizadoDM.StrApellidoMaterno;
                    movilizado.strEmail = movilizadoDM.StrEmail;
                    movilizado.Telefono = new Telefono();
                    movilizado.Telefono.strNumeroCelular = movilizadoDM.TelefonoDomainModel.StrNumeroCelular;
                    movilizadoRepository.Update(movilizado);
                    resultado = "Se Actualizó correctamente";
                }
            }
            return resultado;
        }


        public List<MovilizadoDomainModel> GetAllPersonas()
        {
            List<MovilizadoDomainModel> personas = null;
            personas = movilizadoRepository.GetAll().Select(p => new MovilizadoDomainModel
            {
                Id = p.id,
                StrNombre = p.strNombre,
                StrApellidoPaterno = p.strApellidoPaterno,
                StrApellidoMaterno = p.strApellidoMaterno,
                StrEmail = p.strEmail,

            }).OrderByDescending(p => p.StrNombre).ToList<MovilizadoDomainModel>();
            return personas;
        }


        public MovilizadoDomainModel GetPersonaById(int id)
        {
            Movilizado persona = movilizadoRepository.SingleOrDefault(p => p.id == id);
            if (persona != null)
            {
                MovilizadoDomainModel personaDM = new MovilizadoDomainModel();
                personaDM.Id = persona.id;
                personaDM.StrNombre = persona.strNombre;
                personaDM.StrApellidoPaterno = persona.strApellidoPaterno;
                personaDM.StrApellidoMaterno = persona.strApellidoMaterno;
                personaDM.StrEmail = persona.strEmail;

                TelefonoDomainModel telefonoDM = new TelefonoDomainModel();
                telefonoDM.StrNumeroCelular = persona.Telefono.strNumeroCelular;
                personaDM.TelefonoDomainModel = telefonoDM;
                return personaDM;
            }
            else
            {
                return null;
            }


        }
        
        public List<MovilizadoDomainModel> GetAllMovilizados()
        {
            List<MovilizadoDomainModel> personas = new List<MovilizadoDomainModel>();
            List<Movilizado> movilizados = movilizadoRepository.GetAll().Where(p => p.idStatus == 1).ToList();

            foreach (Movilizado item in movilizados)
            {
                MovilizadoDomainModel movilizadoDomainModel = new MovilizadoDomainModel();

                movilizadoDomainModel.Id = item.id;
                movilizadoDomainModel.StrNombre = item.strNombre;
                movilizadoDomainModel.StrApellidoPaterno = item.strApellidoPaterno;
                movilizadoDomainModel.StrApellidoMaterno = item.strApellidoMaterno;
                movilizadoDomainModel.StrEmail = item.strEmail;

                movilizadoDomainModel.Usuario = new UsuarioDomainModel
                {
                    Id = item.Usuario.Id,
                    Nombres = item.Usuario.Nombres,
                    Apellidos = item.Usuario.Apellidos,
                    area_movilizador = item.Usuario.area_movilizador
                    //UsuarioRoles = item.Usuario.Usuario_Rol as List<UsuarioRolDomainModel>
                };

                foreach (var rol in item.Usuario.Usuario_Rol)
                {
                    UsuarioRolDomainModel usuarioRolDomainModel = new UsuarioRolDomainModel();
                    usuarioRolDomainModel.IdRol = rol.Id_rol;
                    usuarioRolDomainModel.Rol = new RolDomainModel
                    {
                        Nombre = rol.Rol.Nombre
                    };
                    movilizadoDomainModel.Usuario.UsuarioRoles = new List<UsuarioRolDomainModel>();
                    movilizadoDomainModel.Usuario.UsuarioRoles.Add(usuarioRolDomainModel);

                }
                personas.Add(movilizadoDomainModel);
            }


            return personas;
        }

        public List<MovilizadoDomainModel> GetAllMovilizados(int idUsuario)
        {
            List<MovilizadoDomainModel> personas = new List<MovilizadoDomainModel>();
            List<Movilizado> movilizados = movilizadoRepository.GetAll().Where(p => p.idStatus == 1 && p.idUsuario == idUsuario).ToList();

            foreach (Movilizado item in movilizados)
            {
                MovilizadoDomainModel movilizadoDomainModel = new MovilizadoDomainModel();

                movilizadoDomainModel.Id = item.id;
                movilizadoDomainModel.StrNombre = item.strNombre;
                movilizadoDomainModel.StrApellidoPaterno = item.strApellidoPaterno;
                movilizadoDomainModel.StrApellidoMaterno = item.strApellidoMaterno;
                movilizadoDomainModel.StrEmail = item.strEmail;

                movilizadoDomainModel.Usuario = new UsuarioDomainModel
                {
                    Id = item.Usuario.Id,
                    Nombres = item.Usuario.Nombres,
                    Apellidos = item.Usuario.Apellidos,
                    //UsuarioRoles = item.Usuario.Usuario_Rol as List<UsuarioRolDomainModel>
                };

                foreach (var rol in item.Usuario.Usuario_Rol)
                {
                    UsuarioRolDomainModel usuarioRolDomainModel = new UsuarioRolDomainModel();
                    usuarioRolDomainModel.IdRol = rol.Id_rol;
                    usuarioRolDomainModel.Rol = new RolDomainModel
                    {
                        Nombre = rol.Rol.Nombre
                    };
                    movilizadoDomainModel.Usuario.UsuarioRoles = new List<UsuarioRolDomainModel>();
                    movilizadoDomainModel.Usuario.UsuarioRoles.Add(usuarioRolDomainModel);

                }
                personas.Add(movilizadoDomainModel);
            }


            return personas;
        }

        public List<MovilizadoDomainModel> GetMovilizadosByCoordinador(int idCoordinador)
        {
            List<MovilizadoDomainModel> movilizadoDomainModels = new List<MovilizadoDomainModel>();

            List<Movilizado> movilizados = movilizadoRepository.GetAll().Where(p => p.Usuario.idUsuario == idCoordinador).ToList();

            foreach (Movilizado item in movilizados)
            {
                MovilizadoDomainModel movilizadoDomainModel = new MovilizadoDomainModel();

                movilizadoDomainModel.Id = item.id;
                movilizadoDomainModel.StrNombre = item.strNombre;
                movilizadoDomainModel.StrApellidoPaterno = item.strApellidoPaterno;
                movilizadoDomainModel.StrApellidoMaterno = item.strApellidoMaterno;
                movilizadoDomainModel.StrEmail = item.strEmail;

                movilizadoDomainModel.Usuario = new UsuarioDomainModel
                {
                    Id = item.Usuario.Id,
                    Nombres = item.Usuario.Nombres,
                    Apellidos = item.Usuario.Apellidos,
                };

                foreach (var rol in item.Usuario.Usuario_Rol)
                {
                    UsuarioRolDomainModel usuarioRolDomainModel = new UsuarioRolDomainModel();
                    usuarioRolDomainModel.IdRol = rol.Id_rol;
                    usuarioRolDomainModel.Rol = new RolDomainModel
                    {
                        Nombre = rol.Rol.Nombre
                    };
                    movilizadoDomainModel.Usuario.UsuarioRoles = new List<UsuarioRolDomainModel>();
                    movilizadoDomainModel.Usuario.UsuarioRoles.Add(usuarioRolDomainModel);

                }
                movilizadoDomainModels.Add(movilizadoDomainModel);
            }

            return movilizadoDomainModels;
        }

        public MovilizadoDomainModel GetMovilizadoById(int id)
        {
            Movilizado movilizado = movilizadoRepository.SingleOrDefault(p => p.id == id);
        
            if (movilizado != null)
            {
                MovilizadoDomainModel personaDM = new MovilizadoDomainModel();
                personaDM.Id = movilizado.id;
                personaDM.StrNombre = movilizado.strNombre;
                personaDM.StrApellidoPaterno = movilizado.strApellidoPaterno;
                personaDM.StrApellidoMaterno = movilizado.strApellidoMaterno;
                personaDM.StrEmail = movilizado.strEmail;
                personaDM.TelefonoDomainModel = new TelefonoDomainModel
                {
                    StrNumeroCelular = movilizado.Telefono.strNumeroCelular
                };
                personaDM.DireccionDomainModel = new DireccionDomainModel
                {
                    StrCalle = movilizado.Direccion.strCalle,
                    StrNumeroExterior = movilizado.Direccion.strNumeroExterior,
                    IdColonia = movilizado.Direccion.idColonia.Value,
                    ColoniaDomainModel = new ColoniaDomainModel
                    {
                        StrAsentamiento = movilizado.Direccion.Colonia.strAsentamiento,
                        Seccion = new SeccionDomainModel
                        {
                            StrNombre = movilizado.Direccion.Colonia.Seccion.strNombre,
                            Zona = new ZonaDomainModel
                            {
                                StrNombre = movilizado.Direccion.Colonia.Seccion.Zona.strNombre
                            }
                        }
                    }
                };
                return personaDM;
            }
            else
            {
                return null;
            }


        }
        public bool MigrarMovilizados(UsuarioDomainModel usuarioDomainModel)
        {

            bool respuesta = false;

            if (usuarioDomainModel != null)
            {
                List<Movilizado> movilizado = movilizadoRepository.GetAll().Where(p => p.idUsuario == usuarioDomainModel.Id).ToList() ;

                if (movilizado != null)
                {
                    foreach (var item in movilizado)
                    {

                        item.idUsuario = usuarioDomainModel.idCambio;
                    }

                    movilizadoRepository.UpdateAll(movilizado);
                }
            }

            return respuesta;

        }

        public bool BajaMovilizado(MovilizadoDomainModel usuarioDM)
        {
            bool resultado = false;
            if (usuarioDM != null)
            {
                Movilizado usuario = movilizadoRepository.SingleOrDefault(p => p.id == usuarioDM.Id);

                if (usuarioDM.Id > 0)
                {
                    usuario.idStatus = usuarioDM.idStatus;
                    movilizadoRepository.Update(usuario);
                    resultado = true;
                }
            }
            return resultado;
        }

        /// <summary>
        /// Este metodo se encarga de consultar todos los roles de un movilizador sin incluir el rol de administrador o super administrador
        /// </summary>
        /// <returns>una lista de roles sin incluier el rol de administrador y super administrador</returns>
        public List<RolDomainModel> ObtenerRolesMovilizador()
        {

            List<RolDomainModel> roles = new List<RolDomainModel>();
            List<Rol> totalRoles = rolRepository.GetAll().Where(p => p.Nombre != "Administrador").ToList<Rol>();
            foreach (var item in totalRoles)
            {
                if (item.Nombre != "Super Administrador")
                {
                    RolDomainModel rolDM = new RolDomainModel();
                    rolDM.Id = item.Id;
                    rolDM.Nombre = item.Nombre;
                    roles.Add(rolDM);
                }

            }
            RolDomainModel rolDomainModelBase = new RolDomainModel();
            rolDomainModelBase.Id = 0;
            rolDomainModelBase.Nombre = "Seleccionar";
            roles.Insert(0, rolDomainModelBase);
            return roles;
        }

        /// <summary>
        /// Este metodo se encarga de consultar el total de registros pertenecientes a una area en especifico
        /// </summary>
        /// <returns>una cantidad total de registros</returns>
        public int TotalByAreaCoordinadores(string area)
        {
            int total = 0;
            try
            {
                var roles = rolRepository.GetAll().Where(p => p.Nombre == area).ToList();
                foreach (var item in roles)
                {
                    foreach (var userRoles in item.Usuario_Rol)
                    {
                        foreach (var movilizado in userRoles.Usuario.Movilizado)
                        {
                            if (movilizado.idStatus == 1 && movilizado.Usuario.Perfil.strValor == Recursos.RecursosBusiness.COORDINADOR_AREA)
                            {
                                total++;
                            }
                        }
                    }
                }
                return total;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Este metodo se encarga de verificar que no exista un registro duplicado
        /// </summary>
        /// <param name="movilizadoDM">la entidad a evalaur en bd</param>
        /// <returns>true/false</returns>
        public bool ValidarExisteMovilizado(MovilizadoDomainModel movilizadoDM)
        {
            bool respuesta = true;
            try
            {
                Expression<Func<Movilizado, bool>> predicado = p => p.strNombre == movilizadoDM.StrNombre &&
                 p.strApellidoPaterno == movilizadoDM.StrApellidoPaterno && p.strApellidoMaterno == movilizadoDM.StrApellidoMaterno &&
                 p.Telefono.strNumeroCelular == movilizadoDM.TelefonoDomainModel.StrNumeroCelular
                 && p.Direccion.strCalle == movilizadoDM.DireccionDomainModel.StrCalle
                 && p.Direccion.strNumeroInterior == movilizadoDM.DireccionDomainModel.StrNumeroInterior
                 && p.Direccion.idColonia == movilizadoDM.DireccionDomainModel.IdColonia && p.idStatus == (int)EnumStatus.Activo;
                //retorno false si no encuentra a nadie con esos datos
                respuesta = movilizadoRepository.Exists(predicado);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return respuesta;
        }


        /// <summary>
        /// Este metodo se encarga de contar cuantos movilizados se tienen hasta el momento
        /// </summary>
        /// <returns>
        /// regresa el total de movilizados activos
        /// </returns>
        public int CountMovilizadosTotal()
        {
            try
            {
                return movilizadoRepository.GetAll().Count(p => p.idStatus == 1);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                return 0;
            }
        }




    }
}
