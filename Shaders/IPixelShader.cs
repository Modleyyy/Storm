namespace Storm.Shaders;

using System.Drawing;
using System.Drawing.Imaging;

public interface IPixelShader
{
    public Bitmap ShadeImage(Bitmap imageToShade)
    {
        int width = imageToShade.Width;
        int height = imageToShade.Height;
        Vector2 texSize = new(width, height);
        
        Rectangle rect = new(0, 0, width, height);
        Bitmap shadedImage = new(imageToShade);
        BitmapData bmpData = shadedImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

        IntPtr ptr = bmpData.Scan0;
        int bytesPerPixel = 4;
        int stride = bmpData.Stride;

        unsafe
        {
            byte* pixelPtr = (byte*)ptr;

            Parallel.For(0, height, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, y =>
            {
                byte* row = pixelPtr + (y * stride);

                Vector2 coords = new() { y = y };
                Vector2 uv = new() { y = coords.y / texSize.y };

                Parallel.For(0, width, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, x =>
                {
                    int offset = x * bytesPerPixel;

                    byte blue = row[offset];
                    byte green = row[offset + 1];
                    byte red = row[offset + 2];
                    byte alpha = row[offset + 3];

                    Color pixelColor = Color.FromArgb(alpha, red, green, blue);
                    coords.x = x;
                    uv.x = coords.x / texSize.x;

                    Color shadedColor = ShaderCode(pixelColor, uv, coords, texSize);

                    row[offset] = shadedColor.B;
                    row[offset + 1] = shadedColor.G;
                    row[offset + 2] = shadedColor.R;
                    row[offset + 3] = shadedColor.A;
                });
            });
        }

        shadedImage.UnlockBits(bmpData);

        return shadedImage;
    }


    protected abstract Color ShaderCode(Color pixelColor, Vector2 uv, Vector2 coords, Vector2 texSize);
}