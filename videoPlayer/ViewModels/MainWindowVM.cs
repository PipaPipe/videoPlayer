using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using videoPlayer.ViewModels;

namespace videoPlayer
{
    class MainWindowVM : INotifyPropertyChanged
    {
        readonly List<ViewModel> _viewModelList = new List<ViewModel>()
        {
            new ViewModel(), new ViewModel(), new ViewModel(), new ViewModel()
        };

        public List<ViewModel> ViewModelList
        {
            get
            {
                return _viewModelList;
            }
            set
            {
                ;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
