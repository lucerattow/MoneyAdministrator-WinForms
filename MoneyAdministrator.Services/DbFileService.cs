using MoneyAdministrator.DataAccess;
using MoneyAdministrator.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services
{
    public static class DbFileService
    {
        public static void CreateDatabase(string databasePath)
        {
            _ = new UnitOfWork(databasePath);
        }
    }
}
