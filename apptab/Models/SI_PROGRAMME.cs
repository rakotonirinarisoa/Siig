namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_PROGRAMME
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string PROGRAMME { get; set; }

        public int? IDPROJET { get; set; }
    }
}
