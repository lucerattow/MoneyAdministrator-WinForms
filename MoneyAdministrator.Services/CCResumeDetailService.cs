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
    public class CCResumeDetailService : IService<CCResumeDetail>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CCResumeDetailService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<CCResumeDetail> GetAll()
        {
            return _unitOfWork.CCResumeDetailRepository.GetAll().ToList();
        }

        public CCResumeDetail Get(int id)
        {
            return _unitOfWork.CCResumeDetailRepository.GetById(id);
        }

        public void Insert(CCResumeDetail model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el resumen existe
            var ccResume = _unitOfWork.CCResumeRepository.GetById(model.CCResumeId);
            if (ccResume == null)
                throw new Exception("There is no credit card resume with that id");

            //Agrego el modelo a la base de datos
            _unitOfWork.CCResumeDetailRepository.Insert(model);
            _unitOfWork.Save();
        }

        public void Update(CCResumeDetail model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            var item = _unitOfWork.CCResumeDetailRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CCResumeDetailRepository.Update(model);
                _unitOfWork.Save();
            }
        }

        public void Delete(CCResumeDetail model)
        {
            var item = _unitOfWork.CCResumeDetailRepository.GetById(model.Id);
            if (item != null)
            {
                _unitOfWork.CCResumeDetailRepository.Delete(item);
                _unitOfWork.Save();
            }
        }
    }
}
