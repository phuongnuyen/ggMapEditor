using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ggMapEditor.Commands;
using ggMapEditor.Models;
using Microsoft.Win32;

namespace ggMapEditor.ViewModels
{
    class TilesetBoxViewModel : Base.BaseViewModel
    {
        private Models.Tileset tileset;
        //public ObservableCollection<ImageSource> tiles { get; private set; }
        public ObservableCollection<Views.Controls.Tile> Tiles
        {
            get { return tileset.tiles; }
            set
            {
                tileset.tiles = value;
                RaisePropertyChanged("Tiles");
            }
        }
        public TilesetBoxViewModel()
        {

            tileset = new Models.Tileset();

            OpenFileCommand = new RelayCommand(BrowsFile);
            OkCommand = new RelayCommand(ButtonOk);
            CancelCommand = new RelayCommand(ButtonCancel);
        }

        #region Properties
        public string Name
        {
            get { return tileset.name; }
            set
            {
                tileset.name = value;
                RaisePropertyChanged("Name");
            }

        }
        public TilesetType Type
        {
            get { return tileset.type; }
            set
            {
                tileset.type = value;
                RaisePropertyChanged("Type");
            }
        }
        public Uri ImageUri
        {
            get { return tileset.imageUri; }
            set
            {
                tileset.imageUri = value;
                RaisePropertyChanged("ImageUri");
            }
        }
        public int TileSize
        {
            get { return tileset.tileSize; }
            set
            {
                tileset.tileSize = value;
                if (value == 0)
                {
                    MessageBox.Show("Invalid value. Set default tile size is 32.");
                    tileset.tileSize = 32;
                    RaisePropertyChanged("TileSize");
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand OkCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        void ButtonOk(object parameter)
        {
            CropImage();
            CloseWindow();
        }
        void ButtonCancel(object parameter)
        {
            CloseWindow();
        }
        void BrowsFile(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Image files |*png;*jpge;jpg";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fileDialog.ShowDialog() == true)
                try
                {
                    ImageUri = new Uri(fileDialog.FileName, UriKind.RelativeOrAbsolute);
                    Name = fileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

        }
        #endregion

        #region Other functions
        private void CropImage()
        {
            Tiles = new ObservableCollection<Views.Controls.Tile>();
            if (Tiles != null)
            {
                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.UriSource = ImageUri;
                source.CacheOption = BitmapCacheOption.OnLoad;
                source.EndInit();

                for (int i = 0; i < 200; i += tileset.tileSize + 1)
                    for (int k = 0; k < 200; k += tileset.tileSize + 1)
                    {
                        Int32Rect rect = new Int32Rect(k, i, tileset.tileSize, tileset.tileSize);
                        CroppedBitmap imageSource = new CroppedBitmap(source, rect);

                        Views.Controls.Tile tile = new Views.Controls.Tile();
                        tile.TileSource = imageSource;
                        tile.TileSize = tileset.tileSize;
                        Tiles.Add(tile);
                    }
            }
        }
        #endregion
    }
}
