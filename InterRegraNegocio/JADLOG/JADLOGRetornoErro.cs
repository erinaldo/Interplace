using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.JADLOG
{
    public class JADLOGRetornoErro
    {
        public Erro erro { get; set; }
        public string status { get; set; }
    }

    public class Erro
    {
        public string descricao { get; set; }
        public int id { get; set; }
    }

}
