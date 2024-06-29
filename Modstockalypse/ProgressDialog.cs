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
            lblSubAction.Text = report.subMessage;
            pbMainProgressBar.Maximum = Math.Max(1, report.numItems);
            pbMainProgressBar.Value = report.numItemsDone;
            pbSubProgressBar.Maximum = Math.Max(1, report.numSubItems);
            pbSubProgressBar.Value = report.numSubItemsDone;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke();
        }
    }
}
