﻿namespace apptab
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SI_ACTIVITE
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ACTIVITE { get; set; }

        public int? IDPROJET { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DELETIONDATE { get; set; }

        [StringLength(50)]
        public string CODE { get; set; }
    }
}
