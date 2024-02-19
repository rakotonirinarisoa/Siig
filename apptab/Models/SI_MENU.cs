namespace apptab
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SI_MENU
    {
        public int ID { get; set; }

        public string MT0 { get; set; }
        public string MT1 { get; set; }

        public string MT2 { get; set; }

        public string MP1 { get; set; }

        public string MP2 { get; set; }

        public string MP3 { get; set; }

        public string MP4 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CREATIONDATE { get; set; }
    }
}
