using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToxicRagers.Brender.Formats;
using ToxicRagers.Carmageddon2.Formats;

namespace Modstockalypse.Utilities
{
    internal static class PixTools
    {
        public static void ExtractPixFiles(string path)
        {
            DirectoryInfo here = new DirectoryInfo(path);
            foreach (FileInfo fi in here.GetFiles("*.twt", SearchOption.AllDirectories))
            {
                TWT twt = TWT.Load(fi.FullName);
                twt.Name = Path.GetFileNameWithoutExtension(fi.Name);
                twt.Location = fi.DirectoryName;
                foreach (TWTEntry entry in twt.Contents.Where(entry =>
                             entry.Name.EndsWith("p08", true, CultureInfo.InvariantCulture) ||
                             entry.Name.EndsWith("p16", true, CultureInfo.InvariantCulture)))
                {
                    using (MemoryStream stream = new MemoryStream(entry.Data))
                    {
                        PIX pix = PIX.Load(stream);

                        string dest = Path.Combine(fi.Directory.FullName, Path.GetFileNameWithoutExtension(fi.Name), (entry.Name.EndsWith("8") ? "tiffx" : "tiffrgb"));
                        if (!Directory.Exists(dest))
                        {
                            Directory.CreateDirectory(dest);
                        }
                        foreach (var pixie in pix.Pixies)
                        {
                            Bitmap bmp = pixie.GetBitmap();
                            bmp.Save(Path.Combine(dest, $"{pixie.Name}.tif"), ImageFormat.Tiff);
                        }
                    }
                }
            }

            List<string> dirsToDelete = new List<string>();
            foreach (FileInfo fi in here.GetFiles("*.pix", SearchOption.AllDirectories))
            {
                string dest = Path.Combine(fi.Directory.Parent.FullName, (fi.DirectoryName.EndsWith("8") ? "tiffx" : "tiffrgb"));
                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }
                PIX pix = PIX.Load(fi.FullName);
                foreach (var pixie in pix.Pixies)
                {
                    Bitmap bmp = pixie.GetBitmap();
                    string filename = pixie.Name;
                    if (string.IsNullOrWhiteSpace(pixie.Name))
                    {
                        filename = Path.GetFileNameWithoutExtension(fi.Name);
                    }
                    bmp.Save(Path.Combine(dest, $"{filename}.tif"), ImageFormat.Tiff);
                }

                fi.Delete();
                if ((fi.DirectoryName.EndsWith("PIX16") ||
                    fi.DirectoryName.EndsWith("PIX08")) && !dirsToDelete.Contains(fi.DirectoryName))
                {
                    dirsToDelete.Add(fi.DirectoryName);
                }
            }

            foreach (var dirtoDelete in dirsToDelete)
            {
                if (!string.IsNullOrEmpty(dirtoDelete) && Directory.Exists(dirtoDelete) && dirtoDelete.StartsWith(path))
                {
                    Directory.Delete(dirtoDelete);
                }
            }
        }

        public static void CreatePixFiles(string path)
        {
            string target = path;
            string target2 = null;
            string savePath = target;
            string savePath2 = null;
            if (target.EndsWith("tiffrgb") || target.EndsWith("tiffrgb/"))
            {
                savePath = Path.Combine(Directory.GetParent(target).FullName, "PIX16");
            }
            else if (target.EndsWith("tiffx") || target.EndsWith("tiffx/"))
            {
                savePath = Path.Combine(Directory.GetParent(target).FullName, "PIX08");
            }
            else
            {
                string oldtarget = target;
                string tiffrgbDir = Path.Combine(target, "tiffrgb");
                string tiffxDir = Path.Combine(target, "tiffx");
                bool tiffrgbExists = Directory.Exists(tiffrgbDir);
                bool tiffxExists = Directory.Exists(tiffxDir);

                if (tiffxExists || tiffrgbExists)
                {
                    target = tiffrgbExists ? tiffrgbDir : tiffxDir;
                    savePath = tiffrgbExists ? Path.Combine(oldtarget, "PIX16") : Path.Combine(oldtarget, "PIX08");

                    if (tiffxExists && tiffrgbExists)
                    {
                        target2 = tiffrgbDir;
                        savePath2 = Path.Combine(oldtarget, "PIX16");
                    }
                }
            }
        }

        public static void PackPixCollections(string path)
        {

        }
    }
}
