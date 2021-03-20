namespace MSSQL_Programmability.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SQL_Programmability : DbContext
    {
        public SQL_Programmability()
            : base("name=SQL_Programmability")
        {
        }

        public virtual DbSet<TableInfoViewTableInfo> TableInfoViewTableInfo { get; set; }
        public virtual DbSet<FunAndSP> FunAndSP { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FunAndSP>()
                .Property(e => e.CreatedTime)
                .HasPrecision(3);

            modelBuilder.Entity<FunAndSP>()
                .Property(e => e.UpdateTime)
                .HasPrecision(3);
        }
    }
}
