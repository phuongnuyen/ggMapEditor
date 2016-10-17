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

        //public void SplitImage()
        //{
        //    if (tileset != null)
        //    {
        //        BitmapImage bitmapImage;
        //        Bitmap image;
        //        try
        //        {
        //            bitmapImage = new BitmapImage(tileset.imageUri);
        //            using (MemoryStream outStream = new MemoryStream())
        //            {
        //                BitmapEncoder enc = new BmpBitmapEncoder();
        //                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
        //                enc.Save(outStream);
        //                image = new System.Drawing.Bitmap(outStream);
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Windows.MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
        //            return;
        //        }

        //        for (int i = 1; i < image.Height; i += tileset.tileSize)
        //            for (int k = 1; k < image.Width; k += tileset.tileSize)
        //            {
        //                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(i, k, i + tileset.tileSize, k + tileset.tileSize);
        //                //System.Drawing.Imaging.PixelFormat pixelFormat = image.PixelFormat;
        //                Bitmap bitmap = CropImage(image, rect);
        //                list.Add(bitmap);
        //            }
        //        listBox.ItemsSource = list;
        //    }
        //}

        //Bitmap CropImage(System.Drawing.Image imageSource, Rectangle rect)
        //{
        //    Bitmap image = new Bitmap(32, 32);
        //    using (Graphics gp = Graphics.FromImage(image))
        //    {
        //        gp.DrawImage(imageSource, new System.Drawing.Rectangle(0, 0, 32, 32), rect, GraphicsUnit.Pixel);
        //    }
        //    return image;
        //}
    }
}
