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

    public void Draw(Graphics graphics, IPixelShader? shader = null)
    {
        Bitmap spr = GetSprite();

        Vector2 pos = new(boundObject.transform.xPos, boundObject.transform.yPos);
        pos += offset;
        Vector2 size = new(spr.Width * boundObject.transform.width, spr.Height * boundObject.transform.height);
        if (flippedH) size.x *= -1;
        if (flippedV) size.y *= -1;

        if (centered) pos -= size / 2;

        if (shader is not null)
        {
            Bitmap shadedSpr = shader!.ShadeImage(spr);
            if (boundObject.tint != Color.White)
            {
                IPixelShader t = new TintShader()
                {
                    tint = boundObject.tint
                };
                shadedSpr = t.ShadeImage(shadedSpr);
            }
    
            float centerX = pos.x + size.x / 2;
            float centerY = pos.y + size.y / 2;
        
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
        else
        {
            Bitmap tintedSpr = spr;
            if (boundObject.tint != Color.White)
            {
                IPixelShader t = new TintShader()
                {
                    tint = boundObject.tint
                };
                tintedSpr = t.ShadeImage(spr);
            }
        
            float centerX = pos.x + size.x / 2;
            float centerY = pos.y + size.y / 2;

            if (boundObject.transform.rotation == 0)
            {
                graphics.DrawImage(tintedSpr, new RectangleF(pos.x, pos.y, size.x, size.y));
            }
            else
            {
                using (Matrix transformMatrix = new())
                {
                    transformMatrix.RotateAt(boundObject.transform.rotation, new PointF(centerX, centerY));
                    graphics.Transform = transformMatrix;

                    graphics.DrawImage(tintedSpr, pos.x, pos.y, size.x, size.y);
                }

                graphics.Transform = emptyMatrix;
                if (!ReferenceEquals(spr, tintedSpr)) tintedSpr.Dispose();
            }
        }
    }

    public void FlipH() => flippedH = !flippedH;

    public void FlipV() => flippedV = !flippedV;

    private struct TintShader : IPixelShader
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
}