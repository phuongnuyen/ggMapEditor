using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ggMapEditor.QuadTree
{
    public class QTObject<T>
    {
        public string id;
        public T obj;
        public Int32Rect rect;  // Rect tra ve cua doi tuong

        public QTObject(Int32Rect rect, T obj)
        {
            this.id = "";
            this.obj = obj;
            this.rect = rect;
        }
    }

    class QuadNode<T>
    {
        private ObservableCollection<QTObject<T>> listObj;
        private Int32Rect rect;

        private QuadNode<T> topLeft;
        private QuadNode<T> topRight;
        private QuadNode<T> bottomLeft;
        private QuadNode<T> bottomRight;
        public bool hasChild;  //Node la
        public int level;

        public QuadNode(Int32Rect rect, ObservableCollection<QTObject<T>> objs = null)
        {
            if (listObj == null)
                listObj = new ObservableCollection<QTObject<T>>();
            else listObj = objs;
            this.rect = rect;

            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;

            hasChild = false;
        }

        public void Clear()
        {
            if (this.hasChild == false)
                return;
            topLeft.Clear();
            topRight.Clear();
            bottomLeft.Clear();
            bottomRight.Clear();
            listObj.Clear();
        }

        private bool IsContain(QTObject<T> obj)
        {
            return !(obj.rect.X + obj.rect.Width < rect.X
                    || obj.rect.Y + obj.rect.Height < rect.Y
                    || obj.rect.X > obj.rect.Width + rect.X
                    || obj.rect.Y > obj.rect.Height + rect.Y);
        }

        public void Insert(QTObject<T> obj, int cellSize)
        {
            if (hasChild)
            {
                if (topLeft.IsContain(obj))
                    topLeft.Insert(obj, cellSize);
                if (topRight.IsContain(obj))
                    topRight.Insert(obj, cellSize);
                if (bottomLeft.IsContain(obj))
                    bottomLeft.Insert(obj, cellSize);
                if (bottomRight.IsContain(obj))
                    bottomRight.Insert(obj, cellSize);
            }
            else
            {
                if (this.IsContain(obj))
                listObj.Add(obj);
                if (rect.Width > cellSize || rect.Height > cellSize)
                {
                    Split();

                    while (listObj.Count != 0)
                    {
                        var lastObj = this.listObj.Last();
                        this.listObj.Remove(listObj.Last());

                        if (topLeft.IsContain(lastObj))
                        {
                            lastObj.id += "1";
                            topLeft.listObj.Add(lastObj);
                        }
                        if (topRight.IsContain(lastObj))
                        {
                            lastObj.id += "2";
                            topRight.listObj.Add(lastObj);
                        }
                        if (bottomLeft.IsContain(lastObj))
                        {
                            lastObj.id += "3";
                            bottomLeft.listObj.Add(lastObj);
                        }
                        if (bottomRight.IsContain(lastObj))
                        {
                            lastObj.id += "4";
                            bottomRight.listObj.Add(lastObj);
                        }
                    }
                }
            }
        }

        private void Split()
        {
            topLeft = new QuadNode<T>
                (new Int32Rect(rect.X, rect.Y, rect.Width / 2, rect.Height / 2));
            topRight = new QuadNode<T>
                (new Int32Rect(rect.X, rect.Height / 2, rect.Width / 2, rect.Height / 2));
            bottomLeft = new QuadNode<T>
                (new Int32Rect(rect.Width / 2, rect.Y / 2, rect.Width / 2, rect.Height / 2));
            bottomRight = new QuadNode<T>
                (new Int32Rect(rect.Width / 2, rect.Height / 2, rect.Width / 2, rect.Height / 2));
            hasChild = true;
        }

        public ObservableCollection<QTObject<T>> RetrieveObject()
        {
            ObservableCollection<QTObject<T>> listReturn = listObj;
            if (hasChild)
            {
                    listReturn = JoinList(listReturn, topLeft.RetrieveObject());
                    listReturn = JoinList(listReturn, topRight.RetrieveObject());
                    listReturn = JoinList(listReturn, bottomLeft.RetrieveObject());
                    listReturn = JoinList(listReturn, bottomRight.RetrieveObject());
            }
            return listReturn;
        }
        private ObservableCollection<QTObject<T>> JoinList(ObservableCollection<QTObject<T>> list1, ObservableCollection<QTObject<T>> list2)
        {
            ObservableCollection<QTObject<T>> result = list1;
            foreach (var item in list2)
                result.Add(item);

            return result;
        }
    }
}
