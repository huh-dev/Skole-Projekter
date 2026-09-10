namespace Heltevagten;

public static class SearchEngine
{
    public static T FindFirst<T>(IEnumerable<T> collection, Func<T, bool> predicate)
    {
        foreach (T item in collection)
        {
            if (predicate(item))
            {
                return item;
            }
        }

        throw new InvalidOperationException("No matching element was found.");
    }

    public static IEnumerable<T> FindAll<T>(IEnumerable<T> collection, Func<T, bool> predicate)
    {
        List<T> matches = new List<T>();

        foreach (T item in collection)
        {
            if (predicate(item))
            {
                matches.Add(item);
            }
        }

        return matches;
    }
}
