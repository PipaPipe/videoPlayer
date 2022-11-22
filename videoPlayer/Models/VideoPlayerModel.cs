using System;
using System.Collections.Generic;
using System.Text;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace videoPlayer.Models
{
    public class VideoPlayerModel : INotifyPropertyChanged
    {
        internal string pathOfVideoFile;
        private bool playStopControl;
        private bool closeOpenWindowContorl;

        // Реализация открытия/закрытия окна с видео
        public void CloseWindow()
        {
            closeOpenWindowContorl = true;
            OnPropertyChanged();
        }

        // Метод для закрытия окна
        /*public void CloseWindowCV(OpenCvSharp.Window window)
        {
            if (closeOpenWindowContorl)
            {
                window.Close();
            }
            closeOpenWindowContorl = false;
        }*/

        // Методы для проигрывания/паузы видео
        public void ChangeControlToPlay()
        {
            playStopControl = false;
            OnPropertyChanged();
        }

        public void ChangeControlToStop()
        {
            playStopControl = true;
            OnPropertyChanged();
        }

        public string PathOfVideoFile
        {
            get
            {
                return pathOfVideoFile;
            }
            set
            {
                pathOfVideoFile = value;
                OnPropertyChanged("pathOfVideoFile");

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

        // Метод для вывода видео в отдельном окне
        public void VideoOutput()
        {
            if (File.Exists(pathOfVideoFile))
            {
                try
                {
                    VideoCapture capture = new VideoCapture(pathOfVideoFile);
                    int sleepTime = (int)Math.Round(1000 / capture.Fps);

                    OpenCvSharp.Size dsize = new OpenCvSharp.Size(300, 300);
                    var windowVM = new OpenCvSharp.Window();
                    windowVM.Resize(300, 300);
                    // Буфер для видео
                    Mat image = new Mat();

                    while (true)
                    {
                        capture.Read(image);
                        if (image.Empty())
                            break;
                        Cv2.Resize(image, image, dsize, 300, 300);
                        windowVM.ShowImage(image);
                        Cv2.WaitKey(sleepTime);

                        while (playStopControl)
                        {
                            if (closeOpenWindowContorl)
                            {
                                break;
                            }
                            Thread.Sleep(500);
                        }
                        if (closeOpenWindowContorl)
                        {
                            closeOpenWindowContorl = false;
                            windowVM.Close();
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так!\nНажмите кнопку закрыть");
                }
            }
            else
            {
                MessageBox.Show("Указанного файла не существует!\nНажмите кнопку закрыть");
            }
        }
    }
}
