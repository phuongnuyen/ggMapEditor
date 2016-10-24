using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models
{
    public class TileMap
    {
        public int width { get; set; }
        public int height { get; set; }
        public int row { get; set; }
        public int column { get; set; }
        public int tileSize { get; set; }
        public ObservableCollection<Models.Tile> listTile { get; set; }
        //public Models.Tileset tileset { get; set; }
        //public ObservableCollection<Models.Layer> layers { get; set; }  

        public TileMap()
        {
            //tileset = new Tileset();
            listTile = new ObservableCollection<Tile>();
            tileSize = 32;
            //tilesets = new ObservableCollection<Tileset>();
            //layers = new ObservableCollection<Layer>();
        }
    }
}
