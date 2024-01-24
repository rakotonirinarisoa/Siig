<<<<<<< HEAD
﻿namespace apptab
=======
namespace apptab.Models
>>>>>>> 794aa41cd746f0392bf3511c624a905cf52c3b01
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

        public string REF { get; set; }

        public string OBJ { get; set; }

        public string TITUL { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MONT { get; set; }

        public string COMPTE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DATE { get; set; }

        public int? ETAT { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DATECRE { get; set; }

<<<<<<< HEAD
        public int? IDPROJET { get; set; }
=======
        [Required]
        public string IDPROJET { get; set; }
>>>>>>> 794aa41cd746f0392bf3511c624a905cf52c3b01
    }
}
