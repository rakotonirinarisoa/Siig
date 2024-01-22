namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_SOAS
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string SOA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DELETIONDATE { get; set; }
    }
}
