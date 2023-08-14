# Shading sprites and VFX
> By the end of this tutorial, you'll learn to write simple shaders that you can use to modify the look of your `Sprite2D`s!

## Create a Shader
All you gotta do, is create a class or struct (preferably a struct, although a class works just as well) and make it extend the `Storm.Shaders.IPixelShader` interface! Then you override the `Color IPixelShader.ShaderCode(Color, Vector2, Vector2, Vector2)` method, and there you go! Example:
```csharp
// This is a built-in shader that you can access in the Storm.Shaders.BuiltInShaders namespace
public struct UVColorShader : IPixelShader
{
    public UVColorShader() {}

    Color IPixelShader.ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize)
    {
        // Write shader code here

        // Example: Draw a UV Map
        float r = 255 * uv.x;
        float g = 255 * uv.y;

        return Color.FromArgb(255, (byte)r, (byte)g, 0);
    }
}
```
All you need to use this shader is passing it as the second argument of the `Draw` method of your `Sprite2D`! Example:
```csharp
using Storm.Shaders.BuiltInShaders;

public class Player : GameObject
{
    /* ... */

    // Declare the shader
    UVColorShader shader;

    public Player(/* ... */) : base(/* ... */)
    {
        /* ... */

        // Instanciate the shader
        shader = new UVColorShader();
    }

    public override void OnDraw(Graphics graphics)
    {
        // Pass the shader as an argument
        sprite.Draw(graphics, shader);
    }
}
```
And that's it! You can also add properties to the shader to add more customization, or stuff that should change over time. For example:
```csharp
public struct FlashShader : IPixelShader
{
    // Properties
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
```
You will find alot of useful methods in `Storm.Shaders.ShaderHelpers`, such as `Mix` (shown in the previous example), `Fract`, `InvertColor`, `Grayscale`, `Step` and finally `SmoothStep`.