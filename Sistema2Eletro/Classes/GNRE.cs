using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema2Eletro.Classes
{
    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.gnre.pe.gov.br", IsNullable = false)]
    public partial class TLote_GNRE
    {

        private TLote_GNRETDadosGNRE[] guiasField;

        private decimal versaoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TDadosGNRE", IsNullable = false)]
        public TLote_GNRETDadosGNRE[] guias
        {
            get
            {
                return this.guiasField;
            }
            set
            {
                this.guiasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNRE
    {

        private string ufFavorecidaField;

        private byte tipoGnreField;

        private TLote_GNRETDadosGNREContribuinteEmitente contribuinteEmitenteField;

        private TLote_GNRETDadosGNREItensGNRE itensGNREField;

        private decimal valorGNREField;

        private System.DateTime dataPagamentoField;

        private decimal versaoField;

        /// <remarks/>
        public string ufFavorecida
        {
            get
            {
                return this.ufFavorecidaField;
            }
            set
            {
                this.ufFavorecidaField = value;
            }
        }

        /// <remarks/>
        public byte tipoGnre
        {
            get
            {
                return this.tipoGnreField;
            }
            set
            {
                this.tipoGnreField = value;
            }
        }

        /// <remarks/>
        public TLote_GNRETDadosGNREContribuinteEmitente contribuinteEmitente
        {
            get
            {
                return this.contribuinteEmitenteField;
            }
            set
            {
                this.contribuinteEmitenteField = value;
            }
        }

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNRE itensGNRE
        {
            get
            {
                return this.itensGNREField;
            }
            set
            {
                this.itensGNREField = value;
            }
        }

        /// <remarks/>
        public decimal valorGNRE
        {
            get
            {
                return this.valorGNREField;
            }
            set
            {
                this.valorGNREField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dataPagamento
        {
            get
            {
                return this.dataPagamentoField;
            }
            set
            {
                this.dataPagamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREContribuinteEmitente
    {

        private TLote_GNRETDadosGNREContribuinteEmitenteIdentificacao identificacaoField;

        private string razaoSocialField;

        private string enderecoField;

        private ushort municipioField;

        private string ufField;

        private uint cepField;

        private ulong telefoneField;

        /// <remarks/>
        public TLote_GNRETDadosGNREContribuinteEmitenteIdentificacao identificacao
        {
            get
            {
                return this.identificacaoField;
            }
            set
            {
                this.identificacaoField = value;
            }
        }

        /// <remarks/>
        public string razaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public string endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public ushort municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }

        /// <remarks/>
        public string uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public uint cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        public ulong telefone
        {
            get
            {
                return this.telefoneField;
            }
            set
            {
                this.telefoneField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREContribuinteEmitenteIdentificacao
    {

        private ulong cNPJField;

        /// <remarks/>
        public ulong CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNRE
    {

        private TLote_GNRETDadosGNREItensGNREItem itemField;

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItem item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItem
    {

        private uint receitaField;

        private byte detalhamentoReceitaField;

        private bool detalhamentoReceitaFieldSpecified;

        private TLote_GNRETDadosGNREItensGNREItemDocumentoOrigem documentoOrigemField;

        private TLote_GNRETDadosGNREItensGNREItemReferencia referenciaField;

        private System.DateTime dataVencimentoField;

        private TLote_GNRETDadosGNREItensGNREItemValor[] valorField;

        private TLote_GNRETDadosGNREItensGNREItemContribuinteDestinatario contribuinteDestinatarioField;

        private TLote_GNRETDadosGNREItensGNREItemCamposExtras camposExtrasField;

        /// <remarks/>
        public uint receita
        {
            get
            {
                return this.receitaField;
            }
            set
            {
                this.receitaField = value;
            }
        }

        /// <remarks/>
        public byte detalhamentoReceita
        {
            get
            {
                return this.detalhamentoReceitaField;
            }
            set
            {
                this.detalhamentoReceitaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool detalhamentoReceitaSpecified
        {
            get
            {
                return this.detalhamentoReceitaFieldSpecified;
            }
            set
            {
                this.detalhamentoReceitaFieldSpecified = value;
            }
        }

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItemDocumentoOrigem documentoOrigem
        {
            get
            {
                return this.documentoOrigemField;
            }
            set
            {
                this.documentoOrigemField = value;
            }
        }

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItemReferencia referencia
        {
            get
            {
                return this.referenciaField;
            }
            set
            {
                this.referenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dataVencimento
        {
            get
            {
                return this.dataVencimentoField;
            }
            set
            {
                this.dataVencimentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("valor")]
        public TLote_GNRETDadosGNREItensGNREItemValor[] valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItemContribuinteDestinatario contribuinteDestinatario
        {
            get
            {
                return this.contribuinteDestinatarioField;
            }
            set
            {
                this.contribuinteDestinatarioField = value;
            }
        }

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItemCamposExtras camposExtras
        {
            get
            {
                return this.camposExtrasField;
            }
            set
            {
                this.camposExtrasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemDocumentoOrigem
    {

        private byte tipoField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "integer")]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemReferencia
    {

        private byte periodoField;

        private byte mesField;

        private ushort anoField;

        private byte parcelaField;

        /// <remarks/>
        public byte periodo
        {
            get
            {
                return this.periodoField;
            }
            set
            {
                this.periodoField = value;
            }
        }

        /// <remarks/>
        public byte mes
        {
            get
            {
                return this.mesField;
            }
            set
            {
                this.mesField = value;
            }
        }

        /// <remarks/>
        public ushort ano
        {
            get
            {
                return this.anoField;
            }
            set
            {
                this.anoField = value;
            }
        }

        /// <remarks/>
        public byte parcela
        {
            get
            {
                return this.parcelaField;
            }
            set
            {
                this.parcelaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemValor
    {

        private byte tipoField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemContribuinteDestinatario
    {

        private TLote_GNRETDadosGNREItensGNREItemContribuinteDestinatarioIdentificacao identificacaoField;

        private string razaoSocialField;

        private ushort municipioField;

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItemContribuinteDestinatarioIdentificacao identificacao
        {
            get
            {
                return this.identificacaoField;
            }
            set
            {
                this.identificacaoField = value;
            }
        }

        /// <remarks/>
        public string razaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public ushort municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemContribuinteDestinatarioIdentificacao
    {

        private ulong cPFField;

        /// <remarks/>
        public ulong CPF
        {
            get
            {
                return this.cPFField;
            }
            set
            {
                this.cPFField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemCamposExtras
    {

        private TLote_GNRETDadosGNREItensGNREItemCamposExtrasCampoExtra campoExtraField;

        /// <remarks/>
        public TLote_GNRETDadosGNREItensGNREItemCamposExtrasCampoExtra campoExtra
        {
            get
            {
                return this.campoExtraField;
            }
            set
            {
                this.campoExtraField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNRETDadosGNREItensGNREItemCamposExtrasCampoExtra
    {

        private byte codigoField;

        private string valorField;

        /// <remarks/>
        public byte codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }
    }




}
