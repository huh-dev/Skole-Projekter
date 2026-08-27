namespace AnimalSounds
{
    public class Sheep : Animal
    {
        private AudioPlayer audioPlayer;

        public Sheep(string name) : base(name)
        {
            audioPlayer = new AudioPlayer();
        }

        public override void MakeSound()
        {
            audioPlayer.Play("./Sounds/sheep.wav");
        }

        public override void StopSound()
        {
            audioPlayer?.Stop();
        }
    }
}
