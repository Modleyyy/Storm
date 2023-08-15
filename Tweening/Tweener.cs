namespace Storm.Tweening;

public class Tweener<T> : TweenerBase
{
    public T startValue { get; }
    public T endValue { get; }
    public override double duration { get; }
    public override double elapsedTime { get; set; }

    public Func<float, float> easingFunction { get; }
    public override Action onComplete { get; }
    public override bool isFinished { get; set; } = false;
    public Action<T> valueChanged { get; }

    public Tweener(T startValue, T endValue, float duration, Func<float, float>easingFunction,
        Action<T> valueChanged, Action onComplete)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.duration = duration;
        this.easingFunction = easingFunction;
        this.valueChanged = valueChanged;
        this.onComplete = onComplete;
        this.elapsedTime = 0;
    }

    private T Lerp(T start, T end, float t)
    {
        if (typeof(T) == typeof(Color))
        {
            Color startColor = (Color)(object)start!;
            Color endColor = (Color)(object)end!;
            Color interpolatedColor = Color.FromArgb(
                (byte)Math.Round(startColor.A + (endColor.A - startColor.A) * t),
                (byte)Math.Round(startColor.R + (endColor.R - startColor.R) * t),
                (byte)Math.Round(startColor.G + (endColor.G - startColor.G) * t),
                (byte)Math.Round(startColor.B + (endColor.B - startColor.B) * t)
            );
            return (T)(object)interpolatedColor;
        }
        else
        {
            dynamic s = start!;
            dynamic e = end!;
            return (T)(s + (e - s) * t);
        }
    }

    public override double Update(double deltaTime)
    {
        elapsedTime += deltaTime;
        float t = (float)Math.Min(elapsedTime / duration, 1f);
        float easedT = easingFunction(t);
        T value = Lerp(startValue, endValue, easedT);
        valueChanged(value);
        return t;
    }
}

public abstract class TweenerBase
{
    public abstract double elapsedTime { get; set; }
    public abstract double duration { get; }
    public abstract Action onComplete { get; }
    public abstract bool isFinished { get; set; }
    public abstract double Update(double deltaTime);
}
