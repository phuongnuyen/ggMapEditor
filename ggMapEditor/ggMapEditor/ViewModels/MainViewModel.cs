using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.ViewModels
{
    class MainViewModel : Base.BaseViewModel
    {
        Models.TileMap tileMap;
        public int Width
        {
            get { return tileMap.width; }
            set
            {
                tileMap.width = value;
                RaisePropertyChanged("Width");
            }
        }
        public int Height
        {
            get { return tileMap.height; }
            set
            {
                tileMap.height = value;
                RaisePropertyChanged("Heigth");
            }
        }
        public int TileSize
        {
            get { return tileMap.tileSize; }
            set
            {
                tileMap.tileSize = value;
                RaisePropertyChanged("TileSize");
            }
        }
        public ObservableCollection<Models.Tileset> Tileset
        {
            get { return tileMap.tilesets; }
            set
            {
                tileMap.tilesets = value;
                RaisePropertyChanged("Tilesets");
            }
        }
        public ObservableCollection<Models.Layer> layers
        {
            get { return tileMap.layers; }
            set
            {
                tileMap.layers = value;
                RaisePropertyChanged("Layers");
            }
        }
    }
}
