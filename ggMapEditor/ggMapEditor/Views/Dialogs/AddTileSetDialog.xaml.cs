using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ggMapEditor.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for InsertTileSet.xaml
    /// </summary>
    public partial class AddTilesetDialog : Window
    {
        ViewModels.AddTilesetViewModel vm;

        //public event PropertyChangedEventHandler PropertyChanged;
        //internal void RaisePropertyChanged(string property)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}

        public AddTilesetDialog()
        {
            InitializeComponent();
            vm = new ViewModels.AddTilesetViewModel();
            this.DataContext = vm;
        }

        public ObservableCollection<Views.Controls.Tile> GetTiles()
        {
            return vm.Tiles;
        }
    }
}
