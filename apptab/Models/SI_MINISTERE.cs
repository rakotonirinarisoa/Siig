namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_MINISTERE
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string MINISTERE { get; set; }

        public int? IDPROJET { get; set; }
    }
}
