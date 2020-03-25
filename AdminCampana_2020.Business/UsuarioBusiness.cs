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
        public UsuarioBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            usuarioRepository = new UsuarioRepository(unitOfWork);
            usuarioRolRepository = new UsuarioRolRepository(unitOfWork);
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

      
    }
}
