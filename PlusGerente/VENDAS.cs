namespace PlusGerente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VENDAS
    {
        public int CODIGO { get; set; }

        [Key]
        [StringLength(50)]
        public string NOTAFISCAL { get; set; }

        public int? IMPRESSOES { get; set; }

        [StringLength(25)]
        public string PEDIDO { get; set; }

        [StringLength(20)]
        public string LOJA { get; set; }

        public byte[] ETIQUETA { get; set; }

        public bool? FINALIZADO { get; set; }

        public byte[] ETIQUETATXT { get; set; }

        public byte[] ETIQUETAPDF { get; set; }

        public string ETIQUETATXTTXT { get; set; }

        public string ETIQUETAPDFTXT { get; set; }

        [StringLength(250)]
        public string ARQUIVO { get; set; }
    }
}
