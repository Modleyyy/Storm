namespace Storm.Utils;

public class MathHelper
{
    public static float Lerp(float start, float end, float t)
    {
        t = MathF.Max(0, MathF.Min(1, t));
        return start + (end - start) * t;
    }

    public static Color Lerp(Color startColor, Color endColor, float t)
    {
        t = Math.Max(0, Math.Min(1, t));

        byte r = (byte)(startColor.R + (endColor.R - startColor.R) * t);
        byte g = (byte)(startColor.G + (endColor.G - startColor.G) * t);
        byte b = (byte)(startColor.B + (endColor.B - startColor.B) * t);
        byte a = (byte)(startColor.A + (endColor.A - startColor.A) * t);

        return Color.FromArgb(a, r, g, b);
    }


    public static float RadToDeg(float radians)
    {
        return radians * (180 / MathF.PI);
    }

    public static float DegToRad(float degrees)
    {
        return degrees * (MathF.PI / 180);
    }
}
