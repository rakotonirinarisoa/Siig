namespace apptab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SI_PROSOA
    {
        public int ID { get; set; }

        public int? IDPROJET { get; set; }

        public int? IDSOA { get; set; }
    }
}
