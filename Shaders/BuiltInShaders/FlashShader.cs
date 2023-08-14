namespace Storm.Shaders.BuiltInShaders;

using System.ComponentModel.DataAnnotations;

public struct FlashShader : IPixelShader
{
    [Range(0f, 1f)]
    public float flashAmount = 0f;
    public Color flashColor = Color.White;

    public FlashShader() {}

    Color IPixelShader.ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize)
    {
        if (pixelColor == flashColor)
        {
            return pixelColor;
        }
        
        Color flashedColor = ShaderHelpers.Mix(pixelColor, flashColor, flashAmount);
        flashedColor = Color.FromArgb(flashedColor.A * pixelColor.A / 255, flashedColor);
        return flashedColor;
    }
}
