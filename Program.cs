using LubricentroVelezV2.Models;
using LubricentroVelezV2.Repositories.Implementation;
using LubricentroVelezV2.Services.Implementation;

namespace LubricentroVelezV2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var context = new OrdenesContext();
            var repo = new OrdenesRepository(context);
            var service = new OrdenesService(repo);

            Application.Run(new frmPrincipal(service));
        }
    }
}