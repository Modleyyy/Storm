namespace Storm.Utils;

public static class RandomHelper
{
    private static readonly Random random = new();

    public static Random GetRandom() => random;

    public static T[] Shuffle<T>(T[] array)
    {
        lock (random)
        {
            T[] temp = (T[])array.Clone();
            int n = temp.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (temp[j], temp[i]) = (temp[i], temp[j]);
            }
            return temp;
        }
    }

    public static bool RandomBool()
    {
        return random.Next(0,2) == 1;
    }
}
