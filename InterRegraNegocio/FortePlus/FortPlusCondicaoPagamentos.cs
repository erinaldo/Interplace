using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{

    public class FortPlusCondicaoPagamentos
    {
        public FortPlusCondicaoPagamento[] Property1 { get; set; }
    }

    public class FortPlusCondicaoPagamento
    {
        public int? id { get; set; }
        public string cpCodigo { get; set; }
        public string cpNome { get; set; }
        public int? cpQtdeParcela { get; set; }
        public string cpQtdeIntervaloDia { get; set; }
        public int? cpQtdeAcrescimo { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }

}
