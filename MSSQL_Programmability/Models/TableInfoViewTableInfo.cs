namespace MSSQL_Programmability.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TableInfoViewTableInfo")]
    public partial class TableInfoViewTableInfo
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Execute { get; set; }

        public string Content { get; set; }

        public string Remark { get; set; }

        [StringLength(100)]
        public string Tags { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
