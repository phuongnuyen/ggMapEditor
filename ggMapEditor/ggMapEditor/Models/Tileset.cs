using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ggMapEditor.Models
{
    public class Tileset
    {
        private static int countId = 0;
        public int id { get; private set; }
        public string name { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int tileHeight { get; set; }
        public int tileWidth { get; set; }
        public int numberOfCell { get; set; }
        public int numberOfCellPerRow { get; set; }
        public int numberOfCellPerCol { get; set; }



        public Uri imageUri { get; set; }

        //public int tileSize { get; set; }

        //public Brush colorTransparent { get; set; }
        public TilesetType type { get; set; }
        public ObservableCollection<Models.TilesetCell> tileList { get; set; }
        public Tileset()
        {
            id = Tileset.countId++;
            name = "Tileset " + id.ToString();
            //tileSize = 32;
            tileHeight = tileWidth = 32;
            //colorTransparent = Brushes.Pink;
            //tiles = new ObservableCollection<Models.Tile>();
            tileList = new ObservableCollection<TilesetCell>();
        }
    }
}
