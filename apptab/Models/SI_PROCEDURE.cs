namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_PROCEDURE
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string PROCEDURE { get; set; }

        public int? IDPROJET { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DELETIONDATE { get; set; }

        [StringLength(50)]
        public string CODE { get; set; }
    }
}
