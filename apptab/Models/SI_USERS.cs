namespace apptab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SI_USERS
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string LOGIN { get; set; }

        [StringLength(50)]
        public string PWD { get; set; }

        //public int? ROLE { get; set; }

        public int? IDPROJET { get; set; }

        public Role ROLE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DELETIONDATE { get; set; }
    }
}
