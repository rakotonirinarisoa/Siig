using System.Data.Entity;
using apptab.Models;

namespace apptab
{
    public partial class SOFTCONNECTSIIG : DbContext
    {
        public SOFTCONNECTSIIG()
            : base("name=SOFTCONNECTSIIG")
        {
        }

        public virtual DbSet<SI_ROLES> SI_ROLES { get; set; }
        public virtual DbSet<SI_PROJETS> SI_PROJETS { get; set; }
        public virtual DbSet<SI_USERS> SI_USERS { get; set; }
        public virtual DbSet<SI_SOAS> SI_SOAS { get; set; }
        public virtual DbSet<SI_PROSOA> SI_PROSOA { get; set; }
        public virtual DbSet<SI_MAPPAGES> SI_MAPPAGES { get; set; }
        public virtual DbSet<SI_FINANCEMENT> SI_FINANCEMENT { get; set; }
        public virtual DbSet<SI_ACTIVITE> SI_ACTIVITE { get; set; }
        public virtual DbSet<SI_CATEGORIE> SI_CATEGORIE { get; set; }
        public virtual DbSet<SI_CONVENTION> SI_CONVENTION { get; set; }
        public virtual DbSet<SI_ENGAGEMENT> SI_ENGAGEMENT { get; set; }
        public virtual DbSet<SI_MINISTERE> SI_MINISTERE { get; set; }
        public virtual DbSet<SI_MISSION> SI_MISSION { get; set; }
        public virtual DbSet<SI_PROCEDURE> SI_PROCEDURE { get; set; }
        public virtual DbSet<SI_PROGRAMME> SI_PROGRAMME { get; set; }
        public virtual DbSet<SI_MAIL> SI_MAIL { get; set; }
        public virtual DbSet<SI_TRAITPROJET> SI_TRAITPROJET { get; set; }
        public virtual DbSet<SI_PARAMETAT> SI_PARAMETAT { get; set; }
        public virtual DbSet<OPA_CRYPTO> OPA_CRYPTO { get; set; }
        public virtual DbSet<OPA_FTP> OPA_FTP { get; set; }
        public virtual DbSet<SI_DELAISTRAITEMENT> SI_DELAISTRAITEMENT { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
