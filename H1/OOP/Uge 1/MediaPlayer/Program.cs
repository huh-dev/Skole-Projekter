using System;
using System.Diagnostics;
using NAudio.Wave;

namespace MediaPlayer;

class Program {
    static void Main(string[] args) 
    {
        VideoPlayer videoPlayer = new VideoPlayer();
        ImagePlayer imagePlayer = new ImagePlayer();
        AudioPlayer audioPlayer = new AudioPlayer();
        // videoPlayer.Play();
        audioPlayer.Play();
        // imagePlayer.Play();
    }

    internal class VideoPlayer : IMediaPlayer 
    {
        public void Play() 
        {
            string video = "https://www.youtube.com/watch?v=o0CFeS_EJR4";
            Process.Start(new ProcessStartInfo(video) { UseShellExecute = true });
        }
    }

    internal class ImagePlayer : IMediaPlayer 
    {
        public void Play() 
        {
            string image = "https://www.nc-nielsen.dk/files/images/ncnielsen/ecom/groups/luis_edge_ai-kamera/list.jpg";
            Process.Start(new ProcessStartInfo(image) { UseShellExecute = true });
        }
    }

    internal class AudioPlayer : IMediaPlayer, IVolume 
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        private int volume {get; set;} = 50;
        public void SetVolume(int volumeLevel)
        {
            // Math.Clamp it is used here to avoid using if statements to simplify the code
            volumeLevel = Math.Clamp(volumeLevel, 0, 100);

            volume = volumeLevel;

            // Set the volume on the output device if it's not null which is defined by the ? operator
            if (outputDevice != null)
            {
                outputDevice.Volume = volume / 100f;
            }
        }

        public void GetVolume()
        {
            if (outputDevice != null)
            {
                Console.WriteLine($"Current volume: {outputDevice.Volume * 100}%");
            }
            else
            {
                Console.WriteLine($"Current volume: {volume}%");
            }
        }

        public void AudioSettings()
        {
            audioFile = new AudioFileReader("./Optagelse.m4a");
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Volume = volume / 100f;
            outputDevice.Play();
        }

        public void Play() 
        {
            bool isPlaying = true;
            AudioSettings();

            while (isPlaying)
            {
                Console.WriteLine(" Current volume: " + volume);
                Console.WriteLine(" 1) Set volume up by 10");
                Console.WriteLine(" 2) Set volume down by 10");
                Console.WriteLine(" 3) Stop audio");
                Console.WriteLine(" 4) Restart audio");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        SetVolume(volume + 10);
                        break;
                    case "2":
                        SetVolume(volume - 10);
                        break;
                    case "3":
                        isPlaying = false;
                        outputDevice.Stop();
                        outputDevice.Dispose();
                        audioFile.Dispose();
                        break;
                    case "4":
                        outputDevice.Stop();
                        AudioSettings();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
    
    
}