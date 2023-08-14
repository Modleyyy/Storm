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
        if (drawMode == DrawMode.Border)
        {
            graphics.DrawEllipse(new Pen(color), center.x - radius, center.y - radius, radius*2, radius*2);
        }
        else if (drawMode == DrawMode.Fill)
        {
            graphics.FillEllipse(new SolidBrush(color), center.x - radius, center.y - radius, radius*2, radius*2);
        }
    }
}
