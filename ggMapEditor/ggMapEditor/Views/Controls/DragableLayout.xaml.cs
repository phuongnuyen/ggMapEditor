using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace ggMapEditor.Views.Controls
{
    [ContentProperty(nameof(Children))]
    public partial class DragableLayout : UserControl
    {
        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly
        (
            nameof(Children),
            typeof(UIElementCollection),
            typeof(DragableLayout),
            new PropertyMetadata()
        );

        public UIElementCollection Children
        {
            get { return (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty); }
            private set { SetValue(ChildrenProperty, value); }
        }

        public DragableLayout()
        {
            InitializeComponent();
        }


        private void Layout_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;

        }

        private void Layout_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Canvas panel = (Canvas)sender;
                UIElement element = (UIElement)e.Data.GetData("Object");

                if (panel != null && element != null)
                {
                    DependencyObject parent = VisualTreeHelper.GetParent(element);
                    if (parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            Views.Controls.Tile tile = new Views.Controls.Tile(element as Controls.Tile);
                            panel.Children.Add(tile);

                            //var mousePos = e.GetPosition(panel);
                            //Canvas.SetTop(tile, mousePos.Y - tile.TileSize / 2);
                            //Canvas.SetLeft(tile, mousePos.X - tile.TileSize / 2);
                            e.Effects = DragDropEffects.Copy;

                        }
                    }
                }
                container.ReleaseMouseCapture();
                Mouse.Capture(null);
            }
        }
    }
}
