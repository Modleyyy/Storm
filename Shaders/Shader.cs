using System.Drawing.Imaging;

namespace Storm.Shaders;

public delegate Color PixelShaderDelegate<TArgs>(Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture, TArgs args);
public delegate Color PixelShaderDelegate(Color pixelColor, Vector2 uv, Vector2 coords, Bitmap texture);

public static class Shader
{
    public static Bitmap ShadeImage(Bitmap imageToShade, PixelShaderDelegate shader)
    {
        return ShadeImage<byte>(imageToShade, (pixelColor, uv, coords, texSize, args) => shader(pixelColor, uv, coords, texSize), 0);
    }

    public static Bitmap ShadeImage<TArgs>(Bitmap imageToShade, PixelShaderDelegate<TArgs> shader, TArgs args)
    {
        int width = imageToShade.Width;
        int height = imageToShade.Height;
        
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
                Vector2 uv = new() { y = coords.y / height };

                Parallel.For(0, width, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, x =>
                {
                    int offset = x * bytesPerPixel;

                    byte blue = row[offset];
                    byte green = row[offset + 1];
                    byte red = row[offset + 2];
                    byte alpha = row[offset + 3];

                    Color pixelColor = Color.FromArgb(alpha, red, green, blue);
                    coords.x = x;
                    uv.x = coords.x / width;

                    Color shadedColor = shader(pixelColor, uv, coords, imageToShade, args);

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
}
