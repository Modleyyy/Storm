namespace Storm.Animation;

using Components;
using Signals;

public class AnimatedSprite2D : Sprite2D
{
    public new bool isActive = true;

    public Signal animationStarted = new();
    public Signal<AnimationDelegate> animationFinished = new();
    public Signal<FrameChangedDelegate> frameChanged = new();

    private readonly Dictionary<string, Animation> animations;
    public string currentAnimation;
    public int currentFrame;
    private int frameTimer;

    public AnimatedSprite2D(string path, Dictionary<string, Animation> animations, Vector2? offset = null, bool centered = false)
            : base(path, offset, centered)
    {
        this.animations = animations;
        currentAnimation = "";
        currentFrame = 0;
        frameTimer = 0;

        PlayAnimation(animations.Keys.ToArray<string>()[0]);
    }

    public void PlayAnimation(string animationName, int from = 0)
    {
        if (currentAnimation != animationName)
        {
            currentAnimation = animationName;
            animationStarted.Emit();
            currentFrame = from;
            frameTimer = 0;
        }
    }
    public override void OnUpdate(double deltaTime)
    {
        if (currentAnimation != "" && animations!.ContainsKey(currentAnimation))
        {
            Animation animation = animations[currentAnimation];
            frameTimer++;
            if (frameTimer >= Game.FPS / animation.FPS)
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
        if (currentAnimation != "" && animations.ContainsKey(currentAnimation))
        {
            Animation animation = animations[currentAnimation];
            if (currentFrame >= 0 && currentFrame < animation.numFrames)
            {
                return animation.frames[currentFrame];
            }
        }
        return sprite;
    }

    public delegate void AnimationDelegate(bool looping);
    public delegate void FrameChangedDelegate(int currentFrame, int lastFrame);
}