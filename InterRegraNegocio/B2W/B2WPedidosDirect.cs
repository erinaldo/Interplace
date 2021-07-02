using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.B2W
{


    public class B2WPedidosDirect
    {
        public B2WPedidoDirect[] orders { get; set; }
    }

    public class B2WPedidoDirect
    {
        public string code { get; set; }
        public string customer { get; set; }
        public float value { get; set; }
    }

}
