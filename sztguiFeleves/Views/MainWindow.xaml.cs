using Microsoft.Win32;
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
using System.Windows.Shapes;
using sztguiFeleves.ViewModels;
using Path = System.IO.Path;

namespace sztguiFeleves.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] SupportedFileExtensions = { ".mp4", ".webm", ".mkv", ".MP4" };

        private MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }



        // Implementing drag-and-drop functionality for the TextBox
        private void InputFilePathTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            // Allow drag-and-drop only if the data contains file paths
            e.Handled = true;
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }
        
        private void InputFilePathTextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0];
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    // Validate the file extension
                    if (IsSupportedFileType(fileExtension))
                    {
                        InputFilePathTextBox.Text = filePath;

                        // Ensure the DataContext is cast to MainWindowViewModel
                        if (DataContext is MainWindowViewModel viewModel)
                        {
                            viewModel.FilePath = filePath;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unsupported file type. Please select a valid video file.", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
        
        
        private bool IsSupportedFileType(string fileExtension)
        {
            return Array.Exists(SupportedFileExtensions, ext => ext == fileExtension);
        }

        /// Event handler for the "Browse" button click
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create and configure the OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files (*.mp4;*.webm;*.mkv)|*.mp4;*.webm;*.mkv|All Files (*.*)|*.*",
                Title = "Select a File to Convert"
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileExtension = Path.GetExtension(filePath).ToLower();

                // Validate the file extension
                if (IsSupportedFileType(fileExtension))
                {
                    InputFilePathTextBox.Text = filePath;
                    if (DataContext is MainWindowViewModel viewModel)
                    {
                        viewModel.FilePath = filePath;
                    }
                }
                else
                {
                    MessageBox.Show("Unsupported file type. Please select a valid video file.", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }





    }
}
