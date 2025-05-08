using Microsoft.Win32;
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
using System.Windows.Shapes;
using sztguiFeleves.Models;
using sztguiFeleves.Services;
using sztguiFeleves.ViewModels;
using Path = System.IO.Path;

namespace sztguiFeleves.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] SupportedFileExtensions = { ".mp4", ".webm", ".mkv", ".mov", ".MP4", ".WEBM", ".MKV", ".MOV" };

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



        /// Event handler for the "Start" button click
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = InputFilePathTextBox.Text;
            if (ValidateFilePath(filePath))
            {
                var viewModel = DataContext as MainWindowViewModel;

                if (viewModel == null)
                {
                    MessageBox.Show("ViewModel is not properly initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Reset progress before starting a new conversion
                viewModel.ConversionProgress = 0;

                // Create a preset (replace with actual combobox selections)
                var preset = new Preset
                {
                    VideoCodec = viewModel.SelectedVideoCodec,
                    PixelFormat = viewModel.SelectedPixelFormat,
                    CRF = viewModel.CrfValue,
                    Framerate = viewModel.SelectedFramerate,
                    Resolution = viewModel.SelectedResolution,
                    AudioCodec = viewModel.SelectedAudioCodec,
                    AudioBitrate = viewModel.SelectedAudioBitrate,
                    AudioSampleRate = viewModel.SelectedAudioSampleRate,
                    AudioChannel = viewModel.SelectedAudioChannel,

                    // OutputFormat is SelectedOutputFormat or if SelectedOutputFormat Passthrough, then use the file extension of the input file
                    OutputFormat = viewModel.SelectedOutputFormat == OutputFormat.Passthrough
                        ? (OutputFormat)Enum.Parse(typeof(OutputFormat), Path.GetExtension(filePath).TrimStart('.'))
                        : viewModel.SelectedOutputFormat
                };

                
                string outputFolderPath = Path.GetDirectoryName(filePath);
                string outputFileExtension = viewModel.SelectedOutputFormat == OutputFormat.Passthrough
                    ? Path.GetExtension(filePath).TrimStart('.')
                    : viewModel.SelectedOutputFormat.ToString().ToLower();

                string outputFilePath = Path.Combine(outputFolderPath, viewModel.OutputFileName + "." + outputFileExtension);

                var conversionService = new ConversionService();
                try
                {
                    await conversionService.ConvertFileAsync(filePath, outputFilePath, preset, viewModel.ffmpegPath, progress =>
                    {
                        // Update the ConversionProgress property
                        viewModel.ConversionProgress = progress;
                    });
                    MessageBox.Show("Conversion completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    viewModel.ConversionProgress = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Conversion failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    viewModel.ConversionProgress = 0;
                }
            }
        }

        /// Method to validate the file path
        private bool ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("The file path cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("The specified file does not exist. Please provide a valid file path.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            if (!SupportedFileExtensions.Contains(Path.GetExtension(filePath)))
            {
                MessageBox.Show("Invalid file type. Please select a valid file.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //MessageBox.Show("File path is valid!", "Validation Success", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }


    }
}
