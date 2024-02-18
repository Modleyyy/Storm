namespace Storm.Shaders;

using System.ComponentModel.DataAnnotations;

static class BasicShaders {
    public record FlashShaderArgs
    {
        [Range(0f, 1f)]
        public float amount = 0f;
        public Color color = Color.White;
    }
    public static readonly PixelShaderDelegate<FlashShaderArgs> Flash = (
        Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture, FlashShaderArgs args) =>
    {
        if (pixelColor == args.color)
        {
            return pixelColor;
        }

        Color flashedColor = ShaderHelpers.Mix(pixelColor, args.color, args.amount);
        flashedColor = Color.FromArgb(flashedColor.A * pixelColor.A / 255, flashedColor);
        return flashedColor;
    };


    public static readonly PixelShaderDelegate UVColor = (
        Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture) =>
    {
        float r = 255 * uv.x;
        float g = 255 * uv.y;

        return Color.FromArgb(255, (byte)r, (byte)g, 0);
    };


    public static readonly PixelShaderDelegate Grayscale = (
        Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture) => ShaderHelpers.Grayscale(pixelColor);


    public static readonly PixelShaderDelegate InvertColor = (
        Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture) => ShaderHelpers.InvertColor(pixelColor);

    public static readonly PixelShaderDelegate<Color> Tint = (
        Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture, Color tint) =>
    {
        if (tint == Color.White)
        {
            return pixelColor;
        }
        byte a = (byte) (pixelColor.A * tint.A / 255);
        byte r = (byte) (pixelColor.R * tint.R / 255);
        byte g = (byte) (pixelColor.G * tint.G / 255);
        byte b = (byte) (pixelColor.B * tint.B / 255);

        return Color.FromArgb(a, r, g, b);
    };
}
