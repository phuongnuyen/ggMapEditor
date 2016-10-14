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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ggMapEditor.Views.Controls
{
    /// <summary>
    /// Interaction logic for DragableLayout.xaml
    /// </summary>
    [ContentProperty(nameof(Children))]
    public partial class DragableLayout : UserControl
    {
        UIElement source = null;
        Point objectPos;
        Point canvasPos;

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
        private void Object_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            source = sender as UIElement;
            Mouse.Capture(source);
            objectPos.X = Canvas.GetLeft(source);
            objectPos.Y = Canvas.GetTop(source);
            canvasPos = e.GetPosition(container);
        }

        private void Object_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var curPos = e.GetPosition(container);
                objectPos += curPos - canvasPos;
                Canvas.SetLeft(source, objectPos.X);
                Canvas.SetTop(source, objectPos.Y);
                canvasPos = curPos;
            }
        }

        private void Object_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            container.ReleaseMouseCapture();
            Mouse.Capture(null);
        }

        private void Layout_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.Move;
            }
        }

        private void Layout_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel panel = (Panel)sender;
                UIElement element = (UIElement)e.Data.GetData("Object");

                if (panel != null && element != null)
                {
                    Panel parent = (Panel)VisualTreeHelper.GetParent(element);
                    if (parent != null)
                    {
                        if (e.KeyStates == DragDropKeyStates.ControlKey && e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            Views.Controls.Tile tile = new Views.Controls.Tile(element as Controls.Tile);
                            panel.Children.Add(tile);
                            e.Effects = DragDropEffects.Copy;

                        }
                        else
                            if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            parent.Children.Remove(element);
                            panel.Children.Add(element);
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }
    }
}
