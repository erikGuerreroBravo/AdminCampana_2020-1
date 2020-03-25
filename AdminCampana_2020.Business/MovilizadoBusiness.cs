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
    public class MovilizadoBusiness: IMovilizadoBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MovilizadoRepository movilizadoRepository;

        public MovilizadoBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            movilizadoRepository = new MovilizadoRepository(unitOfWork);

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
                        movilizado.Direccion.strCalle = personaDM.Direccion.StrCalle;
                        movilizado.Direccion.strNumeroExterior = personaDM.Direccion.StrNumeroExterior;
                        movilizado.Direccion.idColonia = personaDM.Direccion.idColonia;
                        movilizado.Telefono.strNumeroCelular = personaDM.Telefono.StrNumeroCelular;

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
                    movilizado.idUsuario = personaDM.idUsuario;
                    movilizado.idStatus = personaDM.idStatus;
                    movilizado.Direccion = new Direccion
                    {
                        strCalle = personaDM.Direccion.StrCalle,
                        strNumeroExterior = personaDM.Direccion.StrNumeroExterior,
                        idColonia = personaDM.Direccion.idColonia
                        
                    };
                    movilizado.Telefono = new Telefono
                    {
                        strNumeroCelular = personaDM.Telefono.StrNumeroCelular
                    };
                    movilizadoRepository.Insert(movilizado);
                    resultado = true;
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
                    movilizado.Telefono.strNumeroCelular = movilizadoDM.Telefono.StrNumeroCelular;
                    movilizadoRepository.Update(movilizado);
                    resultado = "Se Actualizó correctamente";
                }
            }
            return resultado;
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
                personaDM.Telefono = new TelefonoDomainModel
                {
                    StrNumeroCelular = movilizado.Telefono.strNumeroCelular
                };
                personaDM.Direccion = new DireccionDomainModel
                {
                    StrCalle = movilizado.Direccion.strCalle,
                    StrNumeroExterior = movilizado.Direccion.strNumeroExterior,
                    idColonia = movilizado.Direccion.idColonia.Value,
                    Colonia = new ColoniaDomainModel
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
    }
}
