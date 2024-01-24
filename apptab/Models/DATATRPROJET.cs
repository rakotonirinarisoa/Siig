// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apptab.Models
{
    public class DATATRPROJET
    {
        public Guid? No { get; set; }
        public string REF { get; set; }
        public string OBJ { get; set; }
        public string TITUL { get; set; }
        public string MONT { get; set; }
        public string COMPTE { get; set; }
        public DateTime? DATE { get; set; }
    }
}
