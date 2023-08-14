namespace Storm.Shaders.BuiltInShaders;

public struct GrayscaleShader : IPixelShader
{
    public GrayscaleShader() {}

    Color IPixelShader.ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize)
    {
        return ShaderHelpers.Grayscale(pixelColor);
    }
}
