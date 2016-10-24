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
        //private ViewModels.MainViewModel vm;
        private Models.Combine combine;
        public int ColumCount
        {
            get { return TileMap.width/TileMap.tileSize; }
        }
        public int RowCount
        {
            get { return TileMap.height / TileMap.tileSize; }
        }

        public Models.TileMap TileMap
        {
            get { return combine.tileMap; }
            set
            {
                combine.tileMap = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //vm = new ViewModels.MainViewModel();
            //this.DataContext = vm;
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

            //if (fileDialog.ShowDialog() == true)
            //    fileName = fileDialog.FileName + ".tmx";
        }

        private void NewTileMap_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.NewTileMapDialog fileDialog = new Dialogs.NewTileMapDialog();
            fileDialog.ShowDialog();
            if (fileDialog.GetCombine() != null)
            {
                combine = fileDialog.GetCombine();
            }

        }

        private void NewTileSet_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.AddTilesetDialog fileDialog = new Dialogs.AddTilesetDialog(combine.folderPath);
            fileDialog.ShowDialog();
            combine.tileset = fileDialog.GetTileset();

            tilesetBox.SetTileset(combine.tileset);
        }


        ////////////////////////////////////////////////
    }
}
