using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.AvalonDock;
using Microsoft.Win32;

namespace ggMapEditor.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveTileMap_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Tile map files (*tmx)|*.tmx";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string fileName;
            if (fileDialog.ShowDialog() == true)
                fileName = fileDialog.FileName + ".tmx";
        }

        private void OpenTileMap_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Tile map files (*tmx)|*.tmx";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string fileName;
            if (fileDialog.ShowDialog() == true)
                fileName = fileDialog.FileName + ".tmx";
        }

        private void NewTileMap_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.NewTileMapDialog fileDialog = new Dialogs.NewTileMapDialog();
            fileDialog.ShowDialog();
        }


        ////////////////////////////////////////////////
    }
}
