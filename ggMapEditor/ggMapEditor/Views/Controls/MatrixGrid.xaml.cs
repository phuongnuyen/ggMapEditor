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

namespace ggMapEditor.Views.Controls
{
    /// <summary>
    /// Interaction logic for MatrixGrid.xaml
    /// </summary>
    public partial class MatrixGrid : UserControl
    {
        public static readonly DependencyProperty RownCountProperty
            = DependencyProperty.Register("RowCount", typeof(int), typeof(MatrixGrid),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnRowCountChanged)));
        public static readonly DependencyProperty ColumnCountProperty
            = DependencyProperty.Register("ColumnCount", typeof(int), typeof(MatrixGrid),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColumnCountChanged)));
        public static readonly DependencyProperty TileSizeProperty
            = DependencyProperty.Register("TileSize", typeof(double), typeof(MatrixGrid),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTileSizeChanged)));

        #region Constructors
        public MatrixGrid()
        {
            InitializeComponent();
            InitGrid(); //To Test
        }
        #endregion

        #region Properties
        public int RowCount
        {
            get { return (int)GetValue(RownCountProperty); }
            set { SetValue(RownCountProperty, value); }
        }
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }
        public double TileSize
        {
            get { return (double)GetValue(TileSizeProperty); }
            set { SetValue(TileSizeProperty, value); }
        }
        #endregion

        #region Callbacks
        private static void OnRowCountChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MatrixGrid matrixGrid = (MatrixGrid)sender;
            matrixGrid.RowCount = (int)e.NewValue;
        }
        private static void OnColumnCountChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MatrixGrid matrixGrid = (MatrixGrid)sender;
            matrixGrid.ColumnCount = (int)e.NewValue;
        }
        private static void OnTileSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MatrixGrid matrixGrid = (MatrixGrid)sender;
            matrixGrid.TileSize = (double)e.NewValue;
        }
        #endregion

        private void InitGrid()
        {
            TileSize = 30;
            RowCount = 30;
            ColumnCount = 10;
            ///====================

            if (TileSize == 0 || RowCount == 0 | ColumnCount == 0)
                return;

            // Clean grid
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            // Init grid
            grid.Width = TileSize * ColumnCount;
            grid.Height = TileSize * RowCount;

            // Create collumns, rows
            for (int i = 0; i < RowCount - 1; i++)
                grid.RowDefinitions.
                    Add(new RowDefinition(){ Height = new GridLength(32)});
            for (int i = 0; i < ColumnCount - 1; i++)
                grid.ColumnDefinitions.
                    Add(new ColumnDefinition() { Width = new GridLength(32) });

            // Add Cells
            for (int i = 0; i < RowCount; i++)
                for (int k = 0; k < ColumnCount; k++)
                {
                    DragableLayout layout = new DragableLayout();
                    grid.Children.Add(layout);
                    Grid.SetRow(layout, i);
                    Grid.SetColumn(layout, k);
                }
        }

        
    }
}
