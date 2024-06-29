namespace Modstockalypse
{
    partial class ProgressDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMainAction = new Label();
            pbMainProgressBar = new ProgressBar();
            lblSubAction = new Label();
            pbSubProgressBar = new ProgressBar();
            btnCancel = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblMainAction
            // 
            lblMainAction.AutoSize = true;
            lblMainAction.Location = new Point(3, 0);
            lblMainAction.Name = "lblMainAction";
            lblMainAction.Size = new Size(105, 15);
            lblMainAction.TabIndex = 0;
            lblMainAction.Text = "Performing Action";
            // 
            // pbMainProgressBar
            // 
            pbMainProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbMainProgressBar.Location = new Point(3, 17);
            pbMainProgressBar.Margin = new Padding(3, 2, 3, 2);
            pbMainProgressBar.Name = "pbMainProgressBar";
            pbMainProgressBar.Size = new Size(687, 22);
            pbMainProgressBar.Step = 1;
            pbMainProgressBar.TabIndex = 1;
            // 
            // lblSubAction
            // 
            lblSubAction.AutoSize = true;
            lblSubAction.Location = new Point(3, 41);
            lblSubAction.Name = "lblSubAction";
            lblSubAction.Size = new Size(79, 15);
            lblSubAction.TabIndex = 2;
            lblSubAction.Text = "Action details";
            // 
            // pbSubProgressBar
            // 
            pbSubProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbSubProgressBar.Location = new Point(3, 58);
            pbSubProgressBar.Margin = new Padding(3, 2, 3, 2);
            pbSubProgressBar.Name = "pbSubProgressBar";
            pbSubProgressBar.Size = new Size(687, 22);
            pbSubProgressBar.Step = 1;
            pbSubProgressBar.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(607, 93);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.RightToLeft = RightToLeft.No;
            btnCancel.Size = new Size(82, 22);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblMainAction);
            flowLayoutPanel1.Controls.Add(pbMainProgressBar);
            flowLayoutPanel1.Controls.Add(lblSubAction);
            flowLayoutPanel1.Controls.Add(pbSubProgressBar);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(700, 88);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // ProgressDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(700, 124);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(btnCancel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ProgressDialog";
            Text = "ProgressDialog";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblMainAction;
        private ProgressBar pbMainProgressBar;
        private Label lblSubAction;
        private ProgressBar pbSubProgressBar;
        private Button btnCancel;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}