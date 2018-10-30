namespace GestionnaireClient.DAL
{
    using GestionnaireClient.Model;
    using System.Data.Entity;

    public class DBContextMgr : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « Customer » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « GestionnaireClient.CustomerContext » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « Customer » dans le fichier de configuration de l'application.
        public DBContextMgr() : base("DBContextMgr")
        {
            Database.SetInitializer<DBContextMgr>(new DbInitializer());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>().ToTable("Customers").HasKey(c => c.Id);


        //    base.OnModelCreating(modelBuilder);
        //}

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                 .HasMany<Sale>(s => s.Sales)
                 .WithMany(c => c.Products)
                 .Map(cs =>
                 {
                     cs.MapLeftKey("ProductRefId");
                     cs.MapRightKey("SaleRefId");
                     cs.ToTable("ProductSale");
                 });
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}