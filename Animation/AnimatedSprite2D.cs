namespace Storm.Animation;

using Components;
using Signals;

using AnimationCollection = Dictionary<string, Animation>;

public class AnimatedSprite2D : Sprite2D
{
    public new bool isActive = true;

    public readonly Signal animationStarted = new();
    public readonly Signal<AnimationEndedDelegate> animationFinished = new();
    public readonly Signal<FrameChangedDelegate> frameChanged = new();

    private readonly AnimationCollection animations;
    private string _cur = "";
    public string currentAnimation {
        set {
            _cur = value;
            interval = Game.FPS / animations[value].FPS;
        }
        get => _cur;
    }
    public int currentFrame;
    private float frameTimer;
    private float interval;

    public AnimatedSprite2D(string path, AnimationCollection animations, Vector2? offset = null, bool centered = false, bool pooled = true)
            : base(path, offset, centered, pooled)
    {
        this.animations = new(animations);
        _cur = "";
        currentFrame = 0;
        frameTimer = 0;

        PlayAnimation(animations.Keys.ElementAt(0));
    }

    public void PlayAnimation(string animationName, int from = 0)
    {
        if (_cur != animationName)
        {
            interval = animations[animationName].frames.Count == 1 || animations[animationName].FPS == animations[_cur].FPS
                ? interval : Game.FPS / animations[animationName].FPS;
            _cur = animationName;
            animationStarted.Emit();
            currentFrame = from;
            frameTimer = 0;
        }
    }
    public void AddAnimation(string name, Animation animation) => animations.Add(name, animation);
    public bool HasAnimation(string animationName) => animations.ContainsKey(animationName);
    public override void OnUpdate(double deltaTime)
    {
        if (_cur != "" && animations!.ContainsKey(_cur))
        {
            if (frameTimer == interval)
            {
                frameTimer = 0;
                int lastFrame = currentFrame;
                currentFrame++;
                Animation animation = animations[_cur];

                if (currentFrame >= animation.numFrames)
                {
                    if (animation.loop)
                    {
                        animationFinished.Emit(true);
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = animation.numFrames - 1;
                        animationFinished.Emit(false);
                    }
                }
                frameChanged.Emit(currentFrame, lastFrame);
            }
        }
    }

    protected override Bitmap GetSprite()
    {
        if (_cur != "" && animations.ContainsKey(_cur))
        {
            Animation animation = animations[_cur];
            if (currentFrame >= 0 && currentFrame < animation.numFrames)
            {
                return animation.frames[currentFrame];
            }
        }
        return sprite;
    }

    public delegate void AnimationEndedDelegate(bool looping);
    public delegate void FrameChangedDelegate(int currentFrame, int lastFrame);
}