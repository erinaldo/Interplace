using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{

    public class ParametroFortPlus
    {
        public int id { get; set; }
        public string stChave { get; set; }
        public string stConteudo { get; set; }
        public string stDescricao { get; set; }
        public object idFilial { get; set; }
        public int idIncluidoPor { get; set; }
        public object idAltaradoPor { get; set; }
        public DateTime dmaInclusao { get; set; }
        public object dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }

}
