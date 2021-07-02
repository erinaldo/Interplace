using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.JADLOG
{

    public class JADLOGRetornoConsultaTracking
    {
        public ConsultaTracking[] consulta { get; set; }
    }

    public class ConsultaTracking
    {
        public string codigo { get; set; }
        public TrackingTracking tracking { get; set; }
    }

    public class TrackingTracking
    {
        public string codigo { get; set; }
        public string shipmentId { get; set; }
        public string dacte { get; set; }
        public string dtEmissao { get; set; }
        public string status { get; set; }
        public float valor { get; set; }
        public float peso { get; set; }
        public EventoTracking[] eventos { get; set; }
        public VolumeTracking[] volumes { get; set; }
    }

    public class EventoTracking
    {
        public string data { get; set; }
        public string status { get; set; }
        public string unidade { get; set; }
    }

    public class VolumeTracking
    {
        public float peso { get; set; }
        public int altura { get; set; }
        public int largura { get; set; }
        public int comprimento { get; set; }
    }

}
