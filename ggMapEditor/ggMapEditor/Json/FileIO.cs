using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ggMapEditor.QuadTree;
using Newtonsoft.Json;

namespace ggMapEditor.Json
{
    public static class ConvertJson
    {
        public static string SaveFile(Models.Combine combine)
        {
            Models.TileMap tileMap = combine.tileMap;
            ObservableCollection<QTObject<Models.Tile>> listQTObj = new ObservableCollection<QTObject<Models.Tile>>();
            foreach (var tile in tileMap.listTile)
            {
                QTObject<Models.Tile> obj = new QTObject<Models.Tile>(tile.Bound, tile);
                listQTObj.Add(obj);
            }
            Int32Rect rect = new Int32Rect(0, 0, tileMap.width, tileMap.height);
            QuadTree<Models.Tile> quadTree = new QuadTree.QuadTree<Models.Tile>(rect, listQTObj);
            quadTree.CreateQuadTree();

            listQTObj.Clear();
            listQTObj = quadTree.RetrieveObjects();

            string json = JsonConvert.SerializeObject(listQTObj);
            string filePath = combine.folderPath + "\\" + combine.folderName + ".json";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(combine.folderPath);

            System.IO.File.WriteAllText(filePath, json);
            return json;
        }
    }
}
