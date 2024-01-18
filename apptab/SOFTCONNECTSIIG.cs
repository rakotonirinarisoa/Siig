using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
