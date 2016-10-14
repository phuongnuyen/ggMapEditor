using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models.Base
{
    class Object
    {
        public int tileSize { get; set; }
        public Point position { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public Color background { get; set; }
        public bool isVisible { get; set; }
    }
}
