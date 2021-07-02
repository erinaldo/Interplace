using interRegraNegocio.MercadoLivre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{
    public class MercadoLivreEnvioMensagem
    {
        public From from { get; set; }
        public To to { get; set; }
        public string text { get; set; }
        public string[] attachments { get; set; }
    }

    public class FromEnvio
    {
        public string user_id { get; set; }
        public string email { get; set; }
    }

    public class ToEnvio
    {
        public string user_id { get; set; }
    }

}
