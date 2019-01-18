namespace PluginProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Co_Workers
    {
        public int ID { get; set; }

        [Column("_Name")]
        [StringLength(50)]
        public string C_Name { get; set; }

        [Column("_Surname")]
        [StringLength(50)]
        public string C_Surname { get; set; }

        [Column("_Date_of_Birth", TypeName = "date")]
        public DateTime? C_Date_of_Birth { get; set; }

        [Column("_email")]
        [StringLength(50)]
        public string C_email { get; set; }

        [Column("_adress")]
        [StringLength(50)]
        public string C_adress { get; set; }
    }
}
