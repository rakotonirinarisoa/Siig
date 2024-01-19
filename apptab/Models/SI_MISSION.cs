namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_MISSION
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string MISSION { get; set; }

        public int? IDPROJET { get; set; }
    }
}
