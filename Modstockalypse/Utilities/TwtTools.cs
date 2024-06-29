using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToxicRagers.Brender.Formats;
using ToxicRagers.Carmageddon2.Formats;

namespace Modstockalypse.Utilities
{
    public static class TwtTools
    {

        public static void ExtractTwtFiles(string path)
        {
            DirectoryInfo here = new DirectoryInfo(path);
            foreach (FileInfo fi in here.GetFiles("*.TWaT", SearchOption.AllDirectories))
            {
                if (!File.Exists(fi.FullName.Replace(fi.Extension, ".twt")))
                {
                    fi.MoveTo(fi.FullName.Replace(fi.Extension, ".twt"));
                }
            }
            foreach (FileInfo fi in here.GetFiles("*.twt", SearchOption.AllDirectories))
            {
                using (FileStream stream = fi.OpenRead())
                {
                    TWT twt = TWT.Load(stream);
                    twt.Name = Path.GetFileNameWithoutExtension(fi.Name);
                    twt.Location = fi.DirectoryName;
                    twt.ExtractAll();
                }

                if (!File.Exists(fi.FullName.Replace(fi.Extension, ".TWaT")))
                {
                    fi.MoveTo(fi.FullName.Replace(fi.Extension, ".TWaT"));
                }
            }
            foreach (FileInfo fi in here.GetFiles("pixies.p*", SearchOption.AllDirectories))
            {
                string dest = fi.DirectoryName + (fi.Extension.EndsWith("08") ? "\\PIX08\\" : "\\PIX16\\");

                PIX pix = PIX.Load(fi.FullName);
                pix.ExtractPixies(dest);
                fi.Delete();
            }
        }

        public static void PackTwtFiles(string path)
        {
            string target = path;
            string savePath = Directory.GetParent(target).FullName;

            if (Directory.Exists(Path.Combine(target, "tiffx")) || Directory.Exists(Path.Combine(target, "tiffrgb")))
            {
                PixTools.CreatePixFiles(target);
            }

            if (Directory.Exists(Path.Combine(target, "pix08")) || Directory.Exists(Path.Combine(target, "pix16")))
            {
                PixTools.PackPixCollections(target);
            }

            TWT twt = TWT.Create(target, savePath, true);
        }

    }
}
