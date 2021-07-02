using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.JADLOG
{
    public class JADLOGConsultaTrackingcs
    {
        public Consulta[] consulta { get; set; }
    }

    public class Consulta
    {
        public string shipmentId { get; set; }
    }


}
