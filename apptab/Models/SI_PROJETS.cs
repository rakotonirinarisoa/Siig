namespace apptab
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class SI_PROJETS
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string PROJET { get; set; }
        public DateTime? DELETIONDATE { get; set; }
    }
}
