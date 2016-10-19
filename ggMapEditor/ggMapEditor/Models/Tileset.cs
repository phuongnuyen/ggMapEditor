using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models
{
    public class Tileset
    {
        public static int id { get; private set; }
        public string name { get; set; }
        public TilesetType type { get; set; }
        public Uri imageUri { get; set; }
        public int tileSize { get; set; }
        public Brush colorTransparent { get; set; }


        public ObservableCollection<Views.Controls.Tile> tiles { get; set; }
        public Tileset()
        {
            id++;
            tileSize = 32;
            colorTransparent = Brushes.Pink;
            tiles = new ObservableCollection<Views.Controls.Tile>();
        }
    }
}
