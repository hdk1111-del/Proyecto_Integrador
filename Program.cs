using System;
using System.Windows.Forms;
using Proyecto_Integrador.View;

namespace Proyecto_Integrador
{
    internal static class Program
    {
     
        [STAThread]
        static void Main()
        {
          
            ApplicationConfiguration.Initialize();

        
            Application.Run(new Login());
        }
    }
}
