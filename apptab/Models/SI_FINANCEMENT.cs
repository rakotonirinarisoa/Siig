namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_FINANCEMENT
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string FINANCEMENT { get; set; }

        public int? IDPROJET { get; set; }
    }
}
