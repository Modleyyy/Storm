namespace Storm.Physics;

public class RectangleBody : PhysicsBody
{
    public Vector2 halfSize;

    public float left   => center.x - halfSize.x;
    public float right  => center.x + halfSize.x;
    public float top    => center.y - halfSize.y;
    public float bottom => center.y + halfSize.y;

    public RectangleBody(Vector2 halfSize)
    {
        this.halfSize = halfSize;
    }

    public RectangleBody(float halfWidth, float halfHeight)
    {
        this.halfSize = new(halfWidth, halfHeight);
    }

    public override void Draw(Graphics graphics, Color color, DrawMode drawMode = DrawMode.Border)
    {
        switch (drawMode)
        {
            case DrawMode.Border:
                graphics.DrawRectangle(new Pen(color), center.x - halfSize.x, center.y - halfSize.y, halfSize.x*2, halfSize.y*2);
                break;
            case DrawMode.Fill:
                graphics.FillRectangle(new SolidBrush(color), center.x - halfSize.x, center.y - halfSize.y, halfSize.x*2, halfSize.y*2);
                break;
        }
    }
}
