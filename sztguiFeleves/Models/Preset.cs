using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sztguiFeleves.Models
{
    public enum VideoCodec
    {
        [Description("H.264")]
        H264,
        [Description("H.265")]
        H265,
        [Description("MPEG-2")]
        MPEG2,
        [Description("MPEG-4")]
        MPEG4,
        [Description("AV1")]
        AV1,
        Passthrough
    }

    public enum PixelFormat
    {
        [Description("8-bit")]
        yuv420p = 8,
        [Description("10-bit")]
        yuv420p10le = 10,
        [Description("12-bit")]
        yuv420p12le = 12,
        Passthrough
    }

    public enum Framerate
    {
        [Description("24 fps")]
        Fps24,
        [Description("25 fps")]
        Fps25,
        [Description("30 fps")]
        Fps30,
        [Description("50 fps")]
        Fps50,
        [Description("60 fps")]
        Fps60,
        [Description("120 fps")]
        Fps120,
        [Description("240 fps")]
        Fps240,
        Passthrough
    }

    public enum Resolution
    {
        [Description("1920x1080")]
        R1920x1080,
        [Description("1280x720")]
        R1280x720,
        [Description("640x360")]
        R640x360,
        [Description("320x180")]
        R320x180,
        [Description("2560x1440")]
        R2560x1440,
        [Description("3840x2160")]
        R3840x2160,
        Passthrough
    }

    public enum AudioCodec
    {
        aac,
        mp3,
        ac3,
        Passthrough
    }

    public enum AudioBitrate
    {
        [Description("32 kbps")]
        kbps32 = 32,
        [Description("48 kbps")]
        kbps48 = 48,
        [Description("64 kbps")]
        kbps64 = 64,
        [Description("80 kbps")]
        kbps80 = 80,
        [Description("96 kbps")]
        kbps96 = 96,
        [Description("112 kbps")]
        kbps112 = 112,
        [Description("128 kbps")]
        kbps128 = 128,
        [Description("160 kbps")]
        kbps160 = 160,
        [Description("192 kbps")]
        kbps192 = 192,
        [Description("224 kbps")]
        kbps224 = 224,
        [Description("256 kbps")]
        kbps256 = 256,
        [Description("320 kbps")]
        kbps320 = 320,
        [Description("384 kbps")]
        kbps384 = 384,
        [Description("448 kbps")]
        kbps448 = 448,
        [Description("512 kbps")]
        kbps512 = 512,
        Passthrough
    }

    public enum AudioSampleRate
    {
        [Description("32 kHz")]
        Hz32000,
        [Description("44.1 kHz")]
        Hz44100,
        [Description("48 kHz")]
        Hz48000,
        [Description("96 kHz")]
        Hz96000,
        Passthrough
    }

    public enum OutputFormat
    {
        mp4,
        mkv,
        webm,
        Passthrough
    }

    public enum AudioChannels
    {
        Mono = 1,
        Stereo = 2,
        Passthrough
    }

    public class Preset
    {
        public string Name { get; set; } // Preset name
        public VideoCodec VideoCodec { get; set; } // h264, h265, mpeg2, mpeg4, av1
        public PixelFormat PixelFormat { get; set; } // 8 bit: PixelFormat.yuv420p, 10 bit: PixelFormat.yuv420p10le, 12 bit: PixelFormat.yuv420p12le
        public int CRF { get; set; } // 0-51, 0 = lossless, 23 = default, 51 = worst quality
        public Framerate Framerate { get; set; } // 24, 25, 30, 50, 60, 120, 240
        public Resolution Resolution { get; set; } // 1920x1080, 1280x720, 640x360, 320x180, 2560x1440, 3840x2160, passthrough
        public AudioCodec AudioCodec { get; set; } // aac, mp3, ac3, passthrough
        public AudioBitrate AudioBitrate { get; set; } // 64, 80, 96, 112, 128, 160, 192, 224 256, 320, 384, 448, 512
        public AudioSampleRate AudioSampleRate { get; set; } // 32000, 44100, 48000, 96000 
        public AudioChannels AudioChannel { get; set; } // 1 = mono, 2 = stereo
        public OutputFormat OutputFormat { get; set; } // mp4, mkv, webm
        public string Description { get; set; } // Description of the preset


        public Preset()
        {

        }

        public Preset(
            string name,
            VideoCodec videoCodec,
            PixelFormat pixelFormat,
            int crf,
            Framerate framerate,
            Resolution resolution,
            AudioCodec audioCodec,
            AudioBitrate audioBitrate,
            AudioSampleRate audioSampleRate,
            AudioChannels audioChannels,
            OutputFormat outputFormat,
            string description)
        {
            Name = name;
            VideoCodec = videoCodec;
            PixelFormat = pixelFormat;
            CRF = crf;
            Framerate = framerate;
            Resolution = resolution;
            AudioCodec = audioCodec;
            AudioBitrate = audioBitrate;
            AudioSampleRate = audioSampleRate;
            AudioChannel = audioChannels;
            OutputFormat = outputFormat;
            Description = description;
        }


    }
}
