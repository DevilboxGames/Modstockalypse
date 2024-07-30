using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modstockalypse.Utilities
{
    public class DataProcessProgressReport
    {
        public int numItemsDone;
        public int numItems;
        public string mainMessage;

        public bool ShowSubProgress = true;
        public int numSubItemsDone;
        public int numSubItems;
        public string subMessage;
    }
}
