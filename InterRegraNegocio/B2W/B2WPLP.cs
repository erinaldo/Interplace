using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.B2W
{
    public class B2WPLP
    {
        public Plp[] plp { get; set; }
    }

    public class Plp
    {
        public int id { get; set; }
        public string expiration_date { get; set; }
        public bool printed { get; set; }
        public string type { get; set; }
        public Order[] orders { get; set; }
    }

    public class Order
    {
        public string code { get; set; }
        public string customer { get; set; }
        public float value { get; set; }
        public string warehouse_id { get; set; }
    }



}
