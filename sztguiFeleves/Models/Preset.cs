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
        [Description("libx264")]
        H264,
        [Description("libx265")]
        H265,
        [Description("mpeg2video")]
        MPEG2,
        [Description("mpeg4")]
        MPEG4,
        [Description("libaom-av1")]
        AV1,
        [Description("copy")]
        Passthrough
    }

    public enum PixelFormat
    {
        [Description("yuv420p")]
        yuv420p = 8,
        [Description("yuv420p10le")]
        yuv420p10le = 10,
        [Description("yuv420p12le")]
        yuv420p12le = 12,
        [Description("copy")]
        Passthrough
    }

    public enum Framerate
    {
        [Description("24")]
        Fps24,
        [Description("25")]
        Fps25,
        [Description("30")]
        Fps30,
        [Description("50")]
        Fps50,
        [Description("60")]
        Fps60,
        [Description("120")]
        Fps120,
        [Description("240")]
        Fps240,
        [Description("copy")]
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
        [Description("copy")]
        Passthrough
    }

    public enum AudioCodec
    {
        [Description("aac")]
        aac,
        [Description("libmp3lame")]
        mp3,
        [Description("ac3")]
        ac3,
        [Description("copy")]
        Passthrough
    }

    public enum AudioBitrate
    {
        [Description("32000")]
        kbps32 = 32,
        [Description("48000")]
        kbps48 = 48,
        [Description("64000")]
        kbps64 = 64,
        [Description("80000")]
        kbps80 = 80,
        [Description("96000")]
        kbps96 = 96,
        [Description("112000")]
        kbps112 = 112,
        [Description("128000")]
        kbps128 = 128,
        [Description("160000")]
        kbps160 = 160,
        [Description("192000")]
        kbps192 = 192,
        [Description("224000")]
        kbps224 = 224,
        [Description("256000")]
        kbps256 = 256,
        [Description("320000")]
        kbps320 = 320,
        [Description("384000")]
        kbps384 = 384,
        [Description("448000")]
        kbps448 = 448,
        [Description("512000")]
        kbps512 = 512,
        [Description("copy")]
        Passthrough
    }

    public enum AudioSampleRate
    {
        [Description("32000")]
        Hz32000,
        [Description("44100")]
        Hz44100,
        [Description("48000")]
        Hz48000,
        [Description("96000")]
        Hz96000,
        [Description("copy")]
        Passthrough
    }

    public enum OutputFormat
    {
        [Description("mp4")]
        mp4,
        [Description("matroska")]
        mkv,
        [Description("webm")]
        webm,
        [Description("mov")]
        mov,
        [Description("copy")]
        Passthrough
    }

    public enum AudioChannels
    {
        [Description("1")]
        Mono = 1,
        [Description("2")]
        Stereo = 2,
        [Description("copy")]
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
