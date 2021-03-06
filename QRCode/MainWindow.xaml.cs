﻿using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using static QRCoder.PayloadGenerator;

namespace QRCodeSample
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

        private static Bitmap CreateQrCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
           return qrCode.GetGraphic(20);
        }

        private static Bitmap CreateBigQrCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            string text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "BigFile.txt"));

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        private static Bitmap CreateMailQrCode()
        {
            Mail generator = new Mail("dummymail@gmail.com", "This is a subject!!", "Hi Corrado, have a look at this QRCoder library!");
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        private static BitmapImage GetMoon()
        {
            string selectedFileName = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Moon.jpg");
            //FileNameLabel.Content = selectedFileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFileName);
            bitmap.EndInit();
            return bitmap;
        }

        private static BitmapImage GetQR()
        {
            return BitmapToBitmapImage(CreateQrCode());
        }

        private static BitmapImage GetMailQR()
        {
            return BitmapToBitmapImage(CreateMailQrCode());
        }

        private static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ImageViewer1.Source = GetMoon();
            //ImageViewer1.Source = GetQR();
            ImageViewer1.Source = GetMailQR();
            //ImageViewer1.Source = BitmapToBitmapImage(CreateBigQrCode());
        }
    }
}
