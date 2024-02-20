// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace apptab.Models
{
    public partial class Model12 : DbContext
    {
        public Model12()
            : base("name=Model122")
        {
        }

        public virtual DbSet<HSI_SOAS> HSI_SOAS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
