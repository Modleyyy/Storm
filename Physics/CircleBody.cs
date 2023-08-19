namespace Storm.Physics;

public class CircleBody : PhysicsBody
{
    public float radius;

    public CircleBody(float radius)
    {
        this.radius = radius;
    }

    public override void Draw(Graphics graphics, Color color, DrawMode drawMode = DrawMode.Border)
    {
        switch (drawMode)
        {
            case DrawMode.Border:
                graphics.DrawEllipse(new Pen(color), center.x - radius, center.y - radius, radius*2, radius*2);
                break;
            case DrawMode.Fill:
                graphics.FillEllipse(new SolidBrush(color), center.x - radius, center.y - radius, radius*2, radius*2);
                break;
        }
    }
}
