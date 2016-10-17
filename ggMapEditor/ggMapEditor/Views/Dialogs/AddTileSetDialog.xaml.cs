using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ggMapEditor.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for InsertTileSet.xaml
    /// </summary>
    public partial class AddTilesetDialog : Window
    {
        private Models.Tileset tileset;
        ViewModels.TilesetBoxViewModel vm;

        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public AddTilesetDialog()
        {
            InitializeComponent();
            vm = new ViewModels.TilesetBoxViewModel();
            this.DataContext = vm;
        }

        public Models.Tileset Tileset
        {
            get { return tileset; }
            private set { }
        }

        public ObservableCollection<Views.Controls.Tile> GetTiles()
        {
            return vm.Tiles;
        }


        //private void BrowswImage_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    fileDialog.Multiselect = true;
        //    fileDialog.Filter = "Image files |*png;*jpge;jpg";
        //    fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //    if (fileDialog.ShowDialog() == true)
        //        tbImagePath.Text = fileDialog.FileName;

        //}

        //private void ButtonOK_Click(object sender, RoutedEventArgs e)
        //{
        //    tileset = new Models.Tileset();
        //    tileset.name = tbName.Text;
        //    tileset.imageUri = new Uri(tbImagePath.Text);

        //    int size;
        //    if (int.TryParse(tbTileSize.Text, out size))
        //        tileset.tileSize = size;
        //    else
        //        tileset.tileSize = 0;
        //    tileset.type = (string)comboType.Tag == "Base" ? Models.TilesetType.Base : Models.TilesetType.Collections;

        //    this.Close();
        //}

        //private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
