using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.GNRE
{
    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.gnre.pe.gov.br", IsNullable = false)]
    public partial class TLote_GNRE
    {

        private TLote_GNREGuias guiasField;

        private string[] textField;

        private decimal versaoField;

        /// <remarks/>
        public TLote_GNREGuias guias
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
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
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
    public partial class TLote_GNREGuias
    {

        private TLote_GNREGuiasTDadosGNRE tDadosGNREField;

        private string[] textField;

        /// <remarks/>
        public TLote_GNREGuiasTDadosGNRE TDadosGNRE
        {
            get
            {
                return this.tDadosGNREField;
            }
            set
            {
                this.tDadosGNREField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNREGuiasTDadosGNRE
    {

        private object ufFavorecidaField;

        private object tipoGnreField;

        private TLote_GNREGuiasTDadosGNREContribuinteEmitente contribuinteEmitenteField;

        private TLote_GNREGuiasTDadosGNREItensGNRE itensGNREField;

        private object valorGNREField;

        private decimal versaoField;

        /// <remarks/>
        public object ufFavorecida
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
        public object tipoGnre
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
        public TLote_GNREGuiasTDadosGNREContribuinteEmitente contribuinteEmitente
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
        public TLote_GNREGuiasTDadosGNREItensGNRE itensGNRE
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
        public object valorGNRE
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
    public partial class TLote_GNREGuiasTDadosGNREContribuinteEmitente
    {

        private TLote_GNREGuiasTDadosGNREContribuinteEmitenteIdentificacao identificacaoField;

        private object razaoSocialField;

        private object enderecoField;

        private object municipioField;

        private object ufField;

        private object cepField;

        private object telefoneField;

        /// <remarks/>
        public TLote_GNREGuiasTDadosGNREContribuinteEmitenteIdentificacao identificacao
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
        public object razaoSocial
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
        public object endereco
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
        public object municipio
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
        public object uf
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
        public object cep
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
        public object telefone
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
    public partial class TLote_GNREGuiasTDadosGNREContribuinteEmitenteIdentificacao
    {

        private object cPFField;

        private object cNPJField;

        private object ieField;

        /// <remarks/>
        public object CPF
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

        /// <remarks/>
        public object CNPJ
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

        /// <remarks/>
        public object IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNREGuiasTDadosGNREItensGNRE
    {

        private TLote_GNREGuiasTDadosGNREItensGNREItem itemField;

        /// <remarks/>
        public TLote_GNREGuiasTDadosGNREItensGNREItem item
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
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItem
    {

        private object receitaField;

        private object detalhamentoReceitaField;

        private TLote_GNREGuiasTDadosGNREItensGNREItemDocumentoOrigem documentoOrigemField;

        private object produtoField;

        private TLote_GNREGuiasTDadosGNREItensGNREItemReferencia referenciaField;

        private object dataVencimentoField;

        private TLote_GNREGuiasTDadosGNREItensGNREItemValor valorField;

        private object convenioField;

        private TLote_GNREGuiasTDadosGNREItensGNREItemContribuinteDestinatario contribuinteDestinatarioField;

        private TLote_GNREGuiasTDadosGNREItensGNREItemCampoExtra[] camposExtrasField;

        /// <remarks/>
        public object receita
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
        public object detalhamentoReceita
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
        public TLote_GNREGuiasTDadosGNREItensGNREItemDocumentoOrigem documentoOrigem
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
        public object produto
        {
            get
            {
                return this.produtoField;
            }
            set
            {
                this.produtoField = value;
            }
        }

        /// <remarks/>
        public TLote_GNREGuiasTDadosGNREItensGNREItemReferencia referencia
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
        public object dataVencimento
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
        public TLote_GNREGuiasTDadosGNREItensGNREItemValor valor
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
        public object convenio
        {
            get
            {
                return this.convenioField;
            }
            set
            {
                this.convenioField = value;
            }
        }

        /// <remarks/>
        public TLote_GNREGuiasTDadosGNREItensGNREItemContribuinteDestinatario contribuinteDestinatario
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
        [System.Xml.Serialization.XmlArrayItemAttribute("campoExtra", IsNullable = false)]
        public TLote_GNREGuiasTDadosGNREItensGNREItemCampoExtra[] camposExtras
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
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItemDocumentoOrigem
    {

        private string tipoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tipo
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItemReferencia
    {

        private object periodoField;

        private object mesField;

        private object anoField;

        private object parcelaField;

        /// <remarks/>
        public object periodo
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
        public object mes
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
        public object ano
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
        public object parcela
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
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItemValor
    {

        private string tipoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tipo
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItemContribuinteDestinatario
    {

        private TLote_GNREGuiasTDadosGNREItensGNREItemContribuinteDestinatarioIdentificacao identificacaoField;

        private object razaoSocialField;

        private object municipioField;

        /// <remarks/>
        public TLote_GNREGuiasTDadosGNREItensGNREItemContribuinteDestinatarioIdentificacao identificacao
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
        public object razaoSocial
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
        public object municipio
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
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItemContribuinteDestinatarioIdentificacao
    {

        private object cPFField;

        private object cNPJField;

        private object ieField;

        /// <remarks/>
        public object CPF
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

        /// <remarks/>
        public object CNPJ
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

        /// <remarks/>
        public object IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public partial class TLote_GNREGuiasTDadosGNREItensGNREItemCampoExtra
    {

        private object codigoField;

        private object valorField;

        /// <remarks/>
        public object codigo
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
        public object valor
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
