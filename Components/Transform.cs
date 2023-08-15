namespace Storm.Components;

public class Transform : Component
{
    public new bool isActive = false;
    private Vector2 _position = new();
    private Vector2 _scale = new();
    private float _xPos;
    private float _yPos;
    private float _width;
    private float _height;

    public Vector2 position
    {
        set { _position = value; _xPos = value.x; _yPos = value.y; }
        get { return _position; }
    }

    public Vector2 scale
    {
        set { _scale = value; _width = value.x; _height = value.y; }
        get { return _scale; }
    }

    public float xPos
    {
        set { _xPos = value; _position.x = value; }
        get { return _xPos; }
    }

    public float yPos
    {
        set { _yPos = value; _position.y = value; }
        get { return _yPos; }
    }

    public float width
    {
        set { _width = value; _scale.x = value; }
        get { return _width; }
    }

    public float height
    {
        set { _height = value; _scale.y = value; }
        get { return _height; }
    }

    public float rotation = 0f;

    public Transform()
    {
        this.position = Vector2.Zero;
        this.scale = Vector2.One;
        this.rotation = 0f;
    }
    
    public Transform(Vector2 position) 
    {
        this.position = position;
        this.scale = Vector2.One;
        this.rotation = 0f;
    }
    
    public Transform(Vector2 position, Vector2 scale, float? rotation = null)
    {
        this.position = position;
        this.scale = scale;
        this.rotation = rotation ?? 0;
    }
    
    public Transform(float? xPos = null, float? yPos = null, float? width = null, float? height = null, float? rotation = null)
    {
        this.xPos     = xPos     ?? 0;
        this.yPos     = yPos     ?? 0;
        this.width    = width    ?? 1;
        this.height   = height   ?? 1;
        this.rotation = rotation ?? 0;
    }

    public override string ToString()
    {
        return $"X : {_xPos}, Y : {_yPos}, Width : {_width}, Height : {_height}";
    }
}
