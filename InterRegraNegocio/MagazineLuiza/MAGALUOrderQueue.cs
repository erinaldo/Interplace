using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MagazineLuiza
{

    public class MAGALUOrderQueue
    {
        public int Total { get; set; }
        public Orderqueue[] OrderQueues { get; set; }
    }

    public class Orderqueue
    {
        public int Id { get; set; }
        public string IdOrder { get; set; }
        public string IdOrderMarketplace { get; set; }
        public DateTime InsertedDate { get; set; }
        public string OrderStatus { get; set; }
    }


    public class RetornoPedidos
    {
        public RetornoPedido[] Property1 { get; set; }
    }

    public class RetornoPedido
    {
        public string Id { get; set; }
    }


}
