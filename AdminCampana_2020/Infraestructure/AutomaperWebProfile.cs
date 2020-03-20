using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminCampana_2020.Domain;
using AdminCampana_2020.Repository;
using AdminCampana_2020.ViewModels;
using AutoMapper;

namespace AdminCampana_2020.Infraestructure
{
    public class AutomaperWebProfile : AutoMapper.Profile
    {

        public AutomaperWebProfile()
        {
            ///entidad movilizado
            CreateMap<MovilizadoVM, MovilizadoDomainModel>();
            CreateMap<MovilizadoDomainModel, MovilizadoVM>();
            
            //entidad direccion
            CreateMap<DireccionVM, DireccionDomainModel>();
            CreateMap<DireccionDomainModel, DireccionVM>();
            
            //entidad telefono
            CreateMap<TelefonoVM, TelefonoDomainModel>();
            CreateMap<TelefonoDomainModel, TelefonoVM>();
            
            //entidad colonia
            CreateMap<ColoniaVM, ColoniaDomainModel>();
            CreateMap<ColoniaDomainModel, ColoniaVM>();
            
            //seccion
            CreateMap<SeccionVM, SeccionDomainModel>();
            CreateMap<SeccionDomainModel, SeccionVM>();
            
            //zona
            CreateMap<ZonaVM, ZonaDomainModel>();
            CreateMap<ZonaDomainModel, ZonaVM>();

            //Usuario
            CreateMap<UsuarioVM, UsuarioDomainModel>();
            CreateMap<UsuarioDomainModel, UsuarioVM>();
            
            //Roles
            CreateMap<RolVM, RolDomainModel>();
            CreateMap<RolDomainModel, RolVM>();
            
            //UsuarioRoles
            CreateMap<UsuarioRolVM, UsuarioRolDomainModel>();
            CreateMap<UsuarioRolDomainModel, UsuarioRolVM>();

            //Metas
            CreateMap<MetaVM,MetaDomainModel>();
            CreateMap<MetaDomainModel,MetaVM>();

            //Perfil
            CreateMap<PerfilVM, PerfilDomainModel>();
            CreateMap<PerfilDomainModel, PerfilVM>();

            //Status

            CreateMap<StatusVM,StatusDomainModel>();
            CreateMap<StatusDomainModel,StatusVM>();

        }

        public static void Run()
        {

            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutomaperWebProfile>();
            });
        }


    }
}