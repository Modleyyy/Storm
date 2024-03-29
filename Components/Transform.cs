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

    public Transform(Vector2 position, float scale)
    {
        this.position = position;
        this.scale = new Vector2(scale, scale);
        this.rotation = 0f;
    }
    
    public Transform(Vector2 position, Vector2 scale, float rotation = 0)
    {
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }
    
    public Transform(float xPos = 0, float yPos = 0, float width = 1, float height = 1, float rotation = 0)
    {
        this.xPos     = xPos;
        this.yPos     = yPos;
        this.width    = width;
        this.height   = height;
        this.rotation = rotation;
    }

    public override string ToString()
    {
        return $"X : {_xPos}, Y : {_yPos}, Width : {_width}, Height : {_height}";
    }
}
