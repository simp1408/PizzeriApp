using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PizzeriApp
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<DettagliOrdine> DettagliOrdine { get; set; }
        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .Property(e => e.PrezzoPizza)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ordini)
                .WithOptional(e => e.Utente)
                .HasForeignKey(e => e.IdUser);
        }
    }
}
