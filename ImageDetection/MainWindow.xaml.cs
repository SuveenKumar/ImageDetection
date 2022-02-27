using System;
using System.Collections.Generic;
using System.IO;
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
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;

namespace ImageDetection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path { get; set; } 
        public string ans { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ApiControl x = new ApiControl();
         //   Task<ApiModel> y = x.Get();
           // ans=y.Result.responses[0].textAnn[0].description;
           // Upload();
        }
        public void Upload()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\MyProjects\centered-center-342511-69c348a40f5b.json");
            string Pathsave = System.Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

            string filePath = path;
            var gcsStorage = StorageClient.Create();
            using (var f = File.OpenRead(filePath))
            {
                string objectName = System.IO.Path.GetFileName(filePath);
                    gcsStorage.UploadObject("osssk", objectName, null, f);
                Console.WriteLine($"Uploaded {objectName}.");
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                FileNameTextBox.Text = openFileDlg.FileName;
                path = openFileDlg.FileName;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Upload();
            TextBlock1.Text = ans;
        }
    }

    
}
