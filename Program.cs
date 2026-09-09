using LubricentroVelezV2.Repositories.Implementation;
using LubricentroVelezV2.Services.Implementation;
using System;
using System.IO;
using System.Windows.Forms;

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

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => HandleGlobal(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => HandleGlobal(e.ExceptionObject as Exception);

            var factory = new OrdenesContextFactory();
            var repo = new OrdenesRepository(factory);
            var service = new OrdenesService(repo);

            Application.Run(new frmPrincipal(service));
        }

        private static void HandleGlobal(Exception? ex)
        {
            try
            {
                var logPath = Path.Combine(AppContext.BaseDirectory, "errores.log");
                File.AppendAllText(logPath, $"{DateTime.Now}: {ex}{Environment.NewLine}");
            }
            catch
            {
                // Swallow logging failures — never let logging itself crash the app.
            }

            MessageBox.Show(
                "Ocurrió un error inesperado. La información fue registrada. Si el problema persiste, contacte al soporte.",
                "Error inesperado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}