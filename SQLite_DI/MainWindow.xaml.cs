using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SQLite_DI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIEx;


namespace SQLite_DI
{
    public sealed partial class MainWindow : WindowEx
    {
        public Main_VM ViewModel { get; }

        public MainWindow()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<Main_VM>();
            ViewModel.TheDispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

            // Set the PersistenceId to save the window size & position in LocalSettings
            PersistenceId = "MainWin";
        }

        private void Grid_Main_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadDb();
        }


        // Seed
        private void SeedDb_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SeedDb(10);
        }

        private void Seed100_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SeedDb(100);
        }

        private void Seed1000_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SeedDb(1000);
        }


        // Update
        private void Btn_update_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Update();
        }


        // Delete
        private void Btn_delete_all_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteAll();
        }

        private void Btn_delete_10_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete10();
        }

    }
}
