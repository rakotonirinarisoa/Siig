namespace apptab
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SI_ROLES
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string INTITULES { get; set; }
    }

    public enum Role
    {
        SAdministrateur, Administrateur, Autre //, ORDSEC, Consultation, PRORDESEC
    }
}
