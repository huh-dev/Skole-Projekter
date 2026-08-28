namespace Lesson05.Interfaces;

public interface IForsikringspligtig
{
    public decimal BeregnForsikringspraemie(decimal pris)
    {
        return pris * 0.1m;
    }
}