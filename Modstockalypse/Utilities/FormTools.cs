using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modstockalypse.Utilities
{
    internal static class FormTools
    {
        public static void DoWorkWithProgressDialog(string title, string successMessage, Form owner, Func<BackgroundWorker, Action<DataProcessProgressReport>, DoWorkEventHandler> workHandlerFactory)
        {
            ProgressDialog progressDialog = new ProgressDialog();
            progressDialog.Name = "progressDialog";
            progressDialog.Text = title;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += (sender, evnt) =>
                progressDialog.UpdateProgress((DataProcessProgressReport)evnt.UserState);
            progressDialog.OnCancel += () =>
            {
                worker.CancelAsync();
                progressDialog.Close();
            };

            Action<DataProcessProgressReport> progressReporter = report =>
            {
                worker.ReportProgress(report.numItemsDone / Math.Max(1, report.numItems), report);
            };

            worker.DoWork += workHandlerFactory(worker, progressReporter);
            worker.RunWorkerCompleted += (sender, evnt) =>
            {
                if (evnt.Cancelled)
                {
                    return;
                }
                MessageBox.Show(owner, successMessage);
                progressDialog.Close();
            };
            worker.RunWorkerAsync();
            progressDialog.ShowDialog(owner);
        }
    }
}
