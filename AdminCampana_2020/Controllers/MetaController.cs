using AdminCampana_2020.Business.Interface;
using AdminCampana_2020.Domain;
using AdminCampana_2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminCampana_2020.Controllers
{
    public class MetaController : Controller
    {

        private IMetaBusiness ImetaBusiness;
        private IMovilizadoBusiness movilizadoBusiness;

        public MetaController(IMetaBusiness metaBusiness, IMovilizadoBusiness movilizadoBusiness)
        {
            this.ImetaBusiness = metaBusiness;
            this.movilizadoBusiness = movilizadoBusiness;
        }

        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetMetaEstablecida()
        {
            List<MetaVM> metasVM = null;
            List<MetaDomainModel> metasDM = new List<MetaDomainModel>();

            metasDM = ImetaBusiness.GetAllMetas();

            if (metasDM.Count > 0)
            {
                metasVM = new List<MetaVM>();
                AutoMapper.Mapper.Map(metasDM, metasVM);
            }

            
            return Json(metasVM,JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateMeta(MetaVM metaVm)
        {

            if (metaVm.Id > 0)
            {
                MetaDomainModel metaDM = new MetaDomainModel();
                AutoMapper.Mapper.Map(metaVm, metaDM);
                ImetaBusiness.UpdateMeta(metaDM);
            }
           

            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetMovilizados()
        {
            int[] totalMovilizados = null;
            int[] totalMetas = null;
            int[][] dataDash = null;

            List<MovilizadoDomainModel> movilizados = movilizadoBusiness.GetAllMovilizados();
            List<MetaDomainModel> metas = ImetaBusiness.GetAllMetas();

            if (movilizados != null && metas != null)
            {
                totalMovilizados = new int[5];
                totalMetas = new int[5];

                for (int i = 0; i < metas.Count; i++)
                {
                    totalMetas[i] = metas[i].meta;
                }

                for (int i = 0; i < movilizados.Count; i++)
                {
                    if (movilizados[i].Usuario.UsuarioRoles.Select(p => p.IdRol.Equals(1)).Contains(true))
                    {
                        totalMovilizados[0]++;
                    }
                    else if (movilizados[i].Usuario.UsuarioRoles.Select(p => p.IdRol.Equals(2)).Contains(true))
                    {
                        totalMovilizados[1]++;
                    }
                    else if (movilizados[i].Usuario.UsuarioRoles.Select(p => p.IdRol.Equals(3)).Contains(true))
                    {
                        totalMovilizados[2]++;
                    }
                    else if (movilizados[i].Usuario.UsuarioRoles.Select(p => p.IdRol.Equals(4)).Contains(true))
                    {
                        totalMovilizados[3]++;
                    }
                    else if (movilizados[i].Usuario.UsuarioRoles.Select(p => p.IdRol.Equals(5)).Contains(true))
                    {
                        totalMovilizados[4]++;
                    }
                }

                dataDash = new int[][]
                {
                    totalMetas,
                    totalMovilizados,                
                };

            }

            return Json(dataDash, JsonRequestBehavior.AllowGet);
        }
    }
}