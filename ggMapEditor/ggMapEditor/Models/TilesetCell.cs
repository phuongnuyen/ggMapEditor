using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models
{
    public class TilesetCell
    {
        private static long countId = 0;
        public long id { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public TilesetCell()
        {
            id = TilesetCell.countId++;
        }

    }
}
