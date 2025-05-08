using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using sztguiFeleves.Helper;
using sztguiFeleves.Models;

namespace sztguiFeleves.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        // Path to FFmpeg executable
        //public string ffmpegPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\ffmpeg"));
        public string ffmpegPath = "./Resources/ffmpeg"; 

        private string _outputFileName;
        public string OutputFileName
        {
            get => _outputFileName;
            set
            {
                if (_outputFileName != value)
                {
                    _outputFileName = value;
                    OnPropertyChanged(nameof(OutputFileName));
                }
            }
        }


        private double _conversionProgress;
        public double ConversionProgress
        {
            get => _conversionProgress;
            set
            {
                if (_conversionProgress != value)
                {
                    _conversionProgress = value;
                    OnPropertyChanged(nameof(ConversionProgress));
                }
            }
        }


        private Preset _selectedPreset;
        public Preset SelectedPreset
        {
            get => _selectedPreset;
            set
            {
                if (_selectedPreset != value)
                {
                    _selectedPreset = value;
                    OnPropertyChanged(nameof(SelectedPreset));
                    UpdateComboBoxValues(); // Update ComboBox values when a preset is selected
                }
            }
        }


        private int _crfValue;
        public int CrfValue
        {
            get => _crfValue;
            set
            {
                if (_crfValue != value)
                {
                    _crfValue = value;
                    OnPropertyChanged(nameof(CrfValue));
                }
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged(nameof(FilePath));
                    LoadFileMetadata();
                    UpdateOutputFileName();
                }
            }
        }

        //Video and Audio Metadata from the input file
        public string OriginalVideoCodec { get; set; }
        public string OriginalPixelFormat { get; set; }
        public string OriginalFrameRate { get; set; }
        public string OriginalResolution { get; set; }
        public string OriginalOutputFormat { get; set; }
        public string OriginalAudioCodec { get; set; }
        public string OriginalAudioBitrate { get; set; }
        public string OriginalAudioSampleRate { get; set; }
        public string OriginalAudioChannels { get; set; }


        // ComboBox values for Video Codec
        public IEnumerable<VideoCodec> VideoCodecs => EnumHelper.GetValues<VideoCodec>();
        public VideoCodec SelectedVideoCodec { get; set; }
        // ComboBox values for Pixel Format
        public IEnumerable<PixelFormat> PixelFormats => EnumHelper.GetValues<PixelFormat>();
        public PixelFormat SelectedPixelFormat { get; set; }

        // ComboBox values for Framerate
        public IEnumerable<Framerate> Framerates => EnumHelper.GetValues<Framerate>();
        public Framerate SelectedFramerate { get; set; }

        // ComboBox values for Resolution
        public IEnumerable<Resolution> Resolutions => EnumHelper.GetValues<Resolution>();
        public Resolution SelectedResolution { get; set; }

        // ComboBox values for Output Format
        public IEnumerable<OutputFormat> OutputFormats => EnumHelper.GetValues<OutputFormat>();
        public OutputFormat SelectedOutputFormat { get; set; }

        // ComboBox values for Audio Codec
        public IEnumerable<AudioCodec> AudioCodecs => EnumHelper.GetValues<AudioCodec>();
        public AudioCodec SelectedAudioCodec { get; set; }

        // ComboBox values for Audio Bitrate
        public IEnumerable<AudioBitrate> AudioBitrates => EnumHelper.GetValues<AudioBitrate>();
        public AudioBitrate SelectedAudioBitrate { get; set; }

        // ComboBox values for Audio Sample Rate
        public IEnumerable<AudioSampleRate> AudioSampleRates => EnumHelper.GetValues<AudioSampleRate>();
        public AudioSampleRate SelectedAudioSampleRate { get; set; }

        // ComboBox values for Audio Channels
        public IEnumerable<AudioChannels> AudioChannel => EnumHelper.GetValues<AudioChannels>();
        public AudioChannels SelectedAudioChannel { get; set; }



        public BindingList<Preset> Presets { get; set; } // List of presets
        public MainWindowViewModel()
        {
            // Initialize default values for metadata
            OriginalVideoCodec = "-";
            OriginalPixelFormat = "-";
            OriginalFrameRate = "-";
            OriginalResolution = "-";
            OriginalOutputFormat = "-";

            OriginalAudioCodec = "-";
            OriginalAudioBitrate = "-";
            OriginalAudioSampleRate = "-";
            OriginalAudioChannels = "-";

            // Set default values for ComboBox selections
            SelectedVideoCodec = VideoCodec.Passthrough;
            SelectedPixelFormat = PixelFormat.Passthrough;
            SelectedFramerate = Framerate.Passthrough;
            SelectedResolution = Resolution.Passthrough;
            SelectedOutputFormat = OutputFormat.Passthrough;

            CrfValue = 23;

            // Set default values for Audio Codec
            SelectedAudioCodec = AudioCodec.Passthrough;
            SelectedAudioBitrate = AudioBitrate.Passthrough;
            SelectedAudioSampleRate = AudioSampleRate.Passthrough;
            SelectedAudioChannel = AudioChannels.Passthrough;



            this.Presets = new BindingList<Preset>()
            {
                new Preset("Archive to H.265 1080p",VideoCodec.H265, PixelFormat.yuv420p,18,Framerate.Passthrough, Resolution.R1920x1080,AudioCodec.Passthrough,AudioBitrate.Passthrough,AudioSampleRate.Passthrough, AudioChannels.Passthrough,OutputFormat.mp4, "Basic Archice"),

                new Preset("H.265 10bit 1080p 24fps",Models.VideoCodec.H265,Models.PixelFormat.yuv420p10le,23,Models.Framerate.Fps24,Models.Resolution.R1920x1080,Models.AudioCodec.aac,(AudioBitrate)192,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Standard quality H.265 preset for archival purposes."),

                new Preset("H.264 8bit 720p 30fps", Models.VideoCodec.H264,Models.PixelFormat.yuv420p,28,Models.Framerate.Fps30,Models.Resolution.R1280x720,Models.AudioCodec.aac,(AudioBitrate)128,Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.mp4,"Mobile-optimized preset with reduced file size."),

                new Preset("YouTube H.264 1080p 60fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,20,Models.Framerate.Fps60,Models.Resolution.R1920x1080,Models.AudioCodec.aac,(AudioBitrate)320,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mp4,"High-quality preset suitable for YouTube uploads."),

                new Preset("Lossless H.264 1080p 30fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,0,Models.Framerate.Fps30,Models.Resolution.R1920x1080,Models.AudioCodec.Passthrough,(AudioBitrate)320,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Lossless preset for maximum quality retention."),

                new Preset("Gaming H.265 1440p 60fps",Models.VideoCodec.H265,Models.PixelFormat.yuv420p10le,24,Models.Framerate.Fps60,Models.Resolution.R2560x1440,Models.AudioCodec.aac,(AudioBitrate)256,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Optimized for high-quality gaming footage."),

                new Preset("Old Film H.264 576p 25fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,20,Models.Framerate.Fps25,Models.Resolution.R640x360,Models.AudioCodec.ac3,(AudioBitrate)192,Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Preset tailored for archiving old films."),

                new Preset("VHS Capture H.264 576p 25fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,18,Models.Framerate.Fps25,Models.Resolution.R640x360,Models.AudioCodec.aac,(AudioBitrate)128,Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.mp4,"Suitable for digitizing VHS tapes."),

                new Preset("SDTV Archive H.264 576p 25fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,20,Models.Framerate.Fps25,Models.Resolution.R640x360,Models.AudioCodec.aac,(AudioBitrate)128,
                    Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.mp4,"Optimized for standard-definition TV content."),

                new Preset("4K Archive H.265 10bit 24fps",Models.VideoCodec.H265,Models.PixelFormat.yuv420p10le,20,Models.Framerate.Fps24,Models.Resolution.R3840x2160,Models.AudioCodec.aac,(AudioBitrate)320,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"High-quality preset for 4K video archiving."),

                new Preset("AV1 Test 1080p 30fps",Models.VideoCodec.AV1,Models.PixelFormat.yuv420p,30,Models.Framerate.Fps30,Models.Resolution.R1920x1080,Models.AudioCodec.aac,(AudioBitrate)192,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Experimental preset for testing AV1 codec."),

                new Preset("Archival H.265 12bit 24fps",Models.VideoCodec.H265,Models.PixelFormat.yuv420p12le,20,Models.Framerate.Fps24,Models.Resolution.R1920x1080,Models.AudioCodec.Passthrough,(AudioBitrate)320,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Professional archival with 12-bit color depth."),

                new Preset("Social Media H.264 1080p 30fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,26,Models.Framerate.Fps30,Models.Resolution.R1920x1080,Models.AudioCodec.mp3,(AudioBitrate)160,Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.mp4,"Optimized settings for social media platforms."),

                new Preset("Low Bandwidth H.264 360p 24fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,35,Models.Framerate.Fps24,Models.Resolution.R640x360,Models.AudioCodec.aac,(AudioBitrate)96,Models.AudioSampleRate.Hz32000,Models.AudioChannels.Mono,Models.OutputFormat.mp4,"Ideal for low bandwidth with small file size."),

                new Preset("H.265 Ultra Low Bitrate",Models.VideoCodec.H265,Models.PixelFormat.yuv420p,35,Models.Framerate.Fps25,Models.Resolution.R320x180,Models.AudioCodec.mp3,(AudioBitrate)64,Models.AudioSampleRate.Hz32000,Models.AudioChannels.Mono,Models.OutputFormat.mp4,"Extremely small size and bandwidth for archival."),

                new Preset("Fast Transcode H.264 720p 30fps",Models.VideoCodec.H264,Models.PixelFormat.yuv420p,28,Models.Framerate.Fps30,Models.Resolution.R1280x720,Models.AudioCodec.Passthrough,(AudioBitrate)192,Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Fast transcoding with minimal quality loss."),

                new Preset("Broadcast MPEG2 720p 25fps",Models.VideoCodec.MPEG2,Models.PixelFormat.yuv420p,18,Models.Framerate.Fps25,Models.Resolution.R1280x720,Models.AudioCodec.ac3,(AudioBitrate)256,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"MPEG2 broadcast format for archival purposes."),

                new Preset("AV1 Archive 1080p 24fps",Models.VideoCodec.AV1,Models.PixelFormat.yuv420p,30,Models.Framerate.Fps24,Models.Resolution.R1920x1080,Models.AudioCodec.aac,(AudioBitrate)128,Models.AudioSampleRate.Hz44100,Models.AudioChannels.Stereo,Models.OutputFormat.webm,"AV1 codec for archival with good quality at low bitrate."),

                new Preset("Passthrough Audio H.265 720p",Models.VideoCodec.H265,Models.PixelFormat.yuv420p10le,26,Models.Framerate.Fps30,Models.Resolution.R1280x720,Models.AudioCodec.Passthrough,(AudioBitrate)192,Models.AudioSampleRate.Hz48000,Models.AudioChannels.Stereo,Models.OutputFormat.mkv,"Compress video to H.265 while keeping original audio.")
            };

            _filePath = string.Empty; // Initialize _filePath to avoid CS8618
            PropertyChanged = null; // Initialize PropertyChanged to avoid CS8618

        }


        private async Task LoadFileMetadata()
        {
            // Simulate loading metadata from the file
            if (!string.IsNullOrEmpty(FilePath))
            {
                try
                {
                    // TODO: Check if the file exists
                    // Initialize FFmpeg
                    Xabe.FFmpeg.FFmpeg.SetExecutablesPath(ffmpegPath);


                    // Get media info
                    var mediaInfo = await Xabe.FFmpeg.FFmpeg.GetMediaInfo(FilePath);

                    // Extract metadata
                    var videoStream = mediaInfo.VideoStreams.FirstOrDefault();
                    var audioStream = mediaInfo.AudioStreams.FirstOrDefault();

                    if (videoStream != null)
                    {
                        OriginalVideoCodec = videoStream.Codec;
                        OriginalPixelFormat = videoStream.PixelFormat;
                        OriginalFrameRate = $"{videoStream.Framerate} fps";
                        OriginalResolution = $"{videoStream.Width}x{videoStream.Height}";
                        OriginalOutputFormat = Path.GetExtension(FilePath)?.TrimStart('.').ToLower();
                    }


                    if (audioStream != null)
                    {
                        OriginalAudioCodec = audioStream.Codec;
                        OriginalAudioBitrate = $"{audioStream.Bitrate / 1000} kbps";
                        OriginalAudioSampleRate = $"{audioStream.SampleRate} Hz";
                        OriginalAudioChannels = audioStream.Channels == 1 ? "Mono" : "Stereo";
                    }

                    // Notify the UI of property changes
                    OnPropertyChanged(nameof(OriginalVideoCodec));
                    OnPropertyChanged(nameof(OriginalPixelFormat));
                    OnPropertyChanged(nameof(OriginalFrameRate));
                    OnPropertyChanged(nameof(OriginalResolution));
                    OnPropertyChanged(nameof(OriginalOutputFormat));
                    OnPropertyChanged(nameof(OriginalAudioCodec));
                    OnPropertyChanged(nameof(OriginalAudioBitrate));
                    OnPropertyChanged(nameof(OriginalAudioSampleRate));
                    OnPropertyChanged(nameof(OriginalAudioChannels));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load metadata: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void UpdateComboBoxValues()
        {
            if (SelectedPreset != null)
            {
                // Update ComboBox values based on the selected preset
                SelectedVideoCodec = SelectedPreset.VideoCodec;
                SelectedPixelFormat = SelectedPreset.PixelFormat;
                SelectedFramerate = SelectedPreset.Framerate;
                SelectedResolution = SelectedPreset.Resolution;
                SelectedOutputFormat = SelectedPreset.OutputFormat;
                SelectedAudioCodec = SelectedPreset.AudioCodec;
                SelectedAudioBitrate = SelectedPreset.AudioBitrate;
                SelectedAudioSampleRate = SelectedPreset.AudioSampleRate;
                SelectedAudioChannel = SelectedPreset.AudioChannel;

                // Notify the UI of property changes
                OnPropertyChanged(nameof(SelectedVideoCodec));
                OnPropertyChanged(nameof(SelectedPixelFormat));
                OnPropertyChanged(nameof(SelectedFramerate));
                OnPropertyChanged(nameof(SelectedResolution));
                OnPropertyChanged(nameof(SelectedOutputFormat));
                OnPropertyChanged(nameof(SelectedAudioCodec));
                OnPropertyChanged(nameof(SelectedAudioBitrate));
                OnPropertyChanged(nameof(SelectedAudioSampleRate));
                OnPropertyChanged(nameof(SelectedAudioChannel));
            }
        }



        private void UpdateOutputFileName()
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                string inputFileName = Path.GetFileNameWithoutExtension(FilePath);
                OutputFileName = $"{inputFileName}_converted";
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
