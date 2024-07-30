using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToxicRagers.Brender.Formats;
using ToxicRagers.Carmageddon2.Formats;

namespace Modstockalypse.Utilities
{
    public static class TwtTools
    {

        public static void ExtractTwtFiles(string path, Action<DataProcessProgressReport> progressReport, BackgroundWorker worker, DoWorkEventArgs evnt)
        {
            DirectoryInfo here = new DirectoryInfo(path);
            foreach (FileInfo fi in here.GetFiles("*.TWaT", SearchOption.AllDirectories))
            {
                if (!File.Exists(fi.FullName.Replace(fi.Extension, ".twt")))
                {
                    fi.MoveTo(fi.FullName.Replace(fi.Extension, ".twt"));
                }
            }

            FileInfo[] twtFiles = here.GetFiles("*.twt", SearchOption.AllDirectories);

            DataProcessProgressReport report = new DataProcessProgressReport()
            {
                mainMessage = "",
                numItems = twtFiles.Length,
                numItemsDone = 0,
                subMessage = "",
                numSubItems = 0,
                numSubItemsDone = 0
            };
            foreach (FileInfo fi in twtFiles)
            {
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
                report.mainMessage = $"Extracting TWT file: {fi.Name}";
                report.numItemsDone++;
                using (FileStream stream = fi.OpenRead())
                {
                    TWT twt = TWT.Load(stream);
                    report.numSubItems = twt.Contents.Count;
                    report.numSubItemsDone = 0;
                    twt.Name = Path.GetFileNameWithoutExtension(fi.Name);
                    twt.Location = fi.DirectoryName;
                    
                    foreach (TWTEntry entry in twt.Contents)
                    {
                        report.numSubItemsDone++;
                        twt.Extract(entry, Path.Combine(fi.DirectoryName, twt.Name));
                        report.subMessage = $"Extracting file {report.numSubItemsDone} of {report.numSubItems}: {entry.Name}";
                        progressReport.Invoke(report);
                        if (worker.CancellationPending)
                        {
                            evnt.Cancel = true;
                            return;
                        }
                        Thread.Sleep(1);
                    }
                }

                if (!File.Exists(fi.FullName.Replace(fi.Extension, ".TWaT")))
                {
                    fi.MoveTo(fi.FullName.Replace(fi.Extension, ".TWaT"));
                }
            }

            FileInfo[] pixPacks = here.GetFiles("pixies.p*", SearchOption.AllDirectories);
            report.numItems += pixPacks.Length;
            foreach (FileInfo fi in pixPacks)
            {
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
                string dest = fi.DirectoryName + (fi.Extension.EndsWith("08") ? "\\PIX08\\" : "\\PIX16\\");

                report.mainMessage = $"Extracting TWT file: {fi.Name}";
                report.numItemsDone++;
                PIX pix = PIX.Load(fi.FullName);
                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }

                report.numSubItems = pix.Pixies.Count;
                report.numSubItemsDone = 0;
                foreach (PIXIE pixie in pix.Pixies)
                {
                    pix.Save(Path.Combine(dest, $"{pixie.Name}.pix"), new[] { pixie });
                    report.numSubItemsDone++;
                    report.subMessage = $"Extracting {pixie.Name}.pix from {fi.Name}";
                    progressReport.Invoke(report);
                    Thread.Sleep(1);
                    if (worker.CancellationPending)
                    {
                        evnt.Cancel = true;
                        return;
                    }
                }
                fi.Delete();
            }
        }

        public static void PackTwtFiles(string path, Action<DataProcessProgressReport> progressReport, BackgroundWorker worker, DoWorkEventArgs evnt)
        {
            string target = path;
            string savePath = Directory.GetParent(target).FullName;
            bool tiffsNeedConverting = Directory.Exists(Path.Combine(target, "tiffx")) || Directory.Exists(Path.Combine(target, "tiffrgb"));
            bool pixNeedPacking = Directory.Exists(Path.Combine(target, "pix08")) || Directory.Exists(Path.Combine(target, "pix16"));
            DataProcessProgressReport report = new()
            {
                numItems = (tiffsNeedConverting ? 1 : 0) + (pixNeedPacking ? 1 : 0) + 1,
                numItemsDone = 0,
                mainMessage = "",
                ShowSubProgress = false,
                numSubItems = 0,
                numSubItemsDone = 0,
                subMessage = ""
            };
            if (tiffsNeedConverting)
            {
                report.mainMessage = "Converting TIFs to PIX";
                PixTools.CreatePixFiles(target, progressReport, worker, evnt, report);
                report.numItemsDone++;
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
            }

            if (pixNeedPacking)
            {
                report.mainMessage = "Packing up PIX files";
                PixTools.PackPixCollections(target, progressReport, worker, evnt, report);
                report.numItemsDone++;
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
            }

            report.mainMessage = $"Packing TWT file";
            TWT twt = TWT.Create(target, savePath);

            IEnumerable<string> filesToPack = Directory.EnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
            report.numSubItems = filesToPack.Count();
            report.numSubItemsDone = 0;
            
            foreach (string file in filesToPack)
            {
                twt.Contents.Add(TWTEntry.FromFile(file));
                report.subMessage = $"Packing {Path.GetFileName(file)} into {Path.GetFileName(target)}";
                report.numSubItemsDone++;

                progressReport.Invoke(report);
                if (worker.CancellationPending)
                {
                    evnt.Cancel = true;
                    return;
                }
            }

            twt.Save();
            report.numItemsDone++;
            report.mainMessage = "TWT saved!";
            progressReport.Invoke(report);
        }

    }
}
