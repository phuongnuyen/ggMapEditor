using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ggMapEditor.Models
{
    public class Tile
    {
        public string Id { get; private set; }
        public int imgId { get; set; }

        private Int32Rect bound;
        public Int32Rect Bound
        {
            get { return bound; }
            set
            {
                bound = value;
                //Id = "{" + bound.X.ToString() + " ," + bound.Y.ToString() + "}";
            }
        }
        //public Int32Rect RectImage { get; set; }

        public Tile()
        {

        }
    }
}