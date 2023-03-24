using MoneyAdministrator.Presenters;
using Microsoft.Extensions.DependencyInjection;
using MyMoneyAdmin;
using MoneyAdministrator.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using MoneyAdministrator.Interfaces;

namespace MoneyAdministrator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IMainView view = new MainView();
            new MainPresenter(view);

            Application.Run((Form)view);
        }
    }
}