using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ggMapEditor.Models
{
    public class Tile
    {
        public static int id { get; set; }
        public ulong position { get; set; }
        public ImageSource image { get; set; }
        public int size { get; set; }

        public Tile()
        {
            id++;
            size = 32;
        }

        public Tile(ImageSource image, int size)
        {
            this.image = image;
            this.size = size;
        }
    }
}