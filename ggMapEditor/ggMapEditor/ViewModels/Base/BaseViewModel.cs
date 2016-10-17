﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ggMapEditor.ViewModels.Base
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        Nullable<bool> closeWindowFlag;
        public Nullable<bool> CloseWindowFlag
        {
            get { return closeWindowFlag; }
            set
            {
                closeWindowFlag = value;
                RaisePropertyChanged("CloseWindowFlag");
            }
        }
        public virtual void CloseWindow(Nullable<bool> result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                if (CloseWindowFlag == null)
                    CloseWindowFlag = true;
                else
                    CloseWindowFlag = !CloseWindowFlag;
            })); 
        }
    }
}
