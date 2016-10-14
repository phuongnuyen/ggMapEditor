using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ggMapEditor.Models
{
    public class Tile
    {
        public int id { get; set; }
        public Point position { get; set; }
        public Uri imageSource { get; set; }
        public double size { get; set; }

        public Tile(){ }

        public Tile(Uri imageSource, double size)
        {
            this.imageSource = imageSource;
            this.size = size;
        }
    }
}