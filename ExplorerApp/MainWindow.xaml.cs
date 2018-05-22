using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ExplorerApp
{
    public partial class MainWindow : Window
    {
        private List<File> newFiles;

        public MainWindow()
        {
            InitializeComponent();

            //files = new ObservableCollection<File>();
            //Explorer.ItemsSource = files;

            newFiles = new List<File>();

            Explorer.ItemsSource = newFiles;
            /*Thread thread = new Thread(new ThreadStart(GetFileList));
            thread.Start();*/
            GetFileList();
            //GetFileList2();
        }

        private void GetFileList()
        {
            string appDirectory = Directory.GetCurrentDirectory();

            IEnumerable<string> filePaths = Directory.EnumerateFiles(appDirectory);
            IEnumerable<string> catalogPaths = Directory.EnumerateDirectories(appDirectory);

            Parallel.ForEach(filePaths, path =>
            {
                FileInfo info = new FileInfo(path);

                File newFile = new File
                {
                    Name = info.Name,
                    CreateDate = info.CreationTime,
                    FilePath = info.FullName,
                    FileType = info.Extension,
                    Image = new BitmapImage(new Uri(appDirectory + "\\File.png"))
                };

                newFiles.Add(newFile);

                Dispatcher.Invoke(() =>
                {
                    Explorer.ItemsSource = null;
                    Explorer.ItemsSource = newFiles;
                });
            });

            Parallel.ForEach(catalogPaths, path =>
            {
                DirectoryInfo info = new DirectoryInfo(path);

                File newFile = new File
                {
                    Name = info.Name,
                    CreateDate = info.CreationTime,
                    FilePath = info.FullName,
                    FileType = info.Extension,
                    Image = new BitmapImage(new Uri(appDirectory + "\\Папка.png"))
                };

                newFiles.Add(newFile);

                Dispatcher.Invoke(() =>
                {
                    Explorer.ItemsSource = null;
                    Explorer.ItemsSource = newFiles;
                });
            });
        }

        // Тестовый метод, без использования Parallel
        private void GetFileList2()
        {
            string appDirectory = Directory.GetCurrentDirectory();

            IEnumerable<string> catalogPaths = Directory.EnumerateDirectories(appDirectory);
            IEnumerable<string> filePaths = Directory.EnumerateFiles(appDirectory);

            ObservableCollection<File> newFiles = new ObservableCollection<File>();
            Explorer.ItemsSource = newFiles;

            foreach (var path in filePaths)
            {
                FileInfo info = new FileInfo(path);

                File newFile = new File
                {
                    Name = info.Name,
                    CreateDate = info.CreationTime,
                    FilePath = info.FullName,
                    FileType = info.Extension,
                    Image = new BitmapImage(new Uri(appDirectory + "\\File.png"))
                };

                newFiles.Add(newFile);
            }

            foreach (var path in catalogPaths)
            {
                DirectoryInfo info = new DirectoryInfo(path);

                File newFile = new File
                {
                    Name = info.Name,
                    CreateDate = info.CreationTime,
                    FilePath = info.FullName,
                    FileType = info.Extension,
                    Image = new BitmapImage(new Uri(appDirectory + "\\Папка.png"))
                };

                newFiles.Add(newFile);
            }
        }
    }
}
