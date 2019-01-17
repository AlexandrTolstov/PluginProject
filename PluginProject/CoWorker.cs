namespace PluginProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoWorker : DbContext
    {
        public CoWorker()
            : base("name=CoWorker")
        {
        }

        public virtual DbSet<Co_Workers> Co_Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Co_Workers>()
                .Property(e => e.C_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Co_Workers>()
                .Property(e => e.C_Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Co_Workers>()
                .Property(e => e.C_email)
                .IsUnicode(false);

            modelBuilder.Entity<Co_Workers>()
                .Property(e => e.C_adress)
                .IsUnicode(false);
        }
    }
}
