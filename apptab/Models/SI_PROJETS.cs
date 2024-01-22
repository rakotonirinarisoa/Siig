namespace apptab
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class SI_PROJETS
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string PROJET { get; set; }
<<<<<<< HEAD

        [Column(TypeName = "smalldatetime")]
=======
>>>>>>> 4125ae2d4be38c88afefd42bbf507c1316e38fbf
        public DateTime? DELETIONDATE { get; set; }
    }
}
