namespace Storm.Shaders;

using System.Diagnostics;

public static class ShaderHelpers
{
    public static Color GetPixelColorUV(Bitmap image, Vector2 uv)
    {
        if (uv.x > 0 && uv.x < 1)
            uv.x = 1 - uv.x;
        if (uv.y > 0 && uv.y < 1)
            uv.y = 1 - uv.y;

        int x = (int)Math.Floor(uv.x * image.Width);
        int y = (int)Math.Floor(uv.y * image.Height);

        return image.GetPixel(x, y);
    }

    public static Color GetPixelColor(Bitmap image, Vector2 coords)
    {
        if (coords.x > 0 && coords.x < image.Width)
            coords.x %= image.Width;
        if(coords.y > 0 && coords.y < image.Height)
            coords.y %= image.Height;

        int x = (int)coords.x;
        int y = (int)coords.y;

        return image.GetPixel(x, y);
    }

    public static Color InvertColor(Color color)
    {
        return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
    }

    public static Color Grayscale(Color color)
    {
        int brightness = (color.R + color.G + color.B) / 3;
        return Color.FromArgb(color.A, brightness, brightness, brightness);
    }

    public static float Fract(float value)
    {
        return value - MathF.Floor(value);
    }

    public static Color Mix(Color color1, Color color2, float ratio)
    {
        ratio = Math.Clamp(ratio, 0, 1);
        
        byte r = (byte)(color1.R * (1f - ratio) + color2.R * ratio);
        byte g = (byte)(color1.G * (1f - ratio) + color2.G * ratio);
        byte b = (byte)(color1.B * (1f - ratio) + color2.B * ratio);

        return Color.FromArgb(r, g, b);
    }

    public static void Normalize(Color color, out float R, out float G, out float B, out float A)
    {
        A = color.A / 255;
        R = color.R / 255;
        G = color.G / 255;
        B = color.B / 255;
    }

    public static Color Denormalize(float R, float G, float B, float A)
    {
        Color c = Color.FromArgb((byte)A*255, (byte)R*255, (byte)G*255, (byte)B*255);
        return c;
    }

    public static float Step(float edge, float x)
    {
        return x < edge ? 0f : 1f;
    }

    public static float SmoothStep(float edge0, float edge1, float x)
    {
        float t = Math.Clamp((x - edge0) / (edge1 - edge0), 0f, 1f);
        return t * t * (3f - 2f * t);
    }
}
