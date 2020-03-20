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

                else
                {
                    movilizado = new Movilizado();
                    movilizado.strNombre = personaDM.StrNombre;
                    movilizado.strApellidoPaterno = personaDM.StrApellidoPaterno;
                    movilizado.strApellidoMaterno = personaDM.StrApellidoMaterno;
                    movilizado.strEmail = personaDM.StrEmail;
                    movilizado.idUsuario = personaDM.idUsuario;
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
                    resultado = "Se Actualizo correctamente";
                }
            }
            return resultado;
        }


        public List<MovilizadoDomainModel> GetAllMovilizados()
        {
            List<MovilizadoDomainModel> personas = null;
            personas = movilizadoRepository.GetAll().Select(p => new MovilizadoDomainModel
            {
                Id = p.id,
                StrNombre = p.strNombre,
                StrApellidoPaterno = p.strApellidoPaterno,
                StrApellidoMaterno = p.strApellidoMaterno,
                StrEmail = p.strEmail,
                
            }).OrderByDescending(p=>p.StrNombre).ToList<MovilizadoDomainModel>();
            return personas;
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
    }
}
