namespace Storm;

using Utils;

sealed public class Vector2 
{
    public float x = 0f;
    public float y = 0f;

    public readonly static Vector2 Zero  = new( 0f    );
    public readonly static Vector2 One   = new( 1f    );
    public readonly static Vector2 Right = new( 1f, 0f);
    public readonly static Vector2 Left  = new(-1f, 0f);
    public readonly static Vector2 Up    = new( 0f, -1f);
    public readonly static Vector2 Down  = new( 0f, 1f);

    #region Constructors
    public Vector2()
    {
        x = 0f;
        y = 0f;
    }
    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public Vector2(float value)
    {
        this.x = value;
        this.y = value;
    }
    #endregion


    #region Helper Functions
    public float GetLength()
    {
        float length = MathF.Sqrt( (x * x) + (y * y) );
        if (float.IsNaN(length))
        {
            return 0;
        }
        return length;
    }
    
    public float GetLengthSquared()
    {
        return (x * x) + (y * y);
    }

    public Vector2 SetLength(float length)
    {
        float currentLength = GetLength();
        float x = this.x;
        float y = this.y;
        if (currentLength != 0)
        {
            float scale = length / currentLength;
            x *= scale;
            y *= scale;
        }
        return new(x, y);
    }

    public float GetAngle()
    {
        return MathHelper.RadToDeg(MathF.Atan2(y, x));
    }

    public void SetAngle(float angle)
    {
        float length = GetLength();
        float radian = MathHelper.DegToRad(angle);
        float x = length * MathF.Cos(radian);
        float y = length * MathF.Sin(radian);

        this.x = x;
        this.y = y;
    }

    public void Normalize()
    {
        float x = this.x / GetLength();
        float y = this.y / GetLength();
        if (float.IsNaN(x) || float.IsNaN(y))
        {
            this.x = 0;
            this.y = 0;
            return;
        }
        this.x = x;
        this.y = y;
    }

    public Vector2 Normalized()
    {
        float x = this.x / GetLength();
        float y = this.y / GetLength();
        if (float.IsNaN(x) || float.IsNaN(y))
        {
            return new();
        }
        return new(x, y);
    }

    public float Distance(Vector2 to)
    {
        Vector2 d = (this - to).Abs();
        return d.GetLength();
    }

    public float DistanceSquared(Vector2 to)
    {
        Vector2 d = (this - to).Abs();
        return d.GetLengthSquared();
    }

    public Vector2 Abs()
    {
        return new(MathF.Abs(x), MathF.Abs(y));
    }

    public float Dot(Vector2 v)
    {
        return (x * v.x) + (y * v.y);
    }

    public float Cross(Vector2 v)
    {
        return (x * v.y) - (y * v.x);
    }

    public Vector2 Rotated(float angle)
    {
        float radian = MathHelper.DegToRad(angle);
        float cos = MathF.Cos(radian);
        float sin = MathF.Sin(radian);
        float x = this.x * cos - this.y * sin;
        float y = this.x * sin + this.y * cos;
        return new Vector2(x, y);
    }

    public Vector2 Perpendicular()
    {
        return new Vector2(-y, x);
    }
    #endregion


    #region DRY
    public override string ToString()
    {
        return $"( X:{x} , Y:{y} )";
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        throw new NotImplementedException();
    }
    public override int GetHashCode()
    {
        return (x.GetHashCode() + y.GetHashCode()) * new Random().Next();
    }
    #endregion


    #region Operators
    public static Vector2 operator +(Vector2 vec1, Vector2 vec2) {
        float x = vec1.x + vec2.x;
        float y = vec1.y + vec2.y;
        return new Vector2(x, y);
    }
    public static Vector2 operator -(Vector2 vec1, Vector2 vec2) {
        float x = vec1.x - vec2.x;
        float y = vec1.y - vec2.y;
        return new Vector2(x, y);
    }
    public static Vector2 operator *(Vector2 vec1, Vector2 vec2) {
        float x = vec1.x * vec2.x;
        float y = vec1.y * vec2.y;
        return new Vector2(x, y);
    }
    public static Vector2 operator /(Vector2 vec1, Vector2 vec2) {
        float x = vec1.x / vec2.x;
        float y = vec1.y / vec2.y;
        return new Vector2(x, y);
    }
    public static Vector2 operator +(Vector2 vec, float i) {
        float x = vec.x + i;
        float y = vec.y + i;
        return new Vector2(x, y);
    }
    public static Vector2 operator -(Vector2 vec, float i) {
        float x = vec.x - i;
        float y = vec.y - i;
        return new Vector2(x, y);
    }
    public static Vector2 operator *(Vector2 vec, float i) {
        float x = vec.x * i;
        float y = vec.y * i;
        return new Vector2(x, y);
    }
    public static Vector2 operator /(Vector2 vec, float i) {
        float x = vec.x / i;
        float y = vec.y / i;
        return new Vector2(x, y);
    }
    public static bool operator ==(Vector2 vec1, Vector2 vec2) {
        return vec1.x == vec2.x && vec1.y == vec2.y;
    }
    public static bool operator !=(Vector2 vec1, Vector2 vec2) {
        return vec1.x != vec2.x || vec1.y != vec2.y;
    }
    public static bool operator >(Vector2 vec1, Vector2 vec2) {
        return vec1.x > vec2.x && vec1.y > vec2.y;
    }
    public static bool operator <(Vector2 vec1, Vector2 vec2) {
        return vec1.x < vec2.x && vec1.y < vec2.y;
    }
    public static bool operator >=(Vector2 vec1, Vector2 vec2) {
        return vec1.x >= vec2.x && vec1.y >= vec2.y;
    }
    public static bool operator <=(Vector2 vec1, Vector2 vec2) {
        return vec1.x <= vec2.x && vec1.y <= vec2.y;
    }

    public static explicit operator Vector2(Point v)
    {
        return new(v.X, v.Y);
    }
    #endregion
}