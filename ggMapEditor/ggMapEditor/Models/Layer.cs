using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggMapEditor.Models
{
    class Layer
    {
        public string name { get; set; }
        public int id { get; private set; }
        public bool isVisible { get; set; }
    }
}
