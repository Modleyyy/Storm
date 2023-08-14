namespace Storm.Shaders.BuiltInShaders;

public struct InvertShader : IPixelShader
{
    public InvertShader() {}

    Color IPixelShader.ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize)
    {
        return ShaderHelpers.InvertColor(pixelColor);
    }
}
