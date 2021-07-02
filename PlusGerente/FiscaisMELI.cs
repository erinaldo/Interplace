using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusGerente
{

    public class FiscaisMELI
    {
        public long id { get; set; }
        public long shipment_id { get; set; }
        public string fiscal_key { get; set; }
        public float weight { get; set; }
        public string item_title { get; set; }
        public string invoice_serie { get; set; }
        public string invoice_number { get; set; }
        public float invoice_amount { get; set; }
        public object status { get; set; }
        public DateTime invoice_date { get; set; }
        public DateTime date_created { get; set; }
        public DateTime last_updated { get; set; }
        public Additional_Data additional_data { get; set; }
    }

    public class Additional_Data
    {
        public int cfop { get; set; }
        public string company_state_tax_id { get; set; }
    }

}
