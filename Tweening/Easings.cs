namespace Storm.Tweening;

#nullable disable
public static class Easings
{
    public readonly static Func<float, float> Linear = t => t;

    public readonly static Func<float, float> EaseInQuad = t => t * t;
    public readonly static Func<float, float> EaseOutQuad = t => 1 - (1 - t) * (1 - t);
    public readonly static Func<float, float> EaseInOutQuad = t => t < 0.5f ? 2 * t * t : 1 - MathF.Pow(-2 * t + 2, 2) / 2;
    public readonly static Func<float, float> EaseOutInQuad = t => t < 0.5f ? (1 - EaseInQuad(1 - 2 * t)) / 2 : (EaseOutQuad(2 * t - 1) + 1) / 2;

    public readonly static Func<float, float> EaseInCubic = t => t * t * t;
    public readonly static Func<float, float> EaseOutCubic = t => 1 - MathF.Pow(1 - t, 3);
    public readonly static Func<float, float> EaseInOutCubic = t => t < 0.5f ? 4 * t * t * t : 1 - MathF.Pow(-2 * t + 2, 3) / 2;
    public readonly static Func<float, float> EaseOutInCubic = t => t < 0.5f ? (1 - EaseInCubic(1 - 2 * t)) / 2 : (EaseOutCubic(2 * t  - 1) + 1) / 2;

    public readonly static Func<float, float> EaseInQuart = t => t * t * t * t;
    public readonly static Func<float, float> EaseOutQuart = t => 1 - MathF.Pow(1 - t, 4);
    public readonly static Func<float, float> EaseInOutQuart = t => t < 0.5f ? 8 * t * t * t * t : 1 - MathF.Pow(-2 * t + 2, 4) / 2;
    public readonly static Func<float, float> EaseOutInQuart = t => t < 0.5f ? (1 - EaseInQuart(1 - 2 * t)) / 2 : (EaseOutQuart(2 * t - 1) + 1) / 2;

    public readonly static Func<float, float> EaseInQuint = t => t * t * t * t * t;
    public readonly static Func<float, float> EaseOutQuint = t => 1 - MathF.Pow(1 - t, 5);
    public readonly static Func<float, float> EaseInOutQuint = t => t < 0.5f ? 16 * t * t * t * t * t : 1 - MathF.Pow(-2 * t + 2, 5) / 2;
    public readonly static Func<float, float> EaseOutInQuint = t => t < 0.5f ? (1 - EaseInQuint(1 - 2 * t)) / 2 : (EaseOutQuint(2 * t - 1) + 1) / 2;

    public readonly static Func<float, float> EaseInSine = t => 1 - MathF.Cos(t * MathF.PI / 2);
    public readonly static Func<float, float> EaseOutSine = t => MathF.Sin(t * MathF.PI / 2);
    public readonly static Func<float, float> EaseInOutSine = t => -(MathF.Cos(MathF.PI * t) - 1) / 2;
    public readonly static Func<float, float> EaseOutInSine = t => t < 0.5f ? (EaseOutSine(2 * t) + 1) / 2 : (1 - EaseInSine(2 - 2 * t)) / 2;

    public readonly static Func<float, float> EaseInExpo = t => t == 0 ? 0 : MathF.Pow(2, 10 * (t - 1));
    public readonly static Func<float, float> EaseOutExpo = t => t == 1 ? 1 : 1 - MathF.Pow(2, -10 * t);
    public readonly static Func<float, float> EaseInOutExpo = t => t == 0 ? 0 : t == 1 ? 1 : t < 0.5 ? MathF.Pow(2, 20 * t - 10) / 2 : (2 - MathF.Pow(2, -20 * t + 10)) / 2;
    public readonly static Func<float, float> EaseOutInExpo = t => t < 0.5f ? (EaseOutExpo(2 * t) + 1) / 2 : (1 - EaseInExpo(2 - 2 * t)) / 2;

    public readonly static Func<float, float> EaseInCirc = t => 1 - MathF.Sqrt(1 - MathF.Pow(t, 2));
    public readonly static Func<float, float> EaseOutCirc = t => MathF.Sqrt(1 - MathF.Pow(t - 1, 2));
    public readonly static Func<float, float> EaseInOutCirc = t => t < 0.5f ? (1 - MathF.Sqrt(1 - MathF.Pow(2 * t, 2))) / 2 : (MathF.Sqrt(1 - MathF.Pow(-2 * t + 2, 2)) + 1) / 2;
    public readonly static Func<float, float> EaseOutInCirc = t => t < 0.5f ? (EaseOutCirc(2 * t) + 1) / 2 : 1 - EaseInCirc(2 - 2 * t) / 2;

    public readonly static Func<float, float> EaseInElastic = t => t == 0 ? 0 : t == 1 ? 1 : -MathF.Pow(2, 10 * (t - 1)) * MathF.Sin((t - 1.1f) * 5 * MathF.PI);
    public readonly static Func<float, float> EaseOutElastic = t => t == 0 ? 0 : t == 1 ? 1 : MathF.Pow(2, -10 * t) * MathF.Sin((t - 0.1f) * 5 * MathF.PI) + 1;
    public readonly static Func<float, float> EaseInOutElastic = t => t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? -(MathF.Pow(2, 20 * t - 10) * MathF.Sin((20 * t - 11.125f) * MathF.PI * 2 / 4.5f)) / 2 : ((MathF.Pow(2, -20 * t + 10) * MathF.Sin((20 * t - 11.125f) * MathF.PI * 2 / 4.5f)) / 2 + 1);
    public readonly static Func<float, float> EaseOutInElastic = t => t < 0.5f ? (EaseOutElastic(2 * t) + 1) / 2 : (1 - EaseInElastic(2 - 2 * t)) / 2;

    public readonly static Func<float, float> EaseInBack = t => t * t * ((1.70158f + 1) * t - 1.70158f);
    public readonly static Func<float, float> EaseOutBack = t => 1 - ((1 - t) * (1 - t) * ((1.70158f + 1) * (1 - t) - 1.70158f));
    public readonly static Func<float, float> EaseInOutBack = t => t < 0.5f ? MathF.Pow(2 * t, 2) * (((1.70158f * 1.525f) + 1) * 2 * t - (1.70158f * 1.525f)) / 2 : MathF.Pow(2 * t - 2, 2) * (((1.70158f * 1.525f) + 1) * (t * 2 - 2) + (1.70158f * 1.525f)) + 2 / 2;
    public readonly static Func<float, float> EaseOutInBack = t => t < 0.5f ? (EaseOutBack(2 * t) + 1) / 2 : (1 - EaseInBack(2 - 2 * t)) / 2;

    public readonly static Func<float, float> EaseInBounce = t => 1 - EaseOutBounce(1 - t);
    public readonly static Func<float, float> EaseOutBounce = t =>
    {
        if (t < 1 / 2.75f)
        {
            return 7.5625f * t * t;
        }
        else if (t < 2 / 2.75f)
        {
            t -= 1.5f / 2.75f;
            return 7.5625f * t * t + 0.75f;
        }
        else if (t < 2.5f / 2.75f)
        {
            t -= 2.25f / 2.75f;
            return 7.5625f * t * t + 0.9375f;
        }

        t -= 2.625f / 2.75f;
        return 7.5625f * t * t + 0.984375f;
    };
    public readonly static Func<float, float> EaseInOutBounce = t => t < 0.5 ? (1 - EaseOutBounce(1 - 2 * t)) / 2 : (1 + EaseOutBounce(2 * t - 1)) / 2;
    public readonly static Func<float, float> EaseOutInBounce = t => t < 0.5 ? (EaseOutBounce(2 * t) + 1) / 2 : (1 - EaseInBounce(2 - 2 * t)) / 2;


    public static Func<float, float> CreateEasing(float p1x, float p1y, float p2x, float p2y)
    {
        return t =>
        {
            t = Math.Clamp(t, 0f, 1f);

            float cx = 3 * p1x;
            float bx = 3 * (p2x - p1x) - cx;
            float ax = 1 - cx - bx;
            float cy = 3 * p1y;
            float by = 3 * (p2y - p1y) - cy;
            float ay = 1 - cy - by;

            float result = (ax * t * t * t) + (bx * t * t) + (cx * t);

            return (ay * result * result * result) + (by * result * result) + (cy * result);
        };
    }
}
