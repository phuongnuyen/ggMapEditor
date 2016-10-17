using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models
{
    public enum TilesetType { Base, Collections };
    public class Tileset
    {
        public int id { get; private set; }
        public string name { get; set; }
        public TilesetType type { get; set; }
        public Uri imageUri { get; set; }
        public int tileSize = 32;


        public ObservableCollection<Views.Controls.Tile> tiles { get; set; }
        public Tileset()
        {
            tiles = new ObservableCollection<Views.Controls.Tile>();
        }
    }
}
