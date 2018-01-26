using System;
using System.Collections.Generic;
using System.Linq;

static class Extensions
{
    public static void Shuffle<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static void Shuffle<T>(this Random rng, IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static List<T> CollectionToList<T>(this System.Collections.ICollection other)
    {
        var output = new List<T>(other.Count);

        output.AddRange(other.Cast<T>());

        return output;
    }
}