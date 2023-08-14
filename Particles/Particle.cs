namespace Storm.Particles;

using Utils;
using Shaders;
using Shaders.BuiltInShaders;

using System.Drawing.Drawing2D;

public class Particle
{
    private readonly static Matrix emptyMatrix = new();
    private readonly static Random r = new();
    private static TintShader t = new();

    private ParticleData data;

    private double lifetimeLeft = 0;
    private float lifetime => (float)(lifetimeLeft/data.lifetime);

    private Vector2 position = new();
    private float scale = 1;
    private float rotation = 0;
    private readonly Vector2 dir;
    private Color color = Color.White;

    public bool isDead = false;

    public Particle(ParticleData data, Vector2 position)
    {
        this.data = data;
        this.position = position;

        float angle = MathHelper.DegToRad(data.angle);
        angle += (r.NextSingle() * 0.75f - 0.375f) * 2.5f * data.angleVariance + (r.NextSingle() - 0.5f) * 0.25f * data.angleVariance;

        dir = new(+MathF.Sin(angle), -MathF.Cos(angle));
    }

    public void Reset(Vector2 center)
    {
        position = center;
        lifetimeLeft = 0;
        isDead = false;
    }

    public void Update(double deltaTime)
    {
        lifetimeLeft += deltaTime;
        isDead = lifetimeLeft >= data.lifetime;

        float dt = (float)deltaTime;

        scale = MathHelper.Lerp(data.scaleStart, data.scaleEnd, lifetime);
        color = MathHelper.Lerp(data.colorStart, data.colorEnd, lifetime);

        position += dir * data.linearVelocity * dt;
        rotation += data.angularVelocity * dt;
    }

    public void Draw(Graphics graphics)
    {
        if (data.texture is null)
        {
            if (rotation == 0)
            {
                graphics.FillRectangle(new SolidBrush(color), new RectangleF(position.x - scale/2, position.y - scale/2, scale, scale));
            }
            else
            {
                float centerX = position.x + scale / 2;
                float centerY = position.y + scale / 2;
                using (Matrix transform = new())
                {
                    transform.RotateAt(rotation, new PointF(centerX, centerY));
                    graphics.Transform = transform;
                    graphics.FillRectangle(new SolidBrush(color), new RectangleF(position.x - scale/2, position.y - scale/2, scale, scale));
                }
                graphics.Transform = new();
            }
        }
        else
        {
            Bitmap spr = data.texture;

            if (color != Color.White)
            {
                t.tint = color;
                IPixelShader s = t;
                spr = s.ShadeImage(spr);
            }

            Vector2 size = new(spr.Width * scale,spr.Height * scale);
            if (rotation == 0)
            {
                graphics.DrawImage(spr, new RectangleF(position.x - size.x/2, position.y - size.y/2, size.x, size.y));
            }
            else
            {
                float centerX = position.x + size.x / 2;
                float centerY = position.y + size.y / 2;
                using (Matrix transform = new())
                {
                    transform.RotateAt(rotation, new PointF(centerX, centerY));
                    graphics.Transform = transform;
                    graphics.DrawImage(spr, new RectangleF(position.x - size.x/2, position.y - size.y/2, size.x, size.y));
                }
                graphics.Transform = emptyMatrix;
            }
        }
    }
}
