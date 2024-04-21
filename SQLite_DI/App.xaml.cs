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

            var DbPath = ApplicationData.Current.LocalFolder.Path;

            Debug.WriteLine($"Db path\n{DbPath}");

            DbConString = $"DataSource={DbPath}\\sqlite_di.db";

            ServiceProvider = ConfigureServices();
            Ioc.Default.ConfigureServices(ServiceProvider);

        }


        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();


            using (var db = new SQLite_DbContext())
            {
                db.Database.EnsureCreated();
            }

        }


        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();


            //services.AddDbContext<SQLite_DbContext>(options => options
            //    .UseSqlite($"{DbConString}")
            //    .EnableSensitiveDataLogging(true)
            //    .EnableThreadSafetyChecks(true)
            //    .EnableDetailedErrors()
            //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
            //    ServiceLifetime.Transient
            //); 


            services.AddTransient<IPersonDb, PersonSQLiteDb>();

            services.AddSingleton<Main_VM>();

            var svcs = services.BuildServiceProvider();


            return svcs;
        }

    }
}
