# Shading sprites and VFX
> By the end of this tutorial, you'll learn to write simple shaders that you can use to modify the look of your `Sprite2D`s!

## Create a Shader
All you gotta do, is create a function implementing the `Storm.Shaders.PixelShaderDelegate` delegate in some kind of static global class or something, and return a `Color`! Example:
```csharp
public readonly PixelShaderDelegate UVColorShader = (
    Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize) =>
{
    float r = 255 * uv.x;
    float g = 255 * uv.y;

    return Color.FromArgb(255, (byte)r, (byte)g, 0);
};
```
You can also write shaders with arguments using the `Storm.Shaders.PixelShaderDelegate<TArgs>` delegate type. I like to use records for the argument types, but you can use structs and classes if you wish. Example:
```csharp
// Argument type (record)
public record FlashShaderArgs
{
    [Range(0f, 1f)]
    public float amount = 0f;
    public Color color = Color.White;
}

// The Shader
public static readonly PixelShaderDelegate<FlashShaderArgs> Flash = ( // We pass in the FlashShaderArgs as the argument type (<TArgs>)
    Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize, FlashShaderArgs args) =>
{
    if (pixelColor == args.color)
    {
        return pixelColor;
    }

    Color flashedColor = ShaderHelpers.Mix(pixelColor, args.color, args.amount);
    flashedColor = Color.FromArgb(flashedColor.A * pixelColor.A / 255, flashedColor);
    return flashedColor;
};
```
You will find alot of useful methods in `Storm.Shaders.ShaderHelpers`, such as `Mix` (shown in the previous example), `Fract`, `InvertColor`, `Grayscale`, `Step` and finally `SmoothStep`.

All you need to use this shader is passing it as the second argument of the `Draw` method of your `Sprite2D`! Example:
```csharp
using Storm.Shaders.BuiltInShaders;

public class Player : GameObject
{
    // Declare the shader
    private static readonly PixelShaderDelegate UVColorShader = (/* ... */) => {/* ... */};

    /* ... */

    public override void OnDraw(Graphics graphics)
    {
        // Pass the shader as an argument
        sprite.Draw(graphics, UVColorShader);
    }
}
```
And for shaders with arguments, just pass the arguments as the third argument!
```csharp
using Storm.Shaders.BuiltInShaders;

public class Player : GameObject
{
    // Declare the argument type
    private record FlashShaderArgs {/* ... */}

    // Declare the shader
    private static readonly PixelShaderDelegate<FlashShaderArgs> FlashShader = (/* ... */) => {/* ... */};

    /* ... */

    public override void OnDraw(Graphics graphics)
    {
        // Pass the shader as an argument
        sprite.Draw(graphics, FlashShader, new FlashShaderArgs() {
                /* ... */
            });
    }
}
```
That's it! Also, those example shaders are from the `Storm.Shaders.BasicShaders` static class! All of those shaders are there, so if you need them, no need to rewrite them!
