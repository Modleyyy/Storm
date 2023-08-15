namespace Storm.Tweening;

using Storm.Components;

public class Tween : Component
{
    private readonly List<TweenerBase> tweeners = new();

    public override void OnUpdate(double deltaTime)
    {
        Parallel.For(0, tweeners.Count, i =>
        {
            TweenerBase tweener = tweeners[i];
            double t = tweener.Update(deltaTime);
            if (t >= 1f)
            {
                tweener.isFinished = true;
                tweener.onComplete();
            }
        });
        tweeners.RemoveAll( t => t.isFinished );
    }

    public void TweenValue<T>(T startValue, T endValue, float duration, Func<float, float> easingFunction,
        Action<T> valueChanged, Action onComplete)
    {
        Tweener<T> tweener = new(startValue, endValue, duration, easingFunction, valueChanged, onComplete);
        tweeners.Add(tweener);
    }
}
