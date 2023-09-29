namespace Storm.Animation;

using Components;
using Signals;

public class AnimatedSprite2D : Sprite2D
{
    public new bool isActive = true;

    public Signal animationStarted = new();
    public Signal<AnimationEndedDelegate> animationFinished = new();
    public Signal<FrameChangedDelegate> frameChanged = new();

    private readonly Dictionary<string, Animation> animations;
    private string _cur = "";
    public string currentAnimation {
        set {
            _cur = value;
            interval = Game.FPS / animations[value].FPS;
        }
        get => _cur;
    }
    public int currentFrame;
    private int frameTimer;
    private int interval;

    public AnimatedSprite2D(string path, Dictionary<string, Animation> animations, Vector2? offset = null, bool centered = false)
            : base(path, offset, centered)
    {
        this.animations = animations;
        _cur = "";
        currentFrame = 0;
        frameTimer = 0;

        PlayAnimation(animations.Keys.ToArray<string>()[0]);
    }

    public void PlayAnimation(string animationName, int from = 0)
    {
        if (_cur != animationName)
        {
            _cur = animationName;
            animationStarted.Emit();
            currentFrame = from;
            frameTimer = 0;
        }
    }
    public override void OnUpdate(double deltaTime)
    {
        if (_cur != "" && animations!.ContainsKey(_cur))
        {
            Animation animation = animations[_cur];
            frameTimer++;
            if (frameTimer >= interval)
            {
                frameTimer = 0;
                frameChanged.Emit(currentFrame, currentFrame++);
                currentFrame++;
                if (currentFrame >= animation.numFrames)
                {
                    if (animation.loop)
                    {
                        animationFinished.Emit(true);
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = animation.numFrames;
                        animationFinished.Emit(false);
                    }
                }
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