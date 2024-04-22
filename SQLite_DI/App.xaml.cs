using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using SQLite_DI.Db;
using SQLite_DI.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;


namespace SQLite_DI
{

    public partial class App : Application
    {
        private Window m_window;

        public static IServiceProvider ServiceProvider { get; private set; }
        public static string DbConString { get; set; }


        public App()
        {
            this.InitializeComponent();

            try
            {

                var DbPath = ApplicationData.Current.LocalFolder.Path;

                Debug.WriteLine($"Db path\n{DbPath}");

                DbConString = $"DataSource={DbPath}\\sqlite_di.db";

                ServiceProvider = ConfigureServices();
                Ioc.Default.ConfigureServices(ServiceProvider);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            try
            {
                m_window = new MainWindow();
                m_window.Activate();

                Debug.WriteLine($"EnsureCreated...");
                SQLite_DbContext db = ServiceProvider.GetRequiredService<SQLite_DbContext>();
                db.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private IServiceProvider ConfigureServices()
        {
            try
            {
                var services = new ServiceCollection();

                services.AddSingleton<Main_VM>();

                services.AddDbContext<SQLite_DbContext>();

               // services.AddSingleton<SQLite_DbContext>();


                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                var svcs = services.BuildServiceProvider();

                return svcs;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
