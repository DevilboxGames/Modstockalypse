namespace Modstockalypse
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new TabControl();
            carsTab = new TabPage();
            tlpCarPanels = new TableLayoutPanel();
            pnlCarButtons = new Panel();
            btnCarUninstall = new Button();
            btnCarInstall = new Button();
            gbCarAvailable = new GroupBox();
            pbCarAvailable = new PictureBox();
            txtCarAvailableDescription = new TextBox();
            lblCarAvailableDescription = new Label();
            lstCarAvailable = new ListBox();
            gbCarInstalled = new GroupBox();
            lblCarVersion = new Label();
            pbCarInstalled = new PictureBox();
            txtCarInstalledDescription = new TextBox();
            lblCarInstalledDescription = new Label();
            btnCarDown = new Button();
            lstCarInstalled = new ListBox();
            btnCarUp = new Button();
            racesTab = new TabPage();
            scRacesPanels = new SplitContainer();
            lstRacesRaces = new ListBox();
            scRacesRight = new SplitContainer();
            lstRacesMods = new ListBox();
            pnlRacesButtons = new Panel();
            llToxicRagers = new LinkLabel();
            btnRacesInstall = new Button();
            toolsTab = new TabPage();
            gbDataTools = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnExtractTwtFiles = new Button();
            btnExtractPixFiles = new Button();
            btnPackTwtFile = new Button();
            btnCreatePixFiles = new Button();
            btnPackPixFiles = new Button();
            ofdFolderBrowser = new OpenFileDialog();
            fbdFolderBrowser = new FolderBrowserDialog();
            tabControl1.SuspendLayout();
            carsTab.SuspendLayout();
            tlpCarPanels.SuspendLayout();
            pnlCarButtons.SuspendLayout();
            gbCarAvailable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCarAvailable).BeginInit();
            gbCarInstalled.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCarInstalled).BeginInit();
            racesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scRacesPanels).BeginInit();
            scRacesPanels.Panel1.SuspendLayout();
            scRacesPanels.Panel2.SuspendLayout();
            scRacesPanels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scRacesRight).BeginInit();
            scRacesRight.Panel1.SuspendLayout();
            scRacesRight.Panel2.SuspendLayout();
            scRacesRight.SuspendLayout();
            pnlRacesButtons.SuspendLayout();
            toolsTab.SuspendLayout();
            gbDataTools.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(carsTab);
            tabControl1.Controls.Add(racesTab);
            tabControl1.Controls.Add(toolsTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(757, 601);
            tabControl1.TabIndex = 0;
            // 
            // carsTab
            // 
            carsTab.Controls.Add(tlpCarPanels);
            carsTab.Location = new Point(4, 24);
            carsTab.Margin = new Padding(4, 3, 4, 3);
            carsTab.Name = "carsTab";
            carsTab.Padding = new Padding(4, 3, 4, 3);
            carsTab.Size = new Size(749, 573);
            carsTab.TabIndex = 0;
            carsTab.Text = "Cars";
            carsTab.UseVisualStyleBackColor = true;
            // 
            // tlpCarPanels
            // 
            tlpCarPanels.ColumnCount = 3;
            tlpCarPanels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCarPanels.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58F));
            tlpCarPanels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCarPanels.Controls.Add(pnlCarButtons, 1, 0);
            tlpCarPanels.Controls.Add(gbCarAvailable, 0, 0);
            tlpCarPanels.Controls.Add(gbCarInstalled, 2, 0);
            tlpCarPanels.Dock = DockStyle.Fill;
            tlpCarPanels.Location = new Point(4, 3);
            tlpCarPanels.Margin = new Padding(4, 3, 4, 3);
            tlpCarPanels.Name = "tlpCarPanels";
            tlpCarPanels.RowCount = 1;
            tlpCarPanels.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCarPanels.Size = new Size(741, 567);
            tlpCarPanels.TabIndex = 0;
            // 
            // pnlCarButtons
            // 
            pnlCarButtons.Controls.Add(btnCarUninstall);
            pnlCarButtons.Controls.Add(btnCarInstall);
            pnlCarButtons.Dock = DockStyle.Fill;
            pnlCarButtons.Location = new Point(345, 3);
            pnlCarButtons.Margin = new Padding(4, 3, 4, 3);
            pnlCarButtons.Name = "pnlCarButtons";
            pnlCarButtons.Size = new Size(50, 561);
            pnlCarButtons.TabIndex = 0;
            // 
            // btnCarUninstall
            // 
            btnCarUninstall.Enabled = false;
            btnCarUninstall.Location = new Point(0, 144);
            btnCarUninstall.Margin = new Padding(4, 3, 4, 3);
            btnCarUninstall.Name = "btnCarUninstall";
            btnCarUninstall.Size = new Size(51, 51);
            btnCarUninstall.TabIndex = 1;
            btnCarUninstall.Text = "←";
            btnCarUninstall.UseVisualStyleBackColor = true;
            btnCarUninstall.Click += btnCarUninstall_Click;
            // 
            // btnCarInstall
            // 
            btnCarInstall.Enabled = false;
            btnCarInstall.Location = new Point(0, 87);
            btnCarInstall.Margin = new Padding(4, 3, 4, 3);
            btnCarInstall.Name = "btnCarInstall";
            btnCarInstall.Size = new Size(51, 51);
            btnCarInstall.TabIndex = 0;
            btnCarInstall.Text = "→";
            btnCarInstall.UseVisualStyleBackColor = true;
            btnCarInstall.Click += btnCarInstall_Click;
            // 
            // gbCarAvailable
            // 
            gbCarAvailable.Controls.Add(pbCarAvailable);
            gbCarAvailable.Controls.Add(txtCarAvailableDescription);
            gbCarAvailable.Controls.Add(lblCarAvailableDescription);
            gbCarAvailable.Controls.Add(lstCarAvailable);
            gbCarAvailable.Dock = DockStyle.Fill;
            gbCarAvailable.Location = new Point(4, 3);
            gbCarAvailable.Margin = new Padding(4, 3, 4, 3);
            gbCarAvailable.Name = "gbCarAvailable";
            gbCarAvailable.Padding = new Padding(4, 3, 4, 3);
            gbCarAvailable.Size = new Size(333, 561);
            gbCarAvailable.TabIndex = 1;
            gbCarAvailable.TabStop = false;
            gbCarAvailable.Text = "available cars";
            // 
            // pbCarAvailable
            // 
            pbCarAvailable.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pbCarAvailable.Location = new Point(62, 403);
            pbCarAvailable.Margin = new Padding(4, 3, 4, 3);
            pbCarAvailable.Name = "pbCarAvailable";
            pbCarAvailable.Size = new Size(224, 148);
            pbCarAvailable.TabIndex = 9;
            pbCarAvailable.TabStop = false;
            // 
            // txtCarAvailableDescription
            // 
            txtCarAvailableDescription.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCarAvailableDescription.Location = new Point(10, 268);
            txtCarAvailableDescription.Margin = new Padding(4, 3, 4, 3);
            txtCarAvailableDescription.Multiline = true;
            txtCarAvailableDescription.Name = "txtCarAvailableDescription";
            txtCarAvailableDescription.ReadOnly = true;
            txtCarAvailableDescription.Size = new Size(320, 127);
            txtCarAvailableDescription.TabIndex = 8;
            // 
            // lblCarAvailableDescription
            // 
            lblCarAvailableDescription.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCarAvailableDescription.AutoSize = true;
            lblCarAvailableDescription.Location = new Point(10, 250);
            lblCarAvailableDescription.Margin = new Padding(4, 0, 4, 0);
            lblCarAvailableDescription.Name = "lblCarAvailableDescription";
            lblCarAvailableDescription.Size = new Size(66, 15);
            lblCarAvailableDescription.TabIndex = 7;
            lblCarAvailableDescription.Text = "description";
            // 
            // lstCarAvailable
            // 
            lstCarAvailable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstCarAvailable.FormattingEnabled = true;
            lstCarAvailable.IntegralHeight = false;
            lstCarAvailable.ItemHeight = 15;
            lstCarAvailable.Location = new Point(7, 22);
            lstCarAvailable.Margin = new Padding(4, 3, 4, 3);
            lstCarAvailable.Name = "lstCarAvailable";
            lstCarAvailable.Size = new Size(319, 224);
            lstCarAvailable.TabIndex = 0;
            lstCarAvailable.SelectedIndexChanged += lstCarAvailable_SelectedIndexChanged;
            // 
            // gbCarInstalled
            // 
            gbCarInstalled.Controls.Add(lblCarVersion);
            gbCarInstalled.Controls.Add(pbCarInstalled);
            gbCarInstalled.Controls.Add(txtCarInstalledDescription);
            gbCarInstalled.Controls.Add(lblCarInstalledDescription);
            gbCarInstalled.Controls.Add(btnCarDown);
            gbCarInstalled.Controls.Add(lstCarInstalled);
            gbCarInstalled.Controls.Add(btnCarUp);
            gbCarInstalled.Dock = DockStyle.Fill;
            gbCarInstalled.Location = new Point(403, 3);
            gbCarInstalled.Margin = new Padding(4, 3, 4, 3);
            gbCarInstalled.Name = "gbCarInstalled";
            gbCarInstalled.Padding = new Padding(4, 3, 4, 3);
            gbCarInstalled.Size = new Size(334, 561);
            gbCarInstalled.TabIndex = 2;
            gbCarInstalled.TabStop = false;
            gbCarInstalled.Text = "installed cars";
            // 
            // lblCarVersion
            // 
            lblCarVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblCarVersion.AutoSize = true;
            lblCarVersion.BorderStyle = BorderStyle.FixedSingle;
            lblCarVersion.Location = new Point(278, 544);
            lblCarVersion.Margin = new Padding(4, 0, 4, 0);
            lblCarVersion.Name = "lblCarVersion";
            lblCarVersion.Size = new Size(48, 17);
            lblCarVersion.TabIndex = 3;
            lblCarVersion.Text = "v1.0.0.0";
            // 
            // pbCarInstalled
            // 
            pbCarInstalled.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pbCarInstalled.Location = new Point(50, 403);
            pbCarInstalled.Margin = new Padding(4, 3, 4, 3);
            pbCarInstalled.Name = "pbCarInstalled";
            pbCarInstalled.Size = new Size(224, 148);
            pbCarInstalled.TabIndex = 6;
            pbCarInstalled.TabStop = false;
            // 
            // txtCarInstalledDescription
            // 
            txtCarInstalledDescription.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCarInstalledDescription.Location = new Point(7, 268);
            txtCarInstalledDescription.Margin = new Padding(4, 3, 4, 3);
            txtCarInstalledDescription.Multiline = true;
            txtCarInstalledDescription.Name = "txtCarInstalledDescription";
            txtCarInstalledDescription.ReadOnly = true;
            txtCarInstalledDescription.Size = new Size(320, 127);
            txtCarInstalledDescription.TabIndex = 5;
            txtCarInstalledDescription.Text = "Driver: ERROL\r\nCar: PLACEHOLDER\r\nStrength: 1\r\nCost: 99p\r\nNetwork Availability: never\r\nTOP SPEED: 4MPH\r\nKERB WEIGHT: SEVERAL TONS\r\n0-60MPH: INFINITE SECONDS";
            // 
            // lblCarInstalledDescription
            // 
            lblCarInstalledDescription.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblCarInstalledDescription.AutoSize = true;
            lblCarInstalledDescription.Location = new Point(7, 250);
            lblCarInstalledDescription.Margin = new Padding(4, 0, 4, 0);
            lblCarInstalledDescription.Name = "lblCarInstalledDescription";
            lblCarInstalledDescription.Size = new Size(66, 15);
            lblCarInstalledDescription.TabIndex = 4;
            lblCarInstalledDescription.Text = "description";
            // 
            // btnCarDown
            // 
            btnCarDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCarDown.Enabled = false;
            btnCarDown.Location = new Point(276, 144);
            btnCarDown.Margin = new Padding(4, 3, 4, 3);
            btnCarDown.Name = "btnCarDown";
            btnCarDown.Size = new Size(51, 51);
            btnCarDown.TabIndex = 3;
            btnCarDown.Text = "↓";
            btnCarDown.UseVisualStyleBackColor = true;
            btnCarDown.Click += btnCarDown_Click;
            // 
            // lstCarInstalled
            // 
            lstCarInstalled.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstCarInstalled.FormattingEnabled = true;
            lstCarInstalled.IntegralHeight = false;
            lstCarInstalled.ItemHeight = 15;
            lstCarInstalled.Location = new Point(5, 22);
            lstCarInstalled.Margin = new Padding(4, 3, 4, 3);
            lstCarInstalled.Name = "lstCarInstalled";
            lstCarInstalled.Size = new Size(264, 224);
            lstCarInstalled.TabIndex = 1;
            lstCarInstalled.SelectedIndexChanged += lstCarInstalled_SelectedIndexChanged;
            // 
            // btnCarUp
            // 
            btnCarUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCarUp.Enabled = false;
            btnCarUp.Location = new Point(276, 87);
            btnCarUp.Margin = new Padding(4, 3, 4, 3);
            btnCarUp.Name = "btnCarUp";
            btnCarUp.Size = new Size(51, 51);
            btnCarUp.TabIndex = 2;
            btnCarUp.Text = "↑";
            btnCarUp.UseVisualStyleBackColor = true;
            btnCarUp.Click += btnCarUp_Click;
            // 
            // racesTab
            // 
            racesTab.Controls.Add(scRacesPanels);
            racesTab.Location = new Point(4, 24);
            racesTab.Margin = new Padding(4, 3, 4, 3);
            racesTab.Name = "racesTab";
            racesTab.Padding = new Padding(4, 3, 4, 3);
            racesTab.Size = new Size(749, 573);
            racesTab.TabIndex = 1;
            racesTab.Text = "Races";
            racesTab.UseVisualStyleBackColor = true;
            // 
            // scRacesPanels
            // 
            scRacesPanels.Dock = DockStyle.Fill;
            scRacesPanels.Location = new Point(4, 3);
            scRacesPanels.Name = "scRacesPanels";
            // 
            // scRacesPanels.Panel1
            // 
            scRacesPanels.Panel1.Controls.Add(lstRacesRaces);
            // 
            // scRacesPanels.Panel2
            // 
            scRacesPanels.Panel2.Controls.Add(scRacesRight);
            scRacesPanels.Size = new Size(741, 567);
            scRacesPanels.SplitterDistance = 340;
            scRacesPanels.TabIndex = 1;
            // 
            // lstRacesRaces
            // 
            lstRacesRaces.BorderStyle = BorderStyle.FixedSingle;
            lstRacesRaces.Dock = DockStyle.Fill;
            lstRacesRaces.FormattingEnabled = true;
            lstRacesRaces.IntegralHeight = false;
            lstRacesRaces.ItemHeight = 15;
            lstRacesRaces.Location = new Point(0, 0);
            lstRacesRaces.Name = "lstRacesRaces";
            lstRacesRaces.Size = new Size(340, 567);
            lstRacesRaces.TabIndex = 0;
            lstRacesRaces.SelectedIndexChanged += lstRaces_SelectedIndexChanged;
            // 
            // scRacesRight
            // 
            scRacesRight.Dock = DockStyle.Fill;
            scRacesRight.Location = new Point(0, 0);
            scRacesRight.Name = "scRacesRight";
            scRacesRight.Orientation = Orientation.Horizontal;
            // 
            // scRacesRight.Panel1
            // 
            scRacesRight.Panel1.Controls.Add(lstRacesMods);
            // 
            // scRacesRight.Panel2
            // 
            scRacesRight.Panel2.Controls.Add(pnlRacesButtons);
            scRacesRight.Size = new Size(397, 567);
            scRacesRight.SplitterDistance = 423;
            scRacesRight.TabIndex = 0;
            // 
            // lstRacesMods
            // 
            lstRacesMods.BorderStyle = BorderStyle.FixedSingle;
            lstRacesMods.Dock = DockStyle.Fill;
            lstRacesMods.FormattingEnabled = true;
            lstRacesMods.IntegralHeight = false;
            lstRacesMods.ItemHeight = 15;
            lstRacesMods.Location = new Point(0, 0);
            lstRacesMods.Name = "lstRacesMods";
            lstRacesMods.Size = new Size(397, 423);
            lstRacesMods.TabIndex = 0;
            lstRacesMods.SelectedIndexChanged += lstRacesMods_SelectedIndexChanged;
            // 
            // pnlRacesButtons
            // 
            pnlRacesButtons.BorderStyle = BorderStyle.FixedSingle;
            pnlRacesButtons.Controls.Add(llToxicRagers);
            pnlRacesButtons.Controls.Add(btnRacesInstall);
            pnlRacesButtons.Dock = DockStyle.Fill;
            pnlRacesButtons.Location = new Point(0, 0);
            pnlRacesButtons.Name = "pnlRacesButtons";
            pnlRacesButtons.Size = new Size(397, 140);
            pnlRacesButtons.TabIndex = 0;
            // 
            // llToxicRagers
            // 
            llToxicRagers.Location = new Point(3, 47);
            llToxicRagers.Name = "llToxicRagers";
            llToxicRagers.Size = new Size(275, 41);
            llToxicRagers.TabIndex = 2;
            llToxicRagers.TabStop = true;
            llToxicRagers.Text = "Built with love by the one and only Toxic Ranger";
            llToxicRagers.TextAlign = ContentAlignment.MiddleCenter;
            llToxicRagers.LinkClicked += llToxicRagers_LinkClicked;
            // 
            // btnRacesInstall
            // 
            btnRacesInstall.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRacesInstall.FlatStyle = FlatStyle.System;
            btnRacesInstall.Location = new Point(3, 3);
            btnRacesInstall.Name = "btnRacesInstall";
            btnRacesInstall.Size = new Size(388, 41);
            btnRacesInstall.TabIndex = 1;
            btnRacesInstall.Text = "replace {race} with {mod}";
            btnRacesInstall.UseVisualStyleBackColor = true;
            btnRacesInstall.Click += btnRacesInstall_Click;
            // 
            // toolsTab
            // 
            toolsTab.Controls.Add(gbDataTools);
            toolsTab.Location = new Point(4, 24);
            toolsTab.Margin = new Padding(4, 3, 4, 3);
            toolsTab.Name = "toolsTab";
            toolsTab.Padding = new Padding(4, 3, 4, 3);
            toolsTab.Size = new Size(749, 573);
            toolsTab.TabIndex = 2;
            toolsTab.Text = "Tools";
            toolsTab.UseVisualStyleBackColor = true;
            // 
            // gbDataTools
            // 
            gbDataTools.AutoSize = true;
            gbDataTools.Controls.Add(flowLayoutPanel1);
            gbDataTools.Dock = DockStyle.Top;
            gbDataTools.Location = new Point(4, 3);
            gbDataTools.Name = "gbDataTools";
            gbDataTools.Size = new Size(741, 51);
            gbDataTools.TabIndex = 0;
            gbDataTools.TabStop = false;
            gbDataTools.Text = "Data Tools";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(btnExtractTwtFiles);
            flowLayoutPanel1.Controls.Add(btnExtractPixFiles);
            flowLayoutPanel1.Controls.Add(btnPackTwtFile);
            flowLayoutPanel1.Controls.Add(btnCreatePixFiles);
            flowLayoutPanel1.Controls.Add(btnPackPixFiles);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(3, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(735, 29);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // btnExtractTwtFiles
            // 
            btnExtractTwtFiles.Location = new Point(3, 3);
            btnExtractTwtFiles.Name = "btnExtractTwtFiles";
            btnExtractTwtFiles.Size = new Size(159, 23);
            btnExtractTwtFiles.TabIndex = 0;
            btnExtractTwtFiles.Text = "Extract all TWT Files";
            btnExtractTwtFiles.UseVisualStyleBackColor = true;
            btnExtractTwtFiles.Click += btnExtractTwtFiles_Click;
            // 
            // btnExtractPixFiles
            // 
            btnExtractPixFiles.Location = new Point(168, 3);
            btnExtractPixFiles.Name = "btnExtractPixFiles";
            btnExtractPixFiles.Size = new Size(156, 23);
            btnExtractPixFiles.TabIndex = 1;
            btnExtractPixFiles.Text = "Extract All PIX Files";
            btnExtractPixFiles.UseVisualStyleBackColor = true;
            btnExtractPixFiles.Click += btnExtractPixFiles_Click;
            // 
            // btnPackTwtFile
            // 
            btnPackTwtFile.Location = new Point(330, 3);
            btnPackTwtFile.Name = "btnPackTwtFile";
            btnPackTwtFile.Size = new Size(111, 23);
            btnPackTwtFile.TabIndex = 2;
            btnPackTwtFile.Text = "Pack TWT File";
            btnPackTwtFile.UseVisualStyleBackColor = true;
            btnPackTwtFile.Click += btnPackTwtFile_Click;
            // 
            // btnCreatePixFiles
            // 
            btnCreatePixFiles.Location = new Point(447, 3);
            btnCreatePixFiles.Name = "btnCreatePixFiles";
            btnCreatePixFiles.Size = new Size(119, 23);
            btnCreatePixFiles.TabIndex = 3;
            btnCreatePixFiles.Text = "Create PIX Files";
            btnCreatePixFiles.UseVisualStyleBackColor = true;
            btnCreatePixFiles.Click += btnCreatePixFiles_Click;
            // 
            // btnPackPixFiles
            // 
            btnPackPixFiles.Location = new Point(572, 3);
            btnPackPixFiles.Name = "btnPackPixFiles";
            btnPackPixFiles.Size = new Size(119, 23);
            btnPackPixFiles.TabIndex = 4;
            btnPackPixFiles.Text = "Pack PIX Files";
            btnPackPixFiles.UseVisualStyleBackColor = true;
            btnPackPixFiles.Visible = false;
            btnPackPixFiles.Click += btnPackPixFiles_Click;
            // 
            // ofdFolderBrowser
            // 
            ofdFolderBrowser.AddExtension = false;
            ofdFolderBrowser.FileName = " ";
            ofdFolderBrowser.Filter = "Folders|";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 601);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "ModStockalypse";
            Load += MainForm_Load;
            tabControl1.ResumeLayout(false);
            carsTab.ResumeLayout(false);
            tlpCarPanels.ResumeLayout(false);
            pnlCarButtons.ResumeLayout(false);
            gbCarAvailable.ResumeLayout(false);
            gbCarAvailable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCarAvailable).EndInit();
            gbCarInstalled.ResumeLayout(false);
            gbCarInstalled.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCarInstalled).EndInit();
            racesTab.ResumeLayout(false);
            scRacesPanels.Panel1.ResumeLayout(false);
            scRacesPanels.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scRacesPanels).EndInit();
            scRacesPanels.ResumeLayout(false);
            scRacesRight.Panel1.ResumeLayout(false);
            scRacesRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scRacesRight).EndInit();
            scRacesRight.ResumeLayout(false);
            pnlRacesButtons.ResumeLayout(false);
            toolsTab.ResumeLayout(false);
            toolsTab.PerformLayout();
            gbDataTools.ResumeLayout(false);
            gbDataTools.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage carsTab;
        private TabPage racesTab;
        private TabPage toolsTab;

        private System.Windows.Forms.TableLayoutPanel tlpCarPanels;
        private System.Windows.Forms.Panel pnlCarButtons;
        private System.Windows.Forms.Button btnCarUninstall;
        private System.Windows.Forms.Button btnCarInstall;
        private System.Windows.Forms.GroupBox gbCarAvailable;
        private System.Windows.Forms.GroupBox gbCarInstalled;
        private System.Windows.Forms.ListBox lstCarAvailable;
        private System.Windows.Forms.Button btnCarDown;
        private System.Windows.Forms.ListBox lstCarInstalled;
        private System.Windows.Forms.Button btnCarUp;
        private System.Windows.Forms.PictureBox pbCarInstalled;
        private System.Windows.Forms.TextBox txtCarInstalledDescription;
        private System.Windows.Forms.Label lblCarInstalledDescription;
        private System.Windows.Forms.PictureBox pbCarAvailable;
        private System.Windows.Forms.TextBox txtCarAvailableDescription;
        private System.Windows.Forms.Label lblCarAvailableDescription;
        private System.Windows.Forms.Label lblCarVersion;


        private System.Windows.Forms.SplitContainer scRacesPanels;
        private System.Windows.Forms.ListBox lstRacesRaces;
        private System.Windows.Forms.ListBox lstRacesMods;
        private System.Windows.Forms.Panel pnlRacesButtons;
        private System.Windows.Forms.Button btnRacesInstall;
        private System.Windows.Forms.LinkLabel llToxicRagers;
        private System.Windows.Forms.SplitContainer scRacesRight;
        private GroupBox gbDataTools;
        private Button btnExtractPixFiles;
        private Button btnExtractTwtFiles;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnPackTwtFile;
        private Button btnCreatePixFiles;
        private Button btnPackPixFiles;
        private OpenFileDialog ofdFolderBrowser;
        private FolderBrowserDialog fbdFolderBrowser;
    }
}
