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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SQLite_DI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
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


        protected async override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();


            using (var db = new SQLite_DbContext())
            {
                // Note: EnsureCreated calls -> OnConfiguring

                //db.Database.EnsureCreated();
                await db.Database.EnsureCreatedAsync();
            }

        }


        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();


            services.AddDbContext<SQLite_DbContext>(options => options
                .UseSqlite($"{DbConString}")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableDetailedErrors()
            ); // options.UseSqlite()); //options.UseSqlite($"Data Source = { dbFile}"));


            services.AddTransient<IPersonDb, PersonSQLiteDb>();

            services.AddSingleton<Main_VM>();

            var svcs = services.BuildServiceProvider();


            return svcs;
        }

    }
}
