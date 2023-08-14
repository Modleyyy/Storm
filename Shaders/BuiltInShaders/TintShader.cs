namespace Storm.Shaders.BuiltInShaders;

public struct TintShader : IPixelShader
{
    public Color tint = Color.White;

    public TintShader() {}

    Color IPixelShader.ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize)
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
    }
}