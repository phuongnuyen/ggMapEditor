using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models
{
    class TileMap
    {
        public int width { get; set; }
        public int height { get; set; }
        public int tileSize { get; set; }
        public ObservableCollection<Models.Tileset> tilesets { get; set; }
        public ObservableCollection<Models.Layer> layers { get; set; }  

        public TileMap()
        {
            tilesets = new ObservableCollection<Tileset>();
            layers = new ObservableCollection<Layer>();
        }
    }
}
