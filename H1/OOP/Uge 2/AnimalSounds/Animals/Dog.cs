namespace AnimalSounds
{
    public class Dog : Animal
    {
        private AudioPlayer audioPlayer;

        public Dog(string name) : base(name)
        {
            audioPlayer = new AudioPlayer();
        }

        public override void MakeSound()
        {
            audioPlayer.Play("./Sounds/dog.wav");
        }

        public override void StopSound()
        {
            audioPlayer?.Stop();
        }
    }
}