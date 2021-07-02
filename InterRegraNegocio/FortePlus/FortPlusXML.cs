using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{

    public class FortPlusXMLs
    {
        public FortPlusXML[] Property1 { get; set; }
    }

    public class FortPlusXML
    {
        public int? id { get; set; }
        public string trTipo { get; set; }
        public string trStatus { get; set; }
        public int? trIdMovto { get; set; }
        public int? trIdCliente { get; set; }
        public string trArquivo { get; set; }
        public string trArquivoRetorno { get; set; }
        public string trUrlWebService { get; set; }
        public string trUrlChave { get; set; }
        public string trQrCode { get; set; }
        public int? trDocto { get; set; }
        public string trCliente { get; set; }
        public string trVersao { get; set; }
        public string trAmbiente { get; set; }
        public string trTipoEmissao { get; set; }
        public string trCstat { get; set; }
        public string trXmot { get; set; }
        public string trProtocolo { get; set; }
        public DateTime? trDmaProtocolo { get; set; }
        public DateTime? trDmaEmissao { get; set; }
        public string trChaveAcesso { get; set; }
        public string trEntidade { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }

}
