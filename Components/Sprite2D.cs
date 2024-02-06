namespace Storm.Components;

using Shaders;
using Utils;

using System.Drawing;
using System.Drawing.Drawing2D;

public class Sprite2D : Component
{
    private readonly static Matrix emptyMatrix = new();
    public new bool isActive = false;
    
    protected Bitmap sprite;
    public readonly string path;
    public Vector2 offset = Vector2.Zero;
    public bool centered = false;

    public bool flippedH = false;
    public bool flippedV = false;


    public Sprite2D(string path, Vector2? offset = null, bool centered = false, bool pooled = true)
    {
        this.path = path;
        this.offset = offset ?? new Vector2();
        this.centered = centered;
        sprite = SpriteLoader.GetSprite(path, pooled);
    }

    protected virtual Bitmap GetSprite()
    {
        return sprite;
    }

    public void Draw(Graphics graphics, PixelShaderDelegate? shader = null)
    {
        if (shader is not null)
            Draw<byte>(graphics, (Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize, byte args) => shader(pixelColor, uv, coords, texSize), 0);
        else
        {
            Bitmap spr = GetSprite();

            Vector2 pos = boundObject.transform.position + offset;
            Vector2 size = new(spr.Width * boundObject.transform.width, spr.Height * boundObject.transform.height);
            if (flippedH) size.x *= -1;
            if (flippedV) size.y *= -1;

            if (centered) pos -= size * 0.5f;

            if (boundObject.tint != Color.White)
            {
                BasicShaders.TintShaderArgs targs = new() { tint = boundObject.tint, };
                spr = Shader.ShadeImage(spr, BasicShaders.Tint, targs);
            }
        
            float centerX = pos.x + size.x * 0.5f;
            float centerY = pos.y + size.y * 0.5f;

            if (boundObject.transform.rotation == 0)
            {
                graphics.DrawImage(spr, new RectangleF(pos.x, pos.y, size.x, size.y));
            }
            else
            {
                using (Matrix transformMatrix = new())
                {
                    transformMatrix.RotateAt(boundObject.transform.rotation, new PointF(centerX, centerY));
                    graphics.Transform = transformMatrix;

                    graphics.DrawImage(spr, pos.x, pos.y, size.x, size.y);
                }

                graphics.Transform = emptyMatrix;
                if (!ReferenceEquals(spr, spr)) spr.Dispose();
            }
        }
    }

    public void Draw<TArgs>(Graphics graphics, PixelShaderDelegate<TArgs> shader, TArgs args)
    {
        Bitmap spr = GetSprite();

        Vector2 pos = new Vector2(boundObject.transform.xPos, boundObject.transform.yPos) + offset;
        Vector2 size = new(spr.Width * boundObject.transform.width, spr.Height * boundObject.transform.height);
        if (flippedH) size.x *= -1;
        if (flippedV) size.y *= -1;

        if (centered) pos -= size * 0.5f;

        Bitmap shadedSpr = Shader.ShadeImage(spr, shader, args);
        if (boundObject.tint != Color.White)
        {
            BasicShaders.TintShaderArgs targs = new() { tint = boundObject.tint, };
            shadedSpr = Shader.ShadeImage(shadedSpr, BasicShaders.Tint, targs);
        }
    
        float centerX = pos.x + size.x * 0.5f;
        float centerY = pos.y + size.y * 0.5f;
    
        if (boundObject.transform.rotation == 0)
        {
            graphics.DrawImage(shadedSpr, new RectangleF(pos.x, pos.y, size.x, size.y));
        }
        else
        {
            using (Matrix transformMatrix = new())
            {
                transformMatrix.RotateAt(boundObject.transform.rotation, new PointF(centerX, centerY));
                graphics.Transform = transformMatrix;
    
                graphics.DrawImage(shadedSpr, new RectangleF(pos.x, pos.y, size.x, size.y));
            }

            graphics.Transform = emptyMatrix;
            shadedSpr.Dispose();
        }
    }

    public void FlipH() => flippedH = !flippedH;

    public void FlipV() => flippedV = !flippedV;
}