namespace apptab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OPA_VALIDATIONS
    {
        public int ID { get; set; }

        public int? IDREGLEMENT { get; set; }

        public int? IDPROJET { get; set; }

        public int? ETAT { get; set; }

        public DateTime? DateIn { get; set; }

        public DateTime? DateOut { get; set; }

        public string ComptaG { get; set; }

        public string auxi { get; set; }

        public DateTime? DateP { get; set; }

        public string Journal { get; set; }
    }
}
