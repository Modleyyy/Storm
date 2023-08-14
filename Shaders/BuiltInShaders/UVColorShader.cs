namespace Storm.Shaders.BuiltInShaders;

public struct UVColorShader : IPixelShader
{
    public UVColorShader() {}

    Color IPixelShader.ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize)
    {
        float r = 255 * uv.x;
        float g = 255 * uv.y;

        return Color.FromArgb(255, (byte)r, (byte)g, 0);
    }
}
