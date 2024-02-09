namespace Storm.Animation;

public struct Animation
{
    public bool loop;
    public int numFrames;
    public int FPS;
    public List<Bitmap> frames;

    public Animation(List<Bitmap> frames, bool loop = true, int FPS = 8)
    {
        this.frames = frames;
        this.loop = loop;
        this.numFrames = frames.Count;
        this.FPS = FPS;
    }
}