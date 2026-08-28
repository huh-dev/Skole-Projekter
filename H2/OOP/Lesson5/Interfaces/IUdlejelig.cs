namespace Lesson05.Interfaces;

public interface IUdlejelig
{
    public decimal BeregnLejepris(int antalDage)
    {
        return 1000 * antalDage;
    }
}