using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.B2W
{

    public class B2WInvoiced
    {
        public string status { get; set; }
        public InvoiceInvoiced invoice { get; set; }
    }

    public class InvoiceInvoiced
    {
        public string key { get; set; }
        public string issue_date { get; set; }
        public int volume_qty { get; set; }
    }

  
}
