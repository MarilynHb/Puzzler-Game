using System.Collections.Immutable;
using SkiaSharp;

namespace Puzzler_Game;

public class ImageManipulator
{
    string BaseDirectory => "C:\\Users\\Marilyn\\source\\repos\\Puzzler Game\\Puzzler Game\\";
    string ImagesFolder => Path.Combine(BaseDirectory, "Resources", "Images");

    #region Divide Image
    public async Task<IDictionary<int, ImageSource>> DivideImage(ImageSource imageSource, int gridSize)
    {
        var dictionary = new Dictionary<int, ImageSource>();
        var image = await ImageSourceToSKBitmap(imageSource);
        var tileWidth = image.Width / gridSize;
        var tileHeight = image.Height / gridSize;

        int count = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                var tile = new SKBitmap(tileWidth, tileHeight);

                using (var canvas = new SKCanvas(tile))
                {
                    var srcRect = new SKRectI(j * tileWidth, i * tileHeight, (j + 1) * tileWidth, (i + 1) * tileHeight);
                    var destRect = new SKRect(0, 0, tileWidth, tileHeight);
                    canvas.DrawBitmap(image, srcRect, destRect);
                }

                dictionary.Add(count++, SKBitmapToImageSource(tile));
            }
        }

        return dictionary;
    }
    #endregion

    #region Bitmap <> ImageSource
    public async Task<SKBitmap> ImageSourceToSKBitmap(ImageSource imageSource)
    {
        StreamImageSource streamImageSource;
        if (imageSource is FileImageSource)
        {
            var fileImageSource = (FileImageSource)imageSource;
            var filePath = Path.Combine(ImagesFolder, fileImageSource.File);
            var fileStream = File.OpenRead(filePath);
            streamImageSource = new StreamImageSource { Stream = token => Task.FromResult<Stream>(fileStream) };
        }
        else
        {
            streamImageSource = (StreamImageSource)imageSource;
        }

        var stream = await streamImageSource.Stream(CancellationToken.None);
        return SKBitmap.Decode(stream);
    }

    public ImageSource SKBitmapToImageSource(SKBitmap bitmap)
    {
        var image = SKImage.FromBitmap(bitmap);
        var data = image.Encode(SKEncodedImageFormat.Png, 100);
        var memoryStream = new MemoryStream();
        data.AsStream().CopyTo(memoryStream);
        memoryStream.Position = 0;
        return new StreamImageSource { Stream = token => Task.FromResult((Stream)memoryStream) };
    }
    #endregion
}
