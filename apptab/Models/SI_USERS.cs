namespace apptab
{
    using System.ComponentModel.DataAnnotations;

    public partial class SI_USERS
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string LOGIN { get; set; }

        [StringLength(50)]
        public string PWD { get; set; }

        //public int? ROLE { get; set; }

        public int? IDPROJET { get; set; }

        public Role ROLE { get; set; }
    }
}
