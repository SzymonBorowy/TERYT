using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using terytDLL;

namespace terytWS
{
    public partial class UslugaPobierajacaPlikiTERYT : ServiceBase
    {
        public UslugaPobierajacaPlikiTERYT()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            teryt_DLL teryt = new teryt_DLL();
            teryt.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
