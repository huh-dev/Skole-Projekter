using NAudio.Wave;

namespace AnimalSounds
{
    public class AudioPlayer
    {
        public WaveOutEvent outputDevice;
        public AudioFileReader audioFile;

        private int volume = 100;

        public void Play(string path)
        {
            audioFile = new AudioFileReader(path);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Volume = volume / 100f;
            outputDevice.Play();
        }

        public void Stop()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
            audioFile?.Dispose();
        }
    }
}
