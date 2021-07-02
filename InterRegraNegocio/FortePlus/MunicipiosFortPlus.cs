using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{
    public class MunicipiosFortPlus
    {
        public MunicipioFortPlus[] Property1 { get; set; }
    }
    public class MunicipioFortPlus
    {
        public int id { get; set; }
        public string mnCodigo { get; set; }
        public string mnNome { get; set; }
        public string mnUf { get; set; }
        public string mnCodigoMunicipio { get; set; }
        public string mnCodigoUf { get; set; }
        public object idFilial { get; set; }
        public int idIncluidoPor { get; set; }
        public object idAltaradoPor { get; set; }
        public DateTime dmaInclusao { get; set; }
        public object dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }

}
