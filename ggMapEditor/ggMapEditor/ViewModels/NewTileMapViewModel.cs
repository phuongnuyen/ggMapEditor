using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ggMapEditor.Commands;

namespace ggMapEditor.ViewModels
{
    class NewTileMapViewModel : Base.BaseViewModel
    {
        private Models.Combine combine;
        private Models.TileMap tileMap;

        public RelayCommand OkCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand BrowseCommand { get; set; }

        public NewTileMapViewModel()
        {
            tileMap = new Models.TileMap();
            combine = new Models.Combine();
            combine.folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            OkCommand = new RelayCommand(OkButton_Click);
            CancelCommand = new RelayCommand(CancelButton_Click);
            BrowseCommand = new RelayCommand(BrowseButton_Click);
            RowCount = 30;
            ColumnCount = 30;
        }

        #region Properties
        public int RowCount
        {
            get { return tileMap.row; }
            set
            {
                tileMap.row = value;
                tileMap.height = tileMap.row * tileMap.tileSize;
                RaisePropertyChanged("RowCount");
                RaisePropertyChanged("TileMapPixels");
            }
        }
        public int ColumnCount
        {
            get { return tileMap.column; }
            set
            {
                tileMap.column = value;
                tileMap.width = tileMap.column * tileMap.tileSize;
                RaisePropertyChanged("ColumnCount");
                RaisePropertyChanged("TileMapPixels");
            }
        }
        public int TileSize
        {
            get { return tileMap.tileSize; }
            set
            {
                tileMap.tileSize = value;
                tileMap.height = tileMap.row * tileMap.tileSize;
                tileMap.width = tileMap.column * tileMap.tileSize;
                RaisePropertyChanged("TileSize");
                RaisePropertyChanged("TileMapPixels");
            }
        }

        public string TileMapPixels
        {
            get
            {
                return tileMap.width + " x " + tileMap.height + " pixels";
            }
            set
            {
                //tileMapPixels = tileMap.width + " x " + tileMap.height + " pixels";
                RaisePropertyChanged("TileMapPixels");
            }
        }

        public string FolderPath
        {
            get { return combine.folderPath; }
            set
            {
                combine.folderPath = value;
                RaisePropertyChanged("FolderPath");
            }
        }

        public string FolderName
        {
            get { return combine.folderName; }
            set
            {
                combine.folderName = value;
                RaisePropertyChanged("FolderName");
            }
        }

        #endregion

        #region Commands
        private void OkButton_Click(object parameter)
        {
            combine.folderPath += "//" + FolderName;
            Json.ConvertJson.SaveFile(combine);
            this.CloseWindow();
        }
        private void CancelButton_Click(object parameter)
        {
            //((IDisposable)tileMap).Dispose();
            this.CloseWindow();
        }
        private void BrowseButton_Click(object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = true;
            var result = folderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = folderDialog.SelectedPath;
            }
        }
        #endregion

        public Models.Combine GetCombine()
        {
            return combine;
        }
    }
}
