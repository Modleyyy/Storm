namespace Storm.Components;

using System.Media;

public class AudioPlayer : Component
{
    readonly SoundPlayer sp;

    public AudioPlayer(string audioPath)
    {
        this.sp = new(Path.Combine(Game.GetRootFolderPath(), audioPath));
        this.sp.LoadAsync();
    }

    public void Play()
    {
        this.sp.Play();
    }

    public void Stop()
    {
        this.sp.Stop();
    }
}
