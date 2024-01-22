namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_MAPPAGES
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string INSTANCE { get; set; }

        public int? AUTH { get; set; }

        [StringLength(50)]
        public string CONNEXION { get; set; }

        [StringLength(50)]
        public string CONNEXPWD { get; set; }

        [StringLength(50)]
        public string DBASE { get; set; }

        public int? IDPROJET { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DELETIONDATE { get; set; }
    }
}
