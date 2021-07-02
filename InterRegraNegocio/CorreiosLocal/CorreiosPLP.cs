using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.CorreiosLocal
{





    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class correioslog
    {

        private string tipo_arquivoField;

        private decimal versao_arquivoField;

        private correioslogPlp plpField;

        private correioslogRemetente remetenteField;

        private object forma_pagamentoField;

        private correioslogObjeto_postal[] objeto_postalField;

        /// <remarks/>
        public string tipo_arquivo
        {
            get
            {
                return this.tipo_arquivoField;
            }
            set
            {
                this.tipo_arquivoField = value;
            }
        }

        /// <remarks/>
        public decimal versao_arquivo
        {
            get
            {
                return this.versao_arquivoField;
            }
            set
            {
                this.versao_arquivoField = value;
            }
        }

        /// <remarks/>
        public correioslogPlp plp
        {
            get
            {
                return this.plpField;
            }
            set
            {
                this.plpField = value;
            }
        }

        /// <remarks/>
        public correioslogRemetente remetente
        {
            get
            {
                return this.remetenteField;
            }
            set
            {
                this.remetenteField = value;
            }
        }

        /// <remarks/>
        public object forma_pagamento
        {
            get
            {
                return this.forma_pagamentoField;
            }
            set
            {
                this.forma_pagamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("objeto_postal")]
        public correioslogObjeto_postal[] objeto_postal
        {
            get
            {
                return this.objeto_postalField;
            }
            set
            {
                this.objeto_postalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogPlp
    {

        private object id_plpField;

        private object valor_globalField;

        private object mcu_unidade_postagemField;

        private object nome_unidade_postagemField;

        private uint cartao_postagemField;

        /// <remarks/>
        public object id_plp
        {
            get
            {
                return this.id_plpField;
            }
            set
            {
                this.id_plpField = value;
            }
        }

        /// <remarks/>
        public object valor_global
        {
            get
            {
                return this.valor_globalField;
            }
            set
            {
                this.valor_globalField = value;
            }
        }

        /// <remarks/>
        public object mcu_unidade_postagem
        {
            get
            {
                return this.mcu_unidade_postagemField;
            }
            set
            {
                this.mcu_unidade_postagemField = value;
            }
        }

        /// <remarks/>
        public object nome_unidade_postagem
        {
            get
            {
                return this.nome_unidade_postagemField;
            }
            set
            {
                this.nome_unidade_postagemField = value;
            }
        }

        /// <remarks/>
        public uint cartao_postagem
        {
            get
            {
                return this.cartao_postagemField;
            }
            set
            {
                this.cartao_postagemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogRemetente
    {

        private ulong numero_contratoField;

        private byte numero_diretoriaField;

        private uint codigo_administrativoField;

        private string nome_remetenteField;

        private string logradouro_remetenteField;

        private decimal numero_remetenteField;

        private string complemento_remetenteField;

        private string bairro_remetenteField;

        private uint cep_remetenteField;

        private string cidade_remetenteField;

        private string uf_remetenteField;

        private string telefone_remetenteField;

        private string fax_remetenteField;

        private string email_remetenteField;

        private string celular_remetenteField;

        private ulong cpf_cnpj_remetenteField;

        private string ciencia_conteudo_proibidoField;

        /// <remarks/>
        public ulong numero_contrato
        {
            get
            {
                return this.numero_contratoField;
            }
            set
            {
                this.numero_contratoField = value;
            }
        }

        /// <remarks/>
        public byte numero_diretoria
        {
            get
            {
                return this.numero_diretoriaField;
            }
            set
            {
                this.numero_diretoriaField = value;
            }
        }

        /// <remarks/>
        public uint codigo_administrativo
        {
            get
            {
                return this.codigo_administrativoField;
            }
            set
            {
                this.codigo_administrativoField = value;
            }
        }

        /// <remarks/>
        public string nome_remetente
        {
            get
            {
                return this.nome_remetenteField;
            }
            set
            {
                this.nome_remetenteField = value;
            }
        }

        /// <remarks/>
        public string logradouro_remetente
        {
            get
            {
                return this.logradouro_remetenteField;
            }
            set
            {
                this.logradouro_remetenteField = value;
            }
        }

        /// <remarks/>
        public decimal numero_remetente
        {
            get
            {
                return this.numero_remetenteField;
            }
            set
            {
                this.numero_remetenteField = value;
            }
        }

        /// <remarks/>
        public string complemento_remetente
        {
            get
            {
                return this.complemento_remetenteField;
            }
            set
            {
                this.complemento_remetenteField = value;
            }
        }

        /// <remarks/>
        public string bairro_remetente
        {
            get
            {
                return this.bairro_remetenteField;
            }
            set
            {
                this.bairro_remetenteField = value;
            }
        }

        /// <remarks/>
        public uint cep_remetente
        {
            get
            {
                return this.cep_remetenteField;
            }
            set
            {
                this.cep_remetenteField = value;
            }
        }

        /// <remarks/>
        public string cidade_remetente
        {
            get
            {
                return this.cidade_remetenteField;
            }
            set
            {
                this.cidade_remetenteField = value;
            }
        }

        /// <remarks/>
        public string uf_remetente
        {
            get
            {
                return this.uf_remetenteField;
            }
            set
            {
                this.uf_remetenteField = value;
            }
        }

        /// <remarks/>
        public string telefone_remetente
        {
            get
            {
                return this.telefone_remetenteField;
            }
            set
            {
                this.telefone_remetenteField = value;
            }
        }

        /// <remarks/>
        public string fax_remetente
        {
            get
            {
                return this.fax_remetenteField;
            }
            set
            {
                this.fax_remetenteField = value;
            }
        }

        /// <remarks/>
        public string email_remetente
        {
            get
            {
                return this.email_remetenteField;
            }
            set
            {
                this.email_remetenteField = value;
            }
        }

        /// <remarks/>
        public string celular_remetente
        {
            get
            {
                return this.celular_remetenteField;
            }
            set
            {
                this.celular_remetenteField = value;
            }
        }

        /// <remarks/>
        public ulong cpf_cnpj_remetente
        {
            get
            {
                return this.cpf_cnpj_remetenteField;
            }
            set
            {
                this.cpf_cnpj_remetenteField = value;
            }
        }

        /// <remarks/>
        public string ciencia_conteudo_proibido
        {
            get
            {
                return this.ciencia_conteudo_proibidoField;
            }
            set
            {
                this.ciencia_conteudo_proibidoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogObjeto_postal
    {

        private string numero_etiquetaField;

        private object codigo_objeto_clienteField;

        private ushort codigo_servico_postagemField;

        private string cubagemField;

        private ushort pesoField;

        private object rt1Field;

        private object rt2Field;

        private string restricao_anacField;

        private correioslogObjeto_postalDestinatario destinatarioField;

        private correioslogObjeto_postalNacional nacionalField;

        private correioslogObjeto_postalServico_adicional servico_adicionalField;

        private correioslogObjeto_postalDimensao_objeto dimensao_objetoField;

        private object data_postagem_saraField;

        private byte status_processamentoField;

        private object numero_comprovante_postagemField;

        private object valor_cobradoField;

        /// <remarks/>
        public string numero_etiqueta
        {
            get
            {
                return this.numero_etiquetaField;
            }
            set
            {
                this.numero_etiquetaField = value;
            }
        }

        /// <remarks/>
        public object codigo_objeto_cliente
        {
            get
            {
                return this.codigo_objeto_clienteField;
            }
            set
            {
                this.codigo_objeto_clienteField = value;
            }
        }

        /// <remarks/>
        public ushort codigo_servico_postagem
        {
            get
            {
                return this.codigo_servico_postagemField;
            }
            set
            {
                this.codigo_servico_postagemField = value;
            }
        }

        /// <remarks/>
        public string cubagem
        {
            get
            {
                return this.cubagemField;
            }
            set
            {
                this.cubagemField = value;
            }
        }

        /// <remarks/>
        public ushort peso
        {
            get
            {
                return this.pesoField;
            }
            set
            {
                this.pesoField = value;
            }
        }

        /// <remarks/>
        public object rt1
        {
            get
            {
                return this.rt1Field;
            }
            set
            {
                this.rt1Field = value;
            }
        }

        /// <remarks/>
        public object rt2
        {
            get
            {
                return this.rt2Field;
            }
            set
            {
                this.rt2Field = value;
            }
        }

        /// <remarks/>
        public string restricao_anac
        {
            get
            {
                return this.restricao_anacField;
            }
            set
            {
                this.restricao_anacField = value;
            }
        }

        /// <remarks/>
        public correioslogObjeto_postalDestinatario destinatario
        {
            get
            {
                return this.destinatarioField;
            }
            set
            {
                this.destinatarioField = value;
            }
        }

        /// <remarks/>
        public correioslogObjeto_postalNacional nacional
        {
            get
            {
                return this.nacionalField;
            }
            set
            {
                this.nacionalField = value;
            }
        }

        /// <remarks/>
        public correioslogObjeto_postalServico_adicional servico_adicional
        {
            get
            {
                return this.servico_adicionalField;
            }
            set
            {
                this.servico_adicionalField = value;
            }
        }

        /// <remarks/>
        public correioslogObjeto_postalDimensao_objeto dimensao_objeto
        {
            get
            {
                return this.dimensao_objetoField;
            }
            set
            {
                this.dimensao_objetoField = value;
            }
        }

        /// <remarks/>
        public object data_postagem_sara
        {
            get
            {
                return this.data_postagem_saraField;
            }
            set
            {
                this.data_postagem_saraField = value;
            }
        }

        /// <remarks/>
        public byte status_processamento
        {
            get
            {
                return this.status_processamentoField;
            }
            set
            {
                this.status_processamentoField = value;
            }
        }

        /// <remarks/>
        public object numero_comprovante_postagem
        {
            get
            {
                return this.numero_comprovante_postagemField;
            }
            set
            {
                this.numero_comprovante_postagemField = value;
            }
        }

        /// <remarks/>
        public object valor_cobrado
        {
            get
            {
                return this.valor_cobradoField;
            }
            set
            {
                this.valor_cobradoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogObjeto_postalDestinatario
    {

        private string nome_destinatarioField;

        private string telefone_destinatarioField;

        private string celular_destinatarioField;

        private string email_destinatarioField;

        private string logradouro_destinatarioField;

        private string complemento_destinatarioField;

        private ushort numero_end_destinatarioField;

        /// <remarks/>
        public string nome_destinatario
        {
            get
            {
                return this.nome_destinatarioField;
            }
            set
            {
                this.nome_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string telefone_destinatario
        {
            get
            {
                return this.telefone_destinatarioField;
            }
            set
            {
                this.telefone_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string celular_destinatario
        {
            get
            {
                return this.celular_destinatarioField;
            }
            set
            {
                this.celular_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string email_destinatario
        {
            get
            {
                return this.email_destinatarioField;
            }
            set
            {
                this.email_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string logradouro_destinatario
        {
            get
            {
                return this.logradouro_destinatarioField;
            }
            set
            {
                this.logradouro_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string complemento_destinatario
        {
            get
            {
                return this.complemento_destinatarioField;
            }
            set
            {
                this.complemento_destinatarioField = value;
            }
        }

        /// <remarks/>
        public ushort numero_end_destinatario
        {
            get
            {
                return this.numero_end_destinatarioField;
            }
            set
            {
                this.numero_end_destinatarioField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogObjeto_postalNacional
    {

        private string bairro_destinatarioField;

        private string cidade_destinatarioField;

        private string uf_destinatarioField;

        private uint cep_destinatarioField;

        private object codigo_usuario_postalField;

        private object centro_custo_clienteField;

        private ushort numero_nota_fiscalField;

        private byte serie_nota_fiscalField;

        private string valor_nota_fiscalField;

        private object natureza_nota_fiscalField;

        private object descricao_objetoField;

        private string valor_a_cobrarField;

        /// <remarks/>
        public string bairro_destinatario
        {
            get
            {
                return this.bairro_destinatarioField;
            }
            set
            {
                this.bairro_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string cidade_destinatario
        {
            get
            {
                return this.cidade_destinatarioField;
            }
            set
            {
                this.cidade_destinatarioField = value;
            }
        }

        /// <remarks/>
        public string uf_destinatario
        {
            get
            {
                return this.uf_destinatarioField;
            }
            set
            {
                this.uf_destinatarioField = value;
            }
        }

        /// <remarks/>
        public uint cep_destinatario
        {
            get
            {
                return this.cep_destinatarioField;
            }
            set
            {
                this.cep_destinatarioField = value;
            }
        }

        /// <remarks/>
        public object codigo_usuario_postal
        {
            get
            {
                return this.codigo_usuario_postalField;
            }
            set
            {
                this.codigo_usuario_postalField = value;
            }
        }

        /// <remarks/>
        public object centro_custo_cliente
        {
            get
            {
                return this.centro_custo_clienteField;
            }
            set
            {
                this.centro_custo_clienteField = value;
            }
        }

        /// <remarks/>
        public ushort numero_nota_fiscal
        {
            get
            {
                return this.numero_nota_fiscalField;
            }
            set
            {
                this.numero_nota_fiscalField = value;
            }
        }

        /// <remarks/>
        public byte serie_nota_fiscal
        {
            get
            {
                return this.serie_nota_fiscalField;
            }
            set
            {
                this.serie_nota_fiscalField = value;
            }
        }

        /// <remarks/>
        public string valor_nota_fiscal
        {
            get
            {
                return this.valor_nota_fiscalField;
            }
            set
            {
                this.valor_nota_fiscalField = value;
            }
        }

        /// <remarks/>
        public object natureza_nota_fiscal
        {
            get
            {
                return this.natureza_nota_fiscalField;
            }
            set
            {
                this.natureza_nota_fiscalField = value;
            }
        }

        /// <remarks/>
        public object descricao_objeto
        {
            get
            {
                return this.descricao_objetoField;
            }
            set
            {
                this.descricao_objetoField = value;
            }
        }

        /// <remarks/>
        public string valor_a_cobrar
        {
            get
            {
                return this.valor_a_cobrarField;
            }
            set
            {
                this.valor_a_cobrarField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogObjeto_postalServico_adicional
    {

        private byte codigo_servico_adicionalField;

        private object valor_declaradoField;

        /// <remarks/>
        public byte codigo_servico_adicional
        {
            get
            {
                return this.codigo_servico_adicionalField;
            }
            set
            {
                this.codigo_servico_adicionalField = value;
            }
        }

        /// <remarks/>
        public object valor_declarado
        {
            get
            {
                return this.valor_declaradoField;
            }
            set
            {
                this.valor_declaradoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class correioslogObjeto_postalDimensao_objeto
    {

        private byte tipo_objetoField;

        private byte dimensao_alturaField;

        private byte dimensao_larguraField;

        private byte dimensao_comprimentoField;

        private byte dimensao_diametroField;

        /// <remarks/>
        public byte tipo_objeto
        {
            get
            {
                return this.tipo_objetoField;
            }
            set
            {
                this.tipo_objetoField = value;
            }
        }

        /// <remarks/>
        public byte dimensao_altura
        {
            get
            {
                return this.dimensao_alturaField;
            }
            set
            {
                this.dimensao_alturaField = value;
            }
        }

        /// <remarks/>
        public byte dimensao_largura
        {
            get
            {
                return this.dimensao_larguraField;
            }
            set
            {
                this.dimensao_larguraField = value;
            }
        }

        /// <remarks/>
        public byte dimensao_comprimento
        {
            get
            {
                return this.dimensao_comprimentoField;
            }
            set
            {
                this.dimensao_comprimentoField = value;
            }
        }

        /// <remarks/>
        public byte dimensao_diametro
        {
            get
            {
                return this.dimensao_diametroField;
            }
            set
            {
                this.dimensao_diametroField = value;
            }
        }
    }










}
