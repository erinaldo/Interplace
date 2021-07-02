namespace PlusGerente
{
    using System.Data.Entity;

    public partial class InterPlaceModel : DbContext
    {
        public InterPlaceModel()
            : base("name=InterPlaceModel")
        {
        }

        public virtual DbSet<CLIENTE> CLIENTE { get; set; }
        public virtual DbSet<VENDAS> VENDAS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.CLIENTE1)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.KEYB2W)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.USUARIOB2W)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.IDMERCADOLIVRE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.SENHAMERCADOLIVRE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.CODEMERCADOLIVRE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.ACCESSTOKENMERCADOLIVRE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.REFRESHTOKENMERCADOLIVRE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.IDCLIENTEMERCADOLIVRE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.TOKENEXPIRA)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.REMETENTE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.ENDERECO)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.CIDADE)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENTE>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<VENDAS>()
                .Property(e => e.NOTAFISCAL)
                .IsUnicode(false);

            modelBuilder.Entity<VENDAS>()
                .Property(e => e.PEDIDO)
                .IsUnicode(false);

            modelBuilder.Entity<VENDAS>()
                .Property(e => e.LOJA)
                .IsUnicode(false);

            modelBuilder.Entity<VENDAS>()
                .Property(e => e.ARQUIVO)
                .IsUnicode(false);
        }
    }
}
