using System.Drawing.Imaging;
using System.Globalization;
using System.IO.Compression;
using System.IO;
using ToxicRagers.Brender.Formats;
using ToxicRagers.Carmageddon.Helpers;
using ToxicRagers.Carmageddon2.Formats;
using ToxicRagers;
using static ToxicRagers.Brender.Formats.MATMaterial;

namespace Modstockalypse
{
    public partial class MainForm : Form
    {
        string path;
        OpponentTXT opponents;
        readonly List<OpponentDetails> carMods = new List<OpponentDetails>();
        readonly Dictionary<int, string> carModZips = new Dictionary<int, string>();

        string selectedRace;
        string raceMod;
        RacesTXT races;
        readonly List<RaceDetails> raceMods = new List<RaceDetails>();
        readonly Dictionary<int, string> raceModZips = new Dictionary<int, string>();
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            lblCarVersion.Text = $"v{Application.ProductVersion}";

            reloadUI();
        }

        private void lstCarAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCarAvailable.SelectedIndex >= 0)
            {
                btnCarInstall.Enabled = true;

                loadDetails(carMods[lstCarAvailable.SelectedIndex], "Available", lstCarAvailable.SelectedIndex);
            }
        }

        private void lstCarInstalled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCarInstalled.SelectedIndex >= 0)
            {
                btnCarUninstall.Enabled = true;

                btnCarUp.Enabled = lstCarInstalled.SelectedIndex > 0;
                btnCarDown.Enabled = lstCarInstalled.SelectedIndex + 1 < opponents.Opponents.Count;

                loadDetails(opponents.Opponents[lstCarInstalled.SelectedIndex], "Installed");
            }
        }

        private void loadDetails(OpponentDetails opponent, string panel, int index = -1)
        {
            string carName = Path.GetFileNameWithoutExtension(opponent.CarFilename);

            (Controls.Find($"txtCar{panel}Description", true)[0] as TextBox).Text = $"Driver: {opponent.DriverName}\r\nCar: {opponent.CarName}\r\nStrength: {opponent.StrengthRating}\r\nCost: {opponent.CostToBuy}\r\nNetwork Availability: {opponent.NetworkAvailability}\r\n{opponent.TopSpeed}\r\n{opponent.KerbWeight}\r\n{opponent.To60}";

            Bitmap carImage = new Bitmap(192, 128);

            using (Graphics g = Graphics.FromImage(carImage))
            {
                g.DrawImage(getBitmap(carName, "A", index), 0, 0);
                g.DrawImage(getBitmap(carName, "B", index), 64, 0);
                g.DrawImage(getBitmap(carName, "C", index), 128, 0);
                g.DrawImage(getBitmap(carName, "D", index), 0, 64);
                g.DrawImage(getBitmap(carName, "E", index), 64, 64);
                g.DrawImage(getBitmap(carName, "F", index), 128, 64);
            }

            (Controls.Find($"pbCar{panel}", true)[0] as PictureBox).Image = carImage;
        }

        private Bitmap getBitmap(string carName, string letter, int index)
        {
            string filename = $"{carName}{letter}";

            if (index > -1 && carModZips.ContainsKey(index))
            {
                using (FileStream fs = new FileStream(carModZips[index], FileMode.Open))
                using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry;

                    entry = archive.Entries.FirstOrDefault(f => string.Compare(f.Name, $"{filename}.tif", true) == 0);

                    if (entry != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            entry.Open().CopyTo(ms);
                            ms.Seek(0, SeekOrigin.Begin);

                            return new Bitmap((Bitmap)Image.FromStream(ms));
                        }
                    }

                    entry = archive.Entries.FirstOrDefault(f => string.Compare(f.Name, $"{filename}.pix", true) == 0 && f.FullName.ToLower().Contains("pix16"));

                    if (entry != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            entry.Open().CopyTo(ms);
                            ms.Seek(0, SeekOrigin.Begin);

                            PIX pix = PIX.Load(ms);

                            if (pix is null) { return new Bitmap(64, 64); }

                            pix.Pixies[0].Format = PIXIE.PixelmapFormat.C2_16bitAlpha;
                            return pix.Pixies[0].GetBitmap();
                        }
                    }

                    entry = archive.Entries.FirstOrDefault(f => string.Compare(f.Name, $"{carName}CI.twt", true) == 0);

                    if (entry != null)
                    {
                        // twt in a zip will contain pixies
                        using (MemoryStream ms = new MemoryStream())
                        {
                            entry.Open().CopyTo(ms);
                            ms.Seek(0, SeekOrigin.Begin);

                            TWT twt = TWT.Load(ms);
                            TWTEntry twtEntry = twt.Contents.FirstOrDefault(c => string.Compare(c.Name, "pixies.p16", true) == 0);

                            if (twtEntry != null)
                            {
                                using (MemoryStream msTWT = new MemoryStream(twt.Extract(twtEntry)))
                                {
                                    PIX pix = PIX.Load(msTWT);

                                    PIXIE pixie = pix.Pixies.First(p => string.Compare(p.Name, $"{filename}", true) == 0);
                                    pixie.Format = PIXIE.PixelmapFormat.C2_16bitAlpha;
                                    return pixie.GetBitmap();
                                }
                            }
                        }
                    }
                }
            }

            if (Directory.Exists(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI")))
            {
                if (Directory.Exists(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI", "tiffrgb")))
                {
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI", "tiffrgb", $"{filename}.tif"))))
                    {
                        return new Bitmap((Bitmap)Image.FromStream(ms));
                    }
                }

                if (Directory.Exists(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI", "pix16")))
                {
                    PIX pix = PIX.Load(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI", "pix16", $"{filename}.pix"));

                    if (pix is null) { return new Bitmap(64, 64); }

                    pix.Pixies[0].Format = PIXIE.PixelmapFormat.C2_16bitAlpha;
                    return pix.Pixies[0].GetBitmap();
                }

            }
            else if (File.Exists(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI.twt")))
            {
                TWT twt = TWT.Load(Path.Combine(path, "data", "intrface", "carimage", $"{carName}CI.twt"));

                using (MemoryStream ms = new MemoryStream(twt.Extract(twt.Contents.First(c => string.Compare(c.Name, "PIXIES.P16", true) == 0))))
                {
                    PIX pix = PIX.Load(ms);

                    PIXIE pixie = pix.Pixies.First(p => string.Compare(p.Name, $"{filename}", true) == 0);
                    pixie.Format = PIXIE.PixelmapFormat.C2_16bitAlpha;
                    return pixie.GetBitmap();
                }
            }

            return new Bitmap(64, 64);
        }

        private void btnCarInstall_Click(object sender, EventArgs e)
        {
            bool success = false;

            OpponentDetails newCar = carMods[lstCarAvailable.SelectedIndex];

            string newCarName = Path.GetFileNameWithoutExtension(newCar.CarFilename);

            // extract archive (if installing from one)
            if (carModZips.ContainsKey(lstCarAvailable.SelectedIndex))
            {
                using (ZipArchive archive = ZipFile.OpenRead(carModZips[lstCarAvailable.SelectedIndex]))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == "") { continue; }

                        string destinationPath = Path.GetFullPath(Path.Combine(path, entry.FullName));

                        if (destinationPath.StartsWith(path, StringComparison.Ordinal))
                        {
                            if (File.Exists(destinationPath)) { File.Delete(destinationPath); }

                            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                            entry.ExtractToFile(destinationPath);
                        }
                    }
                }
            }

            // validate installation by checking CarFilename exists
            if (Directory.Exists(Path.Combine(path, "data", "cars", newCarName)))
            {
                success = File.Exists(Path.Combine(path, "data", "cars", newCarName, $"{newCarName}.txt"));
            }
            else
            {
                if (File.Exists(Path.Combine(path, "data", "cars", $"{newCarName}.twt")))
                {
                    TWT twt = TWT.Load(Path.Combine(path, "data", "cars", $"{newCarName}.twt"));

                    success = twt.Contents.Any(entry => string.Compare(entry.Name, $"{newCarName}.txt", true) == 0);
                }
            }

            if (success)
            {
                opponents.Opponents.Add(carMods[lstCarAvailable.SelectedIndex]);
                saveOpponentsTXT();

                reloadUI();
            }
        }

        private void saveOpponentsTXT()
        {
            File.Delete(Path.Combine(path, "data", "opponent.old"));
            File.Move(Path.Combine(path, "data", "opponent.txt"), Path.Combine(path, "data", "opponent.old"));
            opponents.Save(Path.Combine(path, "data", "opponent.txt"));
        }

        private void btnCarUninstall_Click(object sender, EventArgs e)
        {
            int index = lstCarInstalled.SelectedIndex;
            OpponentDetails opponent = opponents.Opponents[index];

            using (DocumentWriter dw = new DocumentWriter(Path.Combine(path, $"{Path.GetFileNameWithoutExtension(opponent.CarFilename)}.c2c")))
            {
                opponent.Write(dw);
            }

            opponents.Opponents.RemoveAt(index);

            saveOpponentsTXT();
            reloadUI();
        }

        private void btnCarUp_Click(object sender, EventArgs e)
        {
            moveItem(lstCarInstalled.SelectedIndex, -1);
        }

        private void btnCarDown_Click(object sender, EventArgs e)
        {
            moveItem(lstCarInstalled.SelectedIndex, 1);
        }

        private void moveItem(int index, int offset)
        {
            int newIndex = index + offset;

            OpponentDetails opponent = opponents.Opponents[index];

            lstCarInstalled.Items.RemoveAt(index);
            lstCarInstalled.Items.Insert(newIndex, opponent.CarName);

            opponents.Opponents.RemoveAt(index);
            opponents.Opponents.Insert(newIndex, opponent);

            saveOpponentsTXT();

            lstCarInstalled.SelectedIndex = newIndex;
        }

        private void reloadUI()
        {
            path = Directory.GetCurrentDirectory();
            string twtFile = Path.Combine(path, "data.twt");

            if (File.Exists(twtFile))
            {
                TWT twt = TWT.Load(twtFile);

                foreach (TWTEntry entry in twt.Contents)
                {
                    twt.Extract(entry, Path.Combine(path, "data"));
                }

                File.Move(twtFile, Path.Combine(path, "data.twat"));
            }

            reloadCarUI();
            reloadRaceUI();
        }
        private void reloadCarUI()
        {
            carMods.Clear();
            carModZips.Clear();
            btnCarInstall.Enabled = false;
            btnCarUninstall.Enabled = false;
            lstCarAvailable.Items.Clear();
            lstCarInstalled.Items.Clear();
            txtCarAvailableDescription.Text = "";
            txtCarInstalledDescription.Text = "";
            pbCarAvailable.Image = null;
            pbCarInstalled.Image = null;

            string opponentTXT = Path.Combine(path, "data", "opponent.txt");

            if (File.Exists(opponentTXT))
            {
                opponents = OpponentTXT.Load(opponentTXT);

                foreach (OpponentDetails opponent in opponents.Opponents)
                {
                    lstCarInstalled.Items.Add(opponent.CarName);
                }
            }

            string carModDir = Path.Combine(path, ".mods", "cars");

            if (Directory.Exists(carModDir))
            {
                foreach (string file in Directory.GetFiles(carModDir, "*.zip"))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
                    {
                        if (archive.Entries.Any(f => f.Name.EndsWith(".c2c", StringComparison.InvariantCultureIgnoreCase)) &&
                            archive.Entries.Any(f => f.FullName.ToLower().StartsWith(@"data/cars/")))
                        {
                            ZipArchiveEntry entry = archive.Entries.First(f => f.Name.EndsWith(".c2c", StringComparison.InvariantCultureIgnoreCase) && f.Name == f.FullName);

                            using (MemoryStream ms = new MemoryStream())
                            using (BinaryReader br = new BinaryReader(ms))
                            {
                                entry.Open().CopyTo(ms);

                                ms.Seek(0, SeekOrigin.Begin);

                                OpponentDetails details = OpponentDetails.Load(new DocumentParser(br));

                                if (!opponents.Opponents.Any(o => string.Compare(o.CarFilename, details.CarFilename, true) == 0))
                                {
                                    int index = lstCarAvailable.Items.Add(details.CarName);

                                    carModZips.Add(index, file);
                                    carMods.Add(details);
                                }
                            }
                        }
                    }
                }
            }

            foreach (string file in Directory.GetFiles(path, "*.c2c"))
            {
                OpponentDetails details = OpponentDetails.Load(new DocumentParser(file));

                if (!carMods.Any(m => string.Compare(m.CarFilename, details.CarFilename, true) == 0) &&
                    !opponents.Opponents.Any(o => string.Compare(o.CarFilename, details.CarFilename, true) == 0))
                {
                    lstCarAvailable.Items.Add(details.CarName);
                    carMods.Add(details);
                }
            }
        }

        private void reloadRaceUI()
        {
            raceMods.Clear();
            raceModZips.Clear();
            lstRacesRaces.Items.Clear();
            lstRacesMods.Items.Clear();
            btnRacesInstall.Text = "select a mod and a map to replace, then click here!";
            btnRacesInstall.Enabled = false;

            string racesTXT = Path.Combine(path, "data", "races.txt");

            if (File.Exists(racesTXT))
            {
                races = RacesTXT.Load(Path.Combine(path, "data", "races.txt"));

                foreach (RaceDetails race in races.Races)
                {
                    if (!race.BoundaryRace) { lstRacesRaces.Items.Add(race.Name); }
                }
            }

            string mapModDir = Path.Combine(path, ".mods", "maps");

            if (Directory.Exists(mapModDir))
            {
                foreach (string file in Directory.GetFiles(mapModDir, "*.zip"))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
                    {
                        if (archive.Entries.Any(f => f.Name.EndsWith(".c2t", StringComparison.InvariantCultureIgnoreCase)) &&
                            archive.Entries.Any(f => f.Name == "" && f.FullName.ToLower() == @"data/races/"))
                        {
                            ZipArchiveEntry entry = archive.Entries.First(f => f.Name.EndsWith(".c2t", StringComparison.InvariantCultureIgnoreCase));

                            using (MemoryStream ms = new MemoryStream())
                            using (BinaryReader br = new BinaryReader(ms))
                            {
                                entry.Open().CopyTo(ms);

                                ms.Seek(0, SeekOrigin.Begin);

                                RaceDetails details = RaceDetails.Load(new DocumentParser(br));

                                int index = lstRacesMods.Items.Add(details.Name);

                                raceModZips.Add(index, file);
                                raceMods.Add(details);
                            }
                        }
                    }
                }
            }

            foreach (string file in Directory.GetFiles(path, "*.c2t"))
            {
                RaceDetails details = RaceDetails.Load(new DocumentParser(file));

                if (!raceMods.Any(m => m.Name == details.Name))
                {
                    lstRacesMods.Items.Add(details.Name);
                    raceMods.Add(details);
                }
            }
        }

        private void updateRaceAction()
        {
            btnRacesInstall.Text = $"replace {selectedRace} with {raceMod}";

            if (!string.IsNullOrEmpty(selectedRace) && !string.IsNullOrEmpty(raceMod))
            {
                btnRacesInstall.Enabled = true;
            }
        }

        private void lstRaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRace = lstRacesRaces.GetItemText(lstRacesRaces.SelectedItem);

            updateRaceAction();
        }

        private void lstRacesMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            raceMod = lstRacesMods.GetItemText(lstRacesMods.SelectedItem);

            updateRaceAction();
        }
        private void llToxicRagers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llToxicRagers.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.toxic-ragers.co.uk");
        }

        private void btnRacesInstall_Click(object sender, EventArgs e)
        {
            bool success = false;

            RaceDetails oldRace = races.Races[lstRacesRaces.SelectedIndex];
            RaceDetails newRace = raceMods[lstRacesMods.SelectedIndex];

            string oldRaceName = Path.GetFileNameWithoutExtension(oldRace.RaceFilename);
            string newRaceName = Path.GetFileNameWithoutExtension(newRace.RaceFilename);

            // extract archive (if installing from one)
            if (raceModZips.ContainsKey(lstRacesMods.SelectedIndex))
            {
                using (ZipArchive archive = ZipFile.OpenRead(raceModZips[lstRacesMods.SelectedIndex]))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == "") { continue; }

                        string destinationPath = Path.GetFullPath(Path.Combine(path, entry.FullName));

                        if (destinationPath.StartsWith(path, StringComparison.Ordinal))
                        {
                            if (File.Exists(destinationPath)) { File.Delete(destinationPath); }

                            entry.ExtractToFile(destinationPath);
                        }
                    }
                }
            }

            // validate installation by checking MapFilename exists
            if (Directory.Exists(Path.Combine(path, "data", "races", newRaceName)))
            {
                success = File.Exists(Path.Combine(path, "data", "races", newRaceName, $"{newRaceName}.txt"));
            }
            else
            {
                if (File.Exists(Path.Combine(path, "data", "races", $"{newRaceName}.twt")))
                {
                    TWT twt = TWT.Load(Path.Combine(path, "data", "races", $"{newRaceName}.twt"));

                    success = twt.Contents.Any(entry => string.Compare(entry.Name, $"{newRaceName}.txt", true) == 0);
                }
            }

            if (success)
            {
                // create c2t file for track to be replaced
                using (DocumentWriter dw = new DocumentWriter(Path.Combine(path, $"{oldRaceName}.c2t")))
                {
                    oldRace.Write(dw);
                }

                // update races.txt
                races.Races[lstRacesRaces.SelectedIndex] = newRace;
                File.Delete(Path.Combine(path, "data", "races.old"));
                File.Move(Path.Combine(path, "data", "races.txt"), Path.Combine(path, "data", "races.old"));
                races.Save(Path.Combine(path, "data", "races.txt"));

                reloadUI();
            }
        }

        private void btnExtractTwtFiles_Click(object sender, EventArgs e)
        {

            DirectoryInfo here = new DirectoryInfo(path);
            foreach (FileInfo fi in here.GetFiles("*.TWaT", SearchOption.AllDirectories))
            {
                fi.MoveTo(fi.FullName.Replace(fi.Extension, ".twt"));
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
                fi.MoveTo(fi.FullName.Replace(fi.Extension, ".TWaT"));
            }
            foreach (FileInfo fi in here.GetFiles("pixies.p*", SearchOption.AllDirectories))
            {
                string dest = fi.DirectoryName + (fi.Extension.EndsWith("08") ? "\\PIX08\\" : "\\PIX16\\");

                PIX pix = PIX.Load(fi.FullName);
                pix.ExtractPixies(dest);
                fi.Delete();
            }
        }

        private void btnExtractPixFiles_Click(object sender, EventArgs e)
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
                    bmp.Save(Path.Combine(dest, $"{pixie.Name}.tif"), ImageFormat.Tiff);
                }
            }
        }

        private void btnPackTwtFile_Click(object sender, EventArgs e)
        {
            fldPackingBrowser.InitialDirectory = path;

            if (fldPackingBrowser.ShowDialog() == DialogResult.OK)
            {
                string target = fldPackingBrowser.SelectedPath;

            }
        }
    }
}
