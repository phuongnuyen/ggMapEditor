using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ggMapEditor.Views.Main
{
    /// <summary>
    /// Interaction logic for DragWindow.xaml
    /// </summary>
    public partial class DragWindow : Window
    {
        UIElement source = null;
        Point objectPos;
        Point canvasPos;

        public DragWindow()
        {
            InitializeComponent();
        }

        private void Object_MouseLeftButtonDown(object sender, MouseButtonEventArgs e )
        {
            source = sender as UIElement;
            Mouse.Capture(source);
            objectPos.X = Canvas.GetLeft(source);
            objectPos.Y = Canvas.GetTop(source);
            canvasPos = e.GetPosition(zoomBox);
        }

        private void Object_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var curPos = e.GetPosition(zoomBox);
                objectPos += curPos - canvasPos;
                Canvas.SetLeft(source, objectPos.X);
                Canvas.SetTop(source, objectPos.Y);
                canvasPos = curPos;
            }
        }

        private void Object_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            zoomBox.ReleaseMouseCapture();
            Mouse.Capture(null);
        }

        private void ZoomBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.Move;
            }
        }

        private void ZoomBox_Drop(object sender, DragEventArgs e)
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
