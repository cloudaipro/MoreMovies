using MoreMovies.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoreMovies
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {

            DBBO.DBConnect("hibernate.cfg.xml");

            App app = new App();

            // Code to register events and set properties that were
            // defined in XAML in the application definition
            app.InitializeComponent();

            // Start running the application
            app.Run();
            DBBO.DBDispose();
        }
    }
}
