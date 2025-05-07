using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sztguiFeleves.Models;

namespace sztguiFeleves.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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
                }
            }
        }



        public BindingList<Preset> Presets { get; set; } // List of presets
        public MainWindowViewModel()
        {
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

        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
