using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using sztguiFeleves.Models;
using Xabe.FFmpeg;

namespace sztguiFeleves.Services
{
    public class ConversionService
    {
        public async Task ConvertFileAsync(string inputFilePath, string outputFilePath, Preset preset, Action<double>? onProgress = null)
        {
            try
            {
                // Set the FFmpeg path
                FFmpeg.SetExecutablesPath("C:\\Users\\olito\\Desktop\\6.(4.)félév\\Sztgui\\ffmpeg\\");

                var conversion = FFmpeg.Conversions.New()
                    .AddParameter($"-i \"{inputFilePath}\""); // Input file

                // Video codec
                if (preset.VideoCodec != Models.VideoCodec.Passthrough)
                    conversion.AddParameter($"-c:v {GetEnumDescription(preset.VideoCodec)}");

                // Pixel format
                if (preset.PixelFormat != Models.PixelFormat.Passthrough)
                    conversion.AddParameter($"-pix_fmt {GetEnumDescription(preset.PixelFormat)}");

                // CRF value
                if (preset.CRF >= 0 && preset.CRF <= 51)
                    conversion.AddParameter($"-crf {preset.CRF}");

                // Framerate
                if (preset.Framerate != Framerate.Passthrough)
                    conversion.AddParameter($"-r {GetEnumDescription(preset.Framerate)}");

                // Resolution
                if (preset.Resolution != Resolution.Passthrough)
                    conversion.AddParameter($"-s {GetEnumDescription(preset.Resolution)}");

                // Audio codec
                if (preset.AudioCodec != Models.AudioCodec.Passthrough)
                    conversion.AddParameter($"-c:a {GetEnumDescription(preset.AudioCodec)}");

                // Audio bitrate
                if (preset.AudioBitrate != AudioBitrate.Passthrough)
                    conversion.AddParameter($"-b:a {GetEnumDescription(preset.AudioBitrate)}");

                // Audio sample rate
                if (preset.AudioSampleRate != AudioSampleRate.Passthrough)
                    conversion.AddParameter($"-ar {GetEnumDescription(preset.AudioSampleRate)}");

                // Audio channels
                if (preset.AudioChannel != AudioChannels.Passthrough)
                    conversion.AddParameter($"-ac {GetEnumDescription(preset.AudioChannel)}");

                // Set output file
                conversion.SetOutput(outputFilePath);

                // Subscribe to the OnProgress event
                conversion.OnProgress += (sender, args) =>
                {
                    // Call the onProgress action with the progress percentage
                    onProgress?.Invoke(args.Percent);
                };

                // Start the conversion
                await conversion.Start();
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., log or display a message)
                throw new InvalidOperationException("An error occurred during the conversion process.", ex);
            }
        }


        private string GetEnumDescription(Enum value)
        {
            var description = value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            return description?.Description ?? value.ToString();
        }

    }
}
