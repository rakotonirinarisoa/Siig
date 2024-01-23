namespace apptab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SI_TRAITPROJET
    {
        public int ID { get; set; }

        public string No { get; set; }

        [StringLength(50)]
        public string REF { get; set; }

        [StringLength(50)]
        public string OBJ { get; set; }

        [StringLength(50)]
        public string TITUL { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MONT { get; set; }

        [StringLength(50)]
        public string COMPTE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DATE { get; set; }

        public int? ETAT { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DATECRE { get; set; }
    }
}
