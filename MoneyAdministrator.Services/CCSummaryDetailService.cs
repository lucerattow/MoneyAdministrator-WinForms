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
    public class CCSummaryDetailService : IService<CCSummaryDetail>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CCSummaryDetailService(string databasePath)
        {
            _unitOfWork = new UnitOfWork(databasePath);
        }

        public List<CCSummaryDetail> GetAll()
        {
            return _unitOfWork.CCResumeDetailRepository.GetAll().ToList();
        }

        public CCSummaryDetail Get(int id)
        {
            return _unitOfWork.CCResumeDetailRepository.GetById(id);
        }

        public void Insert(CCSummaryDetail model)
        {
            //Valido el modelo
            Utilities.ModelValidator.Validate(model);

            //Compruebo si el resumen existe
            var ccResume = _unitOfWork.CCResumeRepository.GetById(model.CCSummaryId);
            if (ccResume == null)
                throw new Exception("There is no credit card resume with that id");

            //Agrego el modelo a la base de datos
            _unitOfWork.CCResumeDetailRepository.Insert(model);
            _unitOfWork.Save();
        }

        public void Update(CCSummaryDetail model)
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

        public void Delete(CCSummaryDetail model)
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
