namespace MSSQL_Programmability.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FunAndSP")]
    public partial class FunAndSP
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Execute { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Column(TypeName = "ntext")]
        public string Remark { get; set; }

        [StringLength(100)]
        public string Tags { get; set; }

        [Column(Order = 0, TypeName = "datetime2")]
        public DateTime CreatedTime { get; set; }

        [Column(Order = 1, TypeName = "datetime2")]
        public DateTime UpdateTime { get; set; }
    }
}
