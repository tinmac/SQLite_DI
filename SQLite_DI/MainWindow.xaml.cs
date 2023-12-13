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


namespace SQLite_DI
{
    public sealed partial class MainWindow : Window
    {
        public Main_VM ViewModel { get; }

        public MainWindow()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<Main_VM>();
        }

        private async void SeedDb_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SeedDb();

            await ViewModel.LoadDb();
        }

        private async void Grid_Main_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.SeedDb();

            await ViewModel.LoadDb();
        }
    }
}
