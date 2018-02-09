using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Externalisation
{
    class DataGridLigne
    {
        private DataGridViewRow _dtgvr = new DataGridViewRow();
         public DataGridViewRow dtgvr
         {
             get {return _dtgvr;}
             set {_dtgvr = dtgvr;}
         }

    }
}
