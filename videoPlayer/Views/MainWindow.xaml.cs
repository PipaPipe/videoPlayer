using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using videoPlayer.ViewModels;
using videoPlayer.Models;

namespace videoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /*List<ViewModel> viewModels = new List<ViewModel>();

            for(int i = 0; i < 4; ++i)
            {
                ViewModel model = new ViewModel();
                viewModels.Add(model);
            }

            DataContext = viewModels;*/

        /*private void Button_Click(object sender, RoutedEventArgs e)
{
   SmallVideoPlayer player = (SmallVideoPlayer)this.Resources["videos"]; // получаем ресурс по ключу            
   player.VideoOutput();
}*/


    }
}
