using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services.Interfaces
{
    public interface IService<TEntity>
    {
        List<TEntity> GetAll();
        TEntity Get(int id);
        void Insert(TEntity model);
        void Update(TEntity model);
        void Delete(TEntity model);
    }
}
