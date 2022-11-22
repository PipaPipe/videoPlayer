using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using videoPlayer.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using videoPlayer.Commands;
using System.Threading.Tasks;


namespace videoPlayer.ViewModels
{
    public class ViewModel : INotifyPropertyChanged 
    {
        private VideoPlayerModel selectedPlayer = new VideoPlayerModel();
        private RelayCommand _startVideo;
        private RelayCommand _pauseVideo;
        private RelayCommand _continueVideo;
        private RelayCommand _closeVideo;
        private string _pathVids;
        private bool _isVisibleControlPanel;
        private bool _isVisibleStartPanel = true;

        public string PathVids
        {
            set
            {
                _pathVids = value;
                selectedPlayer.pathOfVideoFile = _pathVids;
                OnPropertyChanged();
            }
            get
            {
                return _pathVids;
            }
        }

        public bool IsVisibleStartPanel
        {
            set
            {
                _isVisibleStartPanel = value;
                OnPropertyChanged();
            }
            get
            {
                return _isVisibleStartPanel;
            }
        }

        public bool IsVisibleControlPanel
        {
            set
            {
                _isVisibleControlPanel = value;
                OnPropertyChanged();
            }
            get
            {
                return _isVisibleControlPanel;
            }
        }

        // Команда для паузы видео
        public RelayCommand PauseVideo
        {
            get
            {
                return _pauseVideo ??
                    (_pauseVideo = new RelayCommand(obj =>
                    {
                        selectedPlayer.ChangeControlToStop();
                    }));
            }
        }

        // Команда для продолжения видео
        public RelayCommand ContinueVideo
        {
            get
            {
                return _continueVideo ??
                    (_continueVideo = new RelayCommand(obj =>
                    {
                        selectedPlayer.ChangeControlToPlay();
                    }));
            }
        }

        // Команда для запуска видео в отдельном окне
        public RelayCommand StartVideo
        {
            get
            {
                return _startVideo ??
                    (_startVideo = new RelayCommand(async obj =>
                    {
                        
                        await Task.Run(() =>
                        {
                            IsVisibleControlPanel = true;
                            IsVisibleStartPanel = false;
                            selectedPlayer.VideoOutput();
                        });
                        
                    }));
            }
        }

        // Команда для закрытия окна с видео
        public RelayCommand CloseVideo
        {
            get
            {
                return _closeVideo ??
                    (_closeVideo = new RelayCommand(obj =>
                    {
                        IsVisibleControlPanel = false;
                        IsVisibleStartPanel = true;
                        selectedPlayer.CloseWindow();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }   

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
