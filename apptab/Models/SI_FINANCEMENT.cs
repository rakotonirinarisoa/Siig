namespace apptab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SI_FINANCEMENT
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string FINANCEMENT { get; set; }

        public int? IDPROJET { get; set; }
    }
}
