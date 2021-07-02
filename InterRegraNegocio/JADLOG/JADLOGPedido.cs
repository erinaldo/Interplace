using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio
{
    public class JADLOGPedido
    {
        public string conteudo { get; set; }
        public string[] pedido { get; set; }
        public int totPeso { get; set; }
        public float totValor { get; set; }
        public string obs { get; set; }
        public int modalidade { get; set; }
        public string contaCorrente { get; set; }
        public string tpColeta { get; set; }
        public int tipoFrete { get; set; }
        public string cdUnidadeOri { get; set; }
        public object cdUnidadeDes { get; set; }
        public object cdPickupOri { get; set; }
        public string cdPickupDes { get; set; }
        public int nrContrato { get; set; }
        public int servico { get; set; }
        public object shipmentId { get; set; }
        public object vlColeta { get; set; }
        public Rem rem { get; set; }
        public Des des { get; set; }
        public Dfe[] dfe { get; set; }
        public Volume[] volume { get; set; }
    }

    public class Rem
    {
        public string nome { get; set; }
        public string cnpjCpf { get; set; }
        public object ie { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public object compl { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string fone { get; set; }
        public string cel { get; set; }
        public string email { get; set; }
        public string contato { get; set; }
    }

    public class Des
    {
        public string nome { get; set; }
        public string cnpjCpf { get; set; }
        public object ie { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public object compl { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string fone { get; set; }
        public string cel { get; set; }
        public string email { get; set; }
        public string contato { get; set; }
    }

    public class Dfe
    {
        public string cfop { get; set; }
        public string danfeCte { get; set; }
        public string nrDoc { get; set; }
        public string serie { get; set; }
        public int tpDocumento { get; set; }
        public float valor { get; set; }
    }

    public class Volume
    {
        public int altura { get; set; }
        public int comprimento { get; set; }
        public string identificador { get; set; }
        public int largura { get; set; }
        public float peso { get; set; }
    }



}
