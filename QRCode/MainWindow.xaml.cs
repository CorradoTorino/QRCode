using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QRCode
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedFileName = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Moon.jpg");
            FileNameLabel.Content = selectedFileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFileName);
            bitmap.EndInit();
            ImageViewer1.Source = bitmap;
        }
    }
}
