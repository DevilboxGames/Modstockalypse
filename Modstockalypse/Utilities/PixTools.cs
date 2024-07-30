using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToxicRagers.Brender.Formats;
using ToxicRagers.Carmageddon2.Formats;
using static unluac.decompile.expression.TableLiteral;

namespace Modstockalypse.Utilities
{
    public static class PixTools
    {
        public static void ExtractPixFiles(string path, Action<DataProcessProgressReport> progressReport, BackgroundWorker worker, DoWorkEventArgs evnt )
        {
            DirectoryInfo here = new DirectoryInfo(path);
            DataProcessProgressReport report = new()
            {
                numItems = 0,
                numItemsDone = 0,
                mainMessage = "Gathering PIX files to extract",

                numSubItems = 0,
                numSubItemsDone = 0,
                subMessage = ""
            };

            progressReport.Invoke(report);
            var twtFiles = here.GetFiles("*.twt", SearchOption.AllDirectories);
            report.numSubItems = twtFiles.Length;
            var twtEntries = twtFiles.SelectMany(fi =>
            {
                report.numSubItemsDone++;
                report.mainMessage = $"Finding PIX in {fi.FullName}";
                progressReport.Invoke(report);
                TWT twt = TWT.Load(fi.FullName);
                twt.Name = Path.GetFileNameWithoutExtension(fi.Name);
                twt.Location = fi.DirectoryName;
                return twt.Contents.Where(entry =>
                    entry.Name.EndsWith("p08", true, CultureInfo.InvariantCulture) ||
                    entry.Name.EndsWith("p16", true, CultureInfo.InvariantCulture)).Select(twtentry => (fi, twtentry));
            });
            var pixFiles = here.GetFiles("*.pix", SearchOption.AllDirectories);

            report.numItems = twtEntries.Count() + pixFiles.Length;

            report.subMessage = "";
            report.numSubItems = 0;
            report.numSubItemsDone = 0;

            foreach ((FileInfo fi, TWTEntry entry) in twtEntries)
            {
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }

                report.mainMessage = $"Extracting {entry.Name} from {fi.FullName}";
                using (MemoryStream stream = new MemoryStream(entry.Data))
                {
                    PIX pix = PIX.Load(stream);
                    report.numSubItems = pix.Pixies.Count;
                    report.numSubItemsDone = 0;
                    string dest = Path.Combine(fi.Directory.FullName, Path.GetFileNameWithoutExtension(fi.Name), (entry.Name.EndsWith("8") ? "tiffx" : "tiffrgb"));
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }
                    foreach (var pixie in pix.Pixies)
                    {
                        report.subMessage = $"Extracting {pixie.Name}";
                        report.numSubItemsDone++;
                        progressReport(report);
                        Bitmap bmp = pixie.GetBitmap();
                        bmp.Save(Path.Combine(dest, $"{pixie.Name}.tif"), ImageFormat.Tiff);
                        if (worker.CancellationPending)
                        {
                            evnt.Cancel = true;
                            return;
                        }
                    }
                }
                report.numItemsDone++;
                progressReport.Invoke(report);
            }

            report.subMessage = "";
            report.numSubItems = 0;
            report.numSubItemsDone = 0;

            List<string> dirsToDelete = new List<string>();
            foreach (FileInfo fi in pixFiles)
            {
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
                report.mainMessage = $"Extracting {fi.FullName}";
                string dest = Path.Combine(fi.Directory.Parent.FullName, (fi.DirectoryName.EndsWith("8") ? "tiffx" : "tiffrgb"));
                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }
                PIX pix = PIX.Load(fi.FullName);
                report.numSubItems = pix.Pixies.Count;
                report.numSubItemsDone = 0;
                foreach (var pixie in pix.Pixies)
                {
                    report.subMessage = $"Extracting {pixie.Name}";
                    report.numSubItemsDone++;
                    progressReport(report);
                    Bitmap bmp = pixie.GetBitmap();
                    string filename = pixie.Name;
                    if (string.IsNullOrWhiteSpace(pixie.Name))
                    {
                        filename = Path.GetFileNameWithoutExtension(fi.Name);
                    }
                    bmp.Save(Path.Combine(dest, $"{filename}.tif"), ImageFormat.Tiff);
                    if (worker.CancellationPending)
                    {
                        evnt.Cancel = true;
                        return;
                    }
                }

                fi.Delete();
                if ((fi.DirectoryName.EndsWith("PIX16") ||
                    fi.DirectoryName.EndsWith("PIX08")) && !dirsToDelete.Contains(fi.DirectoryName))
                {
                    dirsToDelete.Add(fi.DirectoryName);
                }
                report.numItemsDone++;
                progressReport.Invoke(report);
            }
            if (worker.CancellationPending)
            {
                evnt.Cancel = true;
                return;
            }
            report.subMessage = "";
            report.numSubItems = 0;
            report.numSubItemsDone = 0;
            report.mainMessage = "Cleaning up...";
            progressReport.Invoke(report);
            foreach (var dirtoDelete in dirsToDelete)
            {
                if (!string.IsNullOrEmpty(dirtoDelete) && Directory.Exists(dirtoDelete) && dirtoDelete.StartsWith(path))
                {
                    Directory.Delete(dirtoDelete);
                }
            }
            if (worker.CancellationPending)
            {
                evnt.Cancel = true;
                return;
            }
        }

        public static void CreatePixFiles(string path, Action<DataProcessProgressReport> progressReport, BackgroundWorker worker, DoWorkEventArgs evnt, DataProcessProgressReport report = null)
        {
            string target = path;
            string target2 = null;
            string savePath = target;
            string savePath2 = null;
            if (target.EndsWith("tiffrgb", true, CultureInfo.InvariantCulture) || target.EndsWith("tiffrgb/", true, CultureInfo.InvariantCulture))
            {
                savePath = Path.Combine(Directory.GetParent(target).FullName, "PIX16");
            }
            else if (target.EndsWith("tiffx", true, CultureInfo.InvariantCulture) || target.EndsWith("tiffx/", true, CultureInfo.InvariantCulture))
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

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            IEnumerable<string> tifFiles = Directory.EnumerateFiles(target, "*.tif");
            IEnumerable<string> tifFiles2 = string.IsNullOrWhiteSpace(target2) ? null : Directory.EnumerateFiles(target2, "*.tif");

            if (report == null)
            {
                report = new()
                {
                    numItems = 0,
                    numItemsDone = 0,
                    mainMessage = "",
                    ShowSubProgress = false,
                    numSubItems = 0,
                    numSubItemsDone = 0,
                    subMessage = ""
                };
            }

            if (report.numItems > 0)
            {
                report.numSubItems = tifFiles.Count() + (tifFiles2?.Count() ?? 0);
                report.numSubItemsDone = 0;
                report.subMessage = "Gathering TIF files to pack";
            }
            else
            {
                report.numItems = tifFiles.Count() + (tifFiles2?.Count() ?? 0);
                report.numItemsDone = 0;
                report.mainMessage = "Gathering TIF files to pack";
            }

            foreach (var file in tifFiles)
            {
                CreateAndSavePixFromFile(file, savePath);
                report.mainMessage =
                    $"Converting {Path.GetFileName(file)} to {Path.ChangeExtension(Path.GetFileName(file), "pix")}";
                report.numItemsDone++;
                progressReport.Invoke(report);
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
                Thread.Sleep(1);
            }

            if (!string.IsNullOrWhiteSpace(target2))
            {
                if (!Directory.Exists(savePath2))
                {
                    Directory.CreateDirectory(savePath2);
                }

                foreach (var file in tifFiles2)
                {
                    CreateAndSavePixFromFile(file, savePath2);
                    if (report.numSubItems > 0)
                    {
                        report.subMessage =
                            $"Converting {Path.GetFileName(file)} to {Path.ChangeExtension(Path.GetFileName(file), "pix")}";
                        report.numSubItemsDone++;
                    }
                    else
                    {
                        report.mainMessage =
                            $"Converting {Path.GetFileName(file)} to {Path.ChangeExtension(Path.GetFileName(file), "pix")}";
                        report.numItemsDone++;
                    }

                    progressReport.Invoke(report);
                    if (worker.CancellationPending)
                    {
                        evnt.Cancel = true;
                        return;
                    }
                    Thread.Sleep(1);
                }
            }
        }

        private static void CreateAndSavePixFromFile(string file, string savePath)
        {
            Bitmap bmp = new Bitmap(file);
            PIXIE pixie = PIXIE.FromBitmap(
                bmp.PixelFormat == PixelFormat.Format32bppArgb
                    ? PIXIE.PixelmapFormat.C2_16bitAlpha
                    : PIXIE.PixelmapFormat.C2_16bit, bmp);
            pixie.Name = Path.GetFileNameWithoutExtension(file);
            PIX pixFile = new PIX()
            {
                Pixies =
                {
                    pixie
                }
            };
            pixFile.Save(Path.Combine(savePath, $"{pixie.Name}.pix"));
        }

        public static void PackPixCollections(string path, Action<DataProcessProgressReport> progressReport, BackgroundWorker worker, DoWorkEventArgs evnt, DataProcessProgressReport report = null)
        {
            string target = path;
            string savePath = Directory.GetParent(target).FullName;
            string target2 = null;
            string saveFile = null;
            string saveFile2 = null;
            if (target.EndsWith("pix08", true, CultureInfo.InvariantCulture))
            {
                saveFile = "pixies.p08";
            }
            else if (target.EndsWith("pix16", true, CultureInfo.InvariantCulture))
            {
                saveFile = "pixies.p16";
            }
            else
            {
                bool pix08Exists = Directory.Exists(Path.Combine(target,"pix08"));
                bool pix16Exists = Directory.Exists(Path.Combine(target,"pix16"));
                if (!pix08Exists && !pix16Exists)
                {
                    // just make a pix file in the same directory?
                    savePath = target;
                    saveFile = "pixies.pix";
                }
                else
                {
                    target = !pix16Exists ? Path.Combine(Directory.GetParent(target).FullName, "PIX08") : Path.Combine(Directory.GetParent(target).FullName, "PIX16");
                    saveFile = !pix16Exists ? "pixies.p08" : "pixies.p16";

                    target2 = pix08Exists && pix16Exists
                        ? Path.Combine(Directory.GetParent(target).FullName, "PIX08")
                        : null;
                    saveFile2 = pix08Exists && pix16Exists ? "pixies.p08" : null;
                }
            }
            IEnumerable<string> pixFiles1 = Directory.EnumerateFiles(target, "*.pix");
            IEnumerable<string> pixFiles2 = string.IsNullOrWhiteSpace(target2) ? null : Directory.EnumerateFiles(target2, "*.pix");

            if (report == null)
            {
                report = new()
                {
                    numItems = 0,
                    numItemsDone = 0,
                    mainMessage = "",
                    ShowSubProgress = false,
                    numSubItems = 0,
                    numSubItemsDone = 0,
                    subMessage = ""
                };
            }

            if (report.numItems > 0)
            {
                report.numSubItems = pixFiles1.Count() + (pixFiles2?.Count() ?? 0);
                report.numSubItemsDone = 0;
                report.subMessage = "Gathering PIX files to pack";
            }
            else
            {
                report.numItems = pixFiles1.Count() + (pixFiles2?.Count() ?? 0);
                report.numItemsDone = 0;
                report.mainMessage = "Gathering PIX files to pack";
            }

            PIX pix = new PIX();
            foreach (string file in pixFiles1)
            {
                pix.Pixies.AddRange(PIX.Load(file).Pixies);
                if (report.numSubItems > 0)
                {
                    report.subMessage =
                        $"Packing {Path.GetFileName(file)} into {saveFile}";
                    report.numSubItemsDone++;
                }
                else
                {
                    report.mainMessage =
                        $"Packing {Path.GetFileName(file)} into {saveFile}";
                    report.numItemsDone++;
                }

                progressReport.Invoke(report);
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
            }
            pix.Save(Path.Combine(savePath,saveFile));

            if (!string.IsNullOrWhiteSpace(target2))
            {
                pix = new PIX();
                foreach (string file in pixFiles2)
                {
                    pix.Pixies.AddRange(PIX.Load(file).Pixies);
                    pix.Pixies.AddRange(PIX.Load(file).Pixies);
                    if (report.numSubItems > 0)
                    {
                        report.subMessage =
                            $"Packing {Path.GetFileName(file)} into {saveFile2}";
                        report.numSubItemsDone++;
                    }
                    else
                    {
                        report.mainMessage =
                            $"Packing {Path.GetFileName(file)} into {saveFile2}";
                        report.numItemsDone++;
                    }

                    progressReport.Invoke(report);
                    if (worker.CancellationPending)
                    {
                        evnt.Cancel = true;
                        return;
                    }
                }
                pix.Save(Path.Combine(savePath, saveFile2));
            }
        }
    }
}
