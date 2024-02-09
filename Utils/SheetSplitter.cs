namespace Storm.Utils;

public static class SheetSplitter
{
    public static List<List<Bitmap>> SplitSpritesheet(string path, int rows, int columns, bool poolSheet = true)
    {
        List<List<Bitmap>> frames = new(rows);
        Bitmap spritesheet = SpriteLoader.GetSprite(path, poolSheet);

        int frameWidth = spritesheet.Width / columns;
        int frameHeight = spritesheet.Height / rows;

        for (int y = 0; y < rows; y++)
        {
            List<Bitmap> row = new(columns);
            for (int x = 0; x < columns; x++)
            {
                Rectangle frameRect = new(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                row.Add(spritesheet.Clone(frameRect, spritesheet.PixelFormat));
            }
            frames.Add(row);
        }

        return frames;
    }
}