﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace apptab
{
    public partial class SOFTCONNECTOM : DbContext
    {
        public SOFTCONNECTOM()
            : base(connex)
        {
        }

        public static string connex = "name=SOFTCONNECTOM";

        public virtual DbSet<CPTADMIN_CHAINETRAITEMENT> CPTADMIN_CHAINETRAITEMENT { get; set; }
        public virtual DbSet<CPTADMIN_COMMENTAIRE> CPTADMIN_COMMENTAIRE { get; set; }
        public virtual DbSet<CPTADMIN_FLIQUIDATION> CPTADMIN_FLIQUIDATION { get; set; }
        public virtual DbSet<CPTADMIN_MLIQUIDATION> CPTADMIN_MLIQUIDATION { get; set; }
        public virtual DbSet<CPTADMIN_MLIQUIDATIONPJ> CPTADMIN_MLIQUIDATIONPJ { get; set; }
        public virtual DbSet<CPTADMIN_MODELEETATS> CPTADMIN_MODELEETATS { get; set; }
        public virtual DbSet<CPTADMIN_STRUCTURE> CPTADMIN_STRUCTURE { get; set; }
        public virtual DbSet<CPTADMIN_TRAITEMENT> CPTADMIN_TRAITEMENT { get; set; }
        public virtual DbSet<CPTADMIN_TYPEENGAGEMENT> CPTADMIN_TYPEENGAGEMENT { get; set; }
        public virtual DbSet<CPTADMIN_TYPEPROCEDURE> CPTADMIN_TYPEPROCEDURE { get; set; }
        public virtual DbSet<RTIERS> RTIERS { get; set; }
        public virtual DbSet<TP_MPIECES_JUSTIFICATIVES> TP_MPIECES_JUSTIFICATIVES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CPTADMIN_FLIQUIDATION>()
                .Property(e => e.COURSDEVISE)
                .HasPrecision(30, 12);

            modelBuilder.Entity<CPTADMIN_FLIQUIDATION>()
                .Property(e => e.COURSRAPPORT)
                .HasPrecision(30, 12);

            modelBuilder.Entity<CPTADMIN_FLIQUIDATION>()
                .HasMany(e => e.CPTADMIN_MLIQUIDATION)
                .WithRequired(e => e.CPTADMIN_FLIQUIDATION)
                .HasForeignKey(e => e.IDLIQUIDATION);

            modelBuilder.Entity<CPTADMIN_FLIQUIDATION>()
                .HasMany(e => e.CPTADMIN_MLIQUIDATIONPJ)
                .WithRequired(e => e.CPTADMIN_FLIQUIDATION)
                .HasForeignKey(e => e.IDLIQUIDATION);

            modelBuilder.Entity<CPTADMIN_MLIQUIDATION>()
                .Property(e => e.MONTANTLOCAL)
                .HasPrecision(30, 12);

            modelBuilder.Entity<CPTADMIN_MLIQUIDATION>()
                .Property(e => e.MONTANTRAPPORT)
                .HasPrecision(30, 12);

            modelBuilder.Entity<CPTADMIN_MLIQUIDATION>()
                .Property(e => e.MONTANTDEVISE)
                .HasPrecision(30, 12);

            modelBuilder.Entity<RTIERS>()
                .Property(e => e.NBJOURS)
                .HasPrecision(10, 0);

            modelBuilder.Entity<RTIERS>()
                .Property(e => e.JOURREF)
                .HasPrecision(10, 0);

            modelBuilder.Entity<RTIERS>()
                .Property(e => e.IMPORTID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TP_MPIECES_JUSTIFICATIVES>()
                .Property(e => e.NOMBRE)
                .HasPrecision(10, 0);

            modelBuilder.Entity<TP_MPIECES_JUSTIFICATIVES>()
                .Property(e => e.RANG)
                .HasPrecision(10, 0);

            modelBuilder.Entity<TP_MPIECES_JUSTIFICATIVES>()
                .Property(e => e.MONTANT)
                .HasPrecision(30, 6);
        }
    }
}
