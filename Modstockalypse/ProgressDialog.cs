using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modstockalypse.Utilities;

namespace Modstockalypse
{
    public partial class ProgressDialog : Form
    {
        public event Action OnCancel;

        public ProgressDialog()
        {
            InitializeComponent();
        }


        public void UpdateProgress(DataProcessProgressReport report)
        {
            lblMainAction.Text = report.mainMessage;
            pbMainProgressBar.Maximum = Math.Max(1, report.numItems);
            pbMainProgressBar.Value = Math.Min(report.numItemsDone, report.numItems);

            lblSubAction.Visible = report.ShowSubProgress;
            pbSubProgressBar.Visible = report.ShowSubProgress;

            if (report.ShowSubProgress)
            {
                lblSubAction.Text = report.subMessage;
                pbSubProgressBar.Maximum = Math.Max(1, report.numSubItems);
                pbSubProgressBar.Value = Math.Min(report.numSubItemsDone, report.numSubItems);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke();
        }
    }
}
