using System;
using System.ComponentModel;
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
        private Models.Tile tile;

        public static readonly DependencyProperty TileSourceProperty
            = DependencyProperty.Register("TileSource", typeof(ImageSource), typeof(Controls.Tile),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTileSourceChanged)));
        public static readonly DependencyProperty TileSizeProperty
            = DependencyProperty.Register("TileSize", typeof(double), typeof(Controls.Tile),
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
            //this.RectImage = tile.RectImage;
        }
        #endregion

        #region properties
        public long ImgId { get; set; }
        public ImageSource TileSource
        {
            get { return (ImageSource)GetValue(TileSourceProperty); }
            set { SetValue(TileSourceProperty, value);
            }
        }
        public double TileSize
        {
            get { return (double)GetValue(TileSizeProperty); }
            set
            {
                SetValue(TileSizeProperty, value);
            }
        }
        //public Int32Rect RectImage { get; set; }
        #endregion

        #region CallBacks
        private static void OnTileSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.Source = (ImageSource)e.NewValue;
        }
        private static void OnTileSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.Height = tile.tileImg.Width = (double)e.NewValue;
        }
        #endregion

        #region functions
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData(DataFormats.Bitmap, TileSource);
                data.SetData("Double", this.TileSize);
                data.SetData("Object", this);

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);

            if (e.Effects.HasFlag(DragDropEffects.Copy))
                Mouse.SetCursor(Cursors.Pen);
            else
                Mouse.SetCursor(Cursors.No);

            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
        #endregion
    }
}
