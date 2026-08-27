using NAudio.Wave;

namespace AnimalSounds
{
    public class Cat : Animal
    {
        private AudioPlayer audioPlayer;

        public Cat(string name) : base(name)
        {
            audioPlayer = new AudioPlayer();
        }

        public override void MakeSound()
        {
            Console.WriteLine("Miau");
            audioPlayer.Play("./Sounds/cat.wav");
        }

        public override void StopSound()
        {
            audioPlayer?.Stop();
        }
    }
}
