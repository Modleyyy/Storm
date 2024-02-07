namespace Storm.Utils;

public static class RandomHelper
{
    private static readonly Random random = new();


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

    public static float RandomFloat()
    {
        return random.NextSingle();
    }

    public static float RandomFloat(float max)
    {
        return random.NextSingle() * max;
    }

    public static float RandomFloat(float min, float max)
    {
        return random.NextSingle() * (max - min) + min;
    }


    public static double RandomDouble()
    {
        return random.NextDouble();
    }

    public static double RandomDouble(double max)
    {
        return random.NextDouble() * max;
    }

    public static double RandomDouble(double min, double max)
    {
        return random.NextDouble() * (max - min) + min;
    }


    public static int RandomInt()
    {
        return random.Next();
    }

    public static int RandomInt(int max)
    {
        return random.Next(max);
    }

    public static int RandomInt(int min, int max)
    {
        return random.Next(min, max);
    }
}
