using System;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ggMapEditor.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveTileMap_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Tile map files|*json";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fileDialog.FileName = "untitled.json";
            if (fileDialog.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(layerBox.GetListLayer());
                System.IO.File.WriteAllText(fileDialog.FileName, json);
            }

                
        }

        private void OpenTileMap_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Tile map files (*tmx)|*.tmx";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fileDialog.ShowDialog() == true)
                fileName = fileDialog.FileName + ".tmx";
        }

        private void NewTileMap_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.NewTileMapDialog fileDialog = new Dialogs.NewTileMapDialog();
            fileDialog.ShowDialog();
        }

        private void NewTileSet_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.AddTilesetDialog fileDialog = new Dialogs.AddTilesetDialog();
            fileDialog.ShowDialog();
            tilesetBox.Tiles = fileDialog.GetTiles();
        }


        ////////////////////////////////////////////////
    }
}
