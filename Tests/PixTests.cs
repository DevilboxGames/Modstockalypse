using System.Drawing.Imaging;
using System.Drawing;
using ToxicRagers.Brender.Formats;

namespace Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("AUTO3HEAD")]
        [InlineData("BBFRAME")]
        public void PixCreateTests(string imageName)
        {
            PIX originalPix = PIX.Load($"Data\\{imageName}.pix");
            Bitmap originalBitmap = originalPix.Pixies[0].GetBitmap();
            Bitmap bmp = new Bitmap($"Data\\{imageName}.tif");

            PIXIE pixie = PIXIE.FromBitmap(
                bmp.PixelFormat == PixelFormat.Format32bppArgb
                    ? PIXIE.PixelmapFormat.C2_16bitAlpha
                    : PIXIE.PixelmapFormat.C2_16bit, bmp);
            pixie.Name = Path.GetFileNameWithoutExtension(imageName);
            PIX newPix = new PIX()
            {
                Pixies =
                {
                    pixie
                }
            };

            Directory.CreateDirectory("Output");

            originalPix.Save($"Output\\{imageName}_orig.pix");
            newPix.Save($"Output\\{imageName}_created.pix");

            PIX savedOrigPix = PIX.Load($"Output\\{imageName}_orig.pix");
            PIX savedNewPix = PIX.Load($"Output\\{imageName}_created.pix");

            Bitmap savedOrigPixBmp = savedOrigPix.Pixies[0].GetBitmap();
            Bitmap savedNewPixBmp = savedNewPix.Pixies[0].GetBitmap();

            savedOrigPixBmp.Save($"Output\\{imageName}_orig.tif");
            savedNewPixBmp.Save($"Output\\{imageName}_created.tif");

            Assert.Equivalent(originalPix.Pixies[0].Data, newPix.Pixies[0].Data);
        }
    }
}