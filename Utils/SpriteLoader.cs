namespace Storm.Utils;

public static class SpriteLoader
{
    private static Dictionary<string, Bitmap> spritePool = new Dictionary<string, Bitmap>();

    public static Bitmap GetSprite(string path, bool pooled = true)
    {
        Bitmap sprite;
        if (pooled)
        {
            if (spritePool.ContainsKey(path))
            {
                sprite = spritePool[path];
            }
            else
            {
                Image i = Image.FromFile( Path.Combine( Game.GetRootFolderPath(), path ) );
                sprite = new(i);
                spritePool.Add( path, sprite );
            }
        }
        else
        {
            Image i = Image.FromFile( Path.Combine( Game.GetRootFolderPath(), path ) );
            sprite = new(i);
        }
        return sprite;
    }
}
