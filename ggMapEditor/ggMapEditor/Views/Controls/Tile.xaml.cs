﻿using System;
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
        public static readonly DependencyProperty TileWidthProperty
            = DependencyProperty.Register("TileHeight", typeof(double), typeof(Controls.Tile),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTileWidthChanged)));
        public static readonly DependencyProperty TileHeightProperty
           = DependencyProperty.Register("TileWidth", typeof(double), typeof(Controls.Tile),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTileHeightChanged)));
        public static readonly DependencyProperty ImgIdProperty
           = DependencyProperty.Register("ImgId", typeof(long), typeof(Controls.Tile),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnImgIdChanged)));
        #region constructors
        public Tile()
        {
            InitializeComponent();
        }

        public Tile(Tile tile)
        {
            InitializeComponent();
            this.TileHeight = tile.TileHeight;
            this.TileWidth = tile.TileWidth;
            this.TileSource = tile.TileSource;
            //this.RectImage = tile.RectImage;
        }
        #endregion

        #region properties
        public long ImgId
        {
            get { return (long)GetValue(ImgIdProperty); }
            set { SetValue(ImgIdProperty, value); }
        }
        public ImageSource TileSource
        {
            get { return (ImageSource)GetValue(TileSourceProperty); }
            set { SetValue(TileSourceProperty, value);
            }
        }
        public double TileWidth
        {
            get { return (double)GetValue(TileWidthProperty); }
            set
            {
                SetValue(TileWidthProperty, value);
            }
        }
        public double TileHeight
        {
            get { return (double)GetValue(TileHeightProperty); }
            set
            {
                SetValue(TileHeightProperty, value);
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
        private static void OnTileWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.Width = (double)e.NewValue;
        }
        private static void OnTileHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.Height = (double)e.NewValue;
        }
        private static void OnImgIdChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Tile tile = (Tile)sender;
            tile.tileImg.ToolTip = (long)e.NewValue;
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
                data.SetData("ImgId", this.ImgId);
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
