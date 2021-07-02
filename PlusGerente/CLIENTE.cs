namespace PlusGerente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENTE")]
    public partial class CLIENTE
    {
        [Key]
        public int CODIGO { get; set; }

        [Column("CLIENTE")]
        [StringLength(200)]
        public string CLIENTE1 { get; set; }

        [StringLength(200)]
        public string KEYB2W { get; set; }

        [StringLength(200)]
        public string USUARIOB2W { get; set; }

        [StringLength(200)]
        public string IDMERCADOLIVRE { get; set; }

        [StringLength(200)]
        public string SENHAMERCADOLIVRE { get; set; }

        [StringLength(200)]
        public string CODEMERCADOLIVRE { get; set; }

        [StringLength(200)]
        public string ACCESSTOKENMERCADOLIVRE { get; set; }

        [StringLength(200)]
        public string REFRESHTOKENMERCADOLIVRE { get; set; }

        [StringLength(250)]
        public string IDCLIENTEMERCADOLIVRE { get; set; }

        [StringLength(200)]
        public string TOKENEXPIRA { get; set; }

        [StringLength(200)]
        public string REMETENTE { get; set; }

        [StringLength(200)]
        public string ENDERECO { get; set; }

        [StringLength(20)]
        public string CEP { get; set; }

        [StringLength(200)]
        public string CIDADE { get; set; }

        [StringLength(2)]
        public string UF { get; set; }
    }
}
