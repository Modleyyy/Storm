namespace Storm.Physics;

using Components;

#nullable disable
public abstract class PhysicsBody : Component
{
    public new bool isActive = false;
    public Vector2 velocity = new();
    public Vector2 offset = new();
    public Vector2 center => boundObject.transform.position + offset;
    public bool isStatic = false;
    public PhysicsWorld world;

    public abstract void Draw(Graphics graphics, Color color, DrawMode drawMode = DrawMode.Border);

    public bool IsOnFloor()
    {
        if (world is not null) return world.IsOnFloor(this);
        Logging.Log.Warning("The body is in no PhysicsWorld, it must return false.");
        return false;
    }

    public bool IsOnWall()
    {
        if (world is not null) return world.IsOnWall(this);
        Logging.Log.Warning("The body is in no PhysicsWorld, it must return false.");
        return false;
    }

    public bool IsOnCeiling()
    {
        if (world is not null) return world.IsOnCeiling(this);
        Logging.Log.Warning("The body is in no PhysicsWorld, it must return false.");
        return false;
    }

    public enum DrawMode
    {
        Border = 0,
        Fill = 1
    }
}
