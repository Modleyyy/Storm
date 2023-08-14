namespace Storm.Animation;

using Components;

public class AnimatedSprite2D : Sprite2D
{
    public new bool isActive = true;

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

    public void PlayAnimation(string animationName)
    {
        if (currentAnimation != animationName)
        {
            currentAnimation = animationName;
            currentFrame = 0;
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
                currentFrame++;
                if (currentFrame >= animation.numFrames)
                {
                    if (animation.loop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = animation.numFrames;
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
}