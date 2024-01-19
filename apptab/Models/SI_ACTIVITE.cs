namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_ACTIVITE
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ACTIVITE { get; set; }

        public int? IDPROJET { get; set; }
    }
}
