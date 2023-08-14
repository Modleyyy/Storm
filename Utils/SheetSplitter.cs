namespace Storm.Utils;

public static class SheetSplitter
{
    public static List<Bitmap> SplitSpritesheet(string path, int rows, int columns, bool poolSheet = true)
    {
        List<Bitmap> frames = new();
        Bitmap spritesheet = SpriteLoader.GetSprite(path, poolSheet);

        int frameWidth = spritesheet.Width / columns;
        int frameHeight = spritesheet.Height / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Rectangle frameRect = new(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                Bitmap frame = spritesheet.Clone(frameRect, spritesheet.PixelFormat);
                frames.Add(frame);
            }
        }

        return frames;
    }
}