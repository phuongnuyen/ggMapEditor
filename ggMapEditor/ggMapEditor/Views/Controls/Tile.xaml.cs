using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ggMapEditor.Views.Controls
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        #region private members
        //private Brush preBrush = null;
        #endregion

        public static readonly DependencyProperty TileSourceProperty
            = DependencyProperty.Register("TileSource", typeof(Uri), typeof(Tile),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTileSourceChanged)));
        public static readonly DependencyProperty TileSizeProperty
            = DependencyProperty.Register("TileSize", typeof(double), typeof(Tile),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTileSizeChanged)));
        #region constructors
        public Tile()
        {
            InitializeComponent();
        }

        public Tile(Tile tile)
        {
            InitializeComponent();
            this.TileSize = tile.TileSize;
            this.TileSource = tile.TileSource;
        }
        #endregion

        #region properties
        public Uri TileSource
        {
            get { return (Uri)GetValue(TileSourceProperty); }
            set { SetValue(TileSourceProperty, value);
            }
        }
        public double TileSize
        {
            get { return (double)GetValue(TileSizeProperty); }
            set { SetValue(TileSizeProperty, value);
            }
        }
        #endregion

        #region CallBacks
        private static void OnTileSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.Source = new BitmapImage((Uri)e.NewValue);
        }
        private static void OnTileSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.Height = tile.tileImg.Width = (double)e.NewValue;
        }
        #endregion

        #region functions
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData(DataFormats.Bitmap, new BitmapImage(this.TileSource));
                data.SetData("Double", this.TileSize);
                data.SetData("Object", this);

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);

            if (e.Effects.HasFlag(DragDropEffects.Copy))
                Mouse.SetCursor(Cursors.Cross);
            else
                if (e.Effects.HasFlag(DragDropEffects.Move))
                Mouse.SetCursor(Cursors.Hand);
            else
                Mouse.SetCursor(Cursors.No);

            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            //if (e.Data.GetDataPresent(typeof(Models.Tile)))
            //{
            //    ImageSource imgSource = 
            //    ImageSourceConverter converter = new ImageSourceConverter();
            //    if (converter.IsValid(dataStr.imageSource))
            //    {

                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.Move;
                //}
            //}
            e.Handled = true;
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.None;

            //if (e.Data.GetDataPresent(DataFormats.Bitmap))
            //{
            //    Bitmap dataBitmap = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            //    ImageSourceConverter converter = new ImageSourceConverter();
            //    if (converter.IsValid(dataBitmap))
            //    {
                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }
            //    }
            //}
            e.Handled = true;
        }
        //protected override void OnDragEnter(DragEventArgs e)
        //{
        //    base.OnDragEnter(e);

        //    //preBrush = tileImg.Fill;
        //    if (e.Data.GetDataPresent(DataFormats.StringFormat))
        //    {
        //        string dataStr = (string)e.Data.GetData(DataFormats.StringFormat);
        //        BrushConverter converter = new BrushConverter();
        //        if (converter.IsValid(dataStr))
        //        {
        //            //tileImg.Fill = (Brush)converter.ConvertFromString(dataStr);
        //        }
        //    }
        //}

        //protected override void OnDragLeave(DragEventArgs e)
        //{
        //    base.OnDragLeave(e);
        //    tileImg.Fill = preBrush;
        //}
        #endregion
    }
}
