using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{


    public class GlobaisFortPlus
    {
        public GlobalFortPlus[] Property1 { get; set; }
    }

    public class GlobalFortPlus
    {
        public int id { get; set; }
        public int? glIdPai { get; set; }
        public string glCodigo { get; set; }
        public string glNome { get; set; }
        public object glIdExterno { get; set; }
        public object idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public object idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public object dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public string glPermiteAlteracao { get; set; }
        public object glUpdateEcommerce { get; set; }
    }

}
