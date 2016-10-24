using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ggMapEditor.QuadTree;

namespace ggMapEditor.QuadTree
{
    

    class QuadTree<T>
    {
        private int cellSize; //
        private ObservableCollection<QTObject<T>> listObj;
        private QuadNode<T> root;

        public QuadTree(Int32Rect rect, ObservableCollection<QTObject<T>> objs)
        {
            listObj = objs;
            root = new QuadNode<T>(rect, listObj);
            cellSize = 32;
        }

        public void SetCellSize(int cellSize)
        {
            this.cellSize = cellSize;
        }

        public void Clear()
        {
            root.Clear();
        }

        public void CreateQuadTree()
        {
            foreach (var o in listObj)
                root.Insert(o, cellSize);
        }

        public ObservableCollection<QTObject<T>> RetrieveObjects()
        {
            return root.RetrieveObject();
        }
    }
}
