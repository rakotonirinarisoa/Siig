namespace apptab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SI_CONVENTION
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CONVENTION { get; set; }

        public int? IDPROJET { get; set; }
    }
}
