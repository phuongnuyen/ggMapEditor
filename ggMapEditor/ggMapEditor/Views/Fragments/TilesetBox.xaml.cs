using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ggMapEditor.Views.Fragments
{
    /// <summary>
    /// Interaction logic for TilesetBox.xaml
    /// </summary>
    public partial class TilesetBox : UserControl, INotifyPropertyChanged
    {
        public Models.Tileset tileset;

        //private ObservableCollection<ImageSource> tiles;
        public ObservableCollection<Views.Controls.Tile> Tiles
        {
            get { return tileset.tiles; }
            set
            {
                tileset.tiles = value;
                listBox.ItemsSource = Tiles;
                RaisePropertyChanged("Tiles");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public TilesetBox()
        {
            InitializeComponent();
            tileset = new Models.Tileset();
            Tiles = new ObservableCollection<Views.Controls.Tile>();
            BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/rampo.jpg", UriKind.Absolute));


            Int32Rect rect = new Int32Rect(0, 0, 320, 320);
            CroppedBitmap image = new CroppedBitmap(bitmapImage, rect);

            Image img = new Image();
            img.Source = image;
            Views.Controls.Tile tile = new Controls.Tile();
            tile.TileSource = image;
            tile.TileSize = tileset.tileSize;
            Tiles.Add(tile);
            //listBox.ItemsSource = Tiles;
        }
    }
}
