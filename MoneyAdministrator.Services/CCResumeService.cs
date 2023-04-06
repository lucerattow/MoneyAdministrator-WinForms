using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.DataAccess;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services
{
    public class CCResumeService : IService<CCResume>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CCResumeService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<CCResume> GetAll()
        {
            return _unitOfWork.CCResumeRepository.GetAll().ToList();
        }

        public CCResume Get(int id)
        {
            return _unitOfWork.CCResumeRepository.GetById(id);
        }

        public void Insert(CCResume model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el objeto ya existe
            var item = _unitOfWork.CCResumeRepository.GetAll()
                .Where(x => x.Period == model.Period).FirstOrDefault();

            if (item != null)
            {
                //Si el objeto ya existe, añado el id en el modelo
                model.Id = item.Id;
            }
            else
            {
                //Agrego el modelo a la base de datos
                _unitOfWork.CCResumeRepository.Insert(model);
                _unitOfWork.Save();
            }
        }

        public void Update(CCResume model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CCResumeRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CCResumeRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(CCResume model)
        {
            var item = _unitOfWork.CCResumeRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CCResumeRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
