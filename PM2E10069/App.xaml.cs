
using PM2E10069.vista;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10069
{
    public partial class App : Application
    {
        static controladores.DBProc dBProc;
        PageListSitio listSitio;
        public static controladores.DBProc Instancia
        {
            get
            {
                if (dBProc == null)
                {
                    String dbname = "Proc.db3";
                    String dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    String dbfulp = Path.Combine(dbpath, dbname);
                    dBProc = new controladores.DBProc(dbfulp);
                }

                return dBProc;
            }
        }
        public App()
        {
            InitializeComponent();

            listSitio = new PageListSitio();

            MainPage = new NavigationPage(listSitio);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

}
