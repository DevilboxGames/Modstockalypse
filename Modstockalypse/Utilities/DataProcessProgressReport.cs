using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modstockalypse.Utilities
{
    public struct DataProcessProgressReport
    {
        public int numItemsDone;
        public int numItems;
        public string mainMessage;

        public int numSubItemsDone;
        public int numSubItems;
        public string subMessage;
    }
}
